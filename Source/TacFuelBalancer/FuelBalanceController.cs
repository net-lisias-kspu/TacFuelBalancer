/**
 * FuelBalanceController.cs
 * 
 * Thunder Aerospace Corporation's Fuel Balancer for the Kerbal Space Program, by Taranis Elsu
 * 
 * (C) Copyright 2013, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KSP.UI.Screens;
using ToolbarControl_NS;

using Log = TacFuelBalancer.Log;

namespace Tac
{

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    sealed internal class FuelBalanceController : MonoBehaviour
    {
        private const int MaxRecentVessels = 5;
        private const double AmountEpsilon = 0.001, PercentEpsilon = 0.00001;
        
        sealed internal class VesselInfo
        {
            public VesselInfo()
            {
                resources     = new Dictionary<string, ResourceInfo>();
                lastPartCount = 0;
                lastSituation = Vessel.Situations.PRELAUNCH;
            }
            
            public VesselInfo(Vessel vessel)
            {
                this.vessel        = vessel;
                this.resources     = new Dictionary<string, ResourceInfo>();
                this.lastSituation = vessel.situation;
                this.lastPartCount = vessel.parts.Count;
            }

            public readonly Vessel vessel;
            public Dictionary<string, ResourceInfo> resources;
            public Vessel.Situations lastSituation;
            public int lastPartCount;
        }

        internal static Settings settings = null;
        internal MainWindow mainWindow;
#if false
        internal static SettingsWindow settingsWindow;
#endif
        private HelpWindow helpWindow;
        private string configFilename;
		//private UnifiedButton button;
        private VesselInfo vesselInfo;
        private readonly List<VesselInfo> recentVessels = new List<VesselInfo>(MaxRecentVessels);
		private bool UiHidden;
		private DateTime? _nextListRebuild;

        public static string ROOT_PATH { get { return KSPUtil.ApplicationRootPath; } }
        private static string GAMEDATA_FOLDER { get { return ROOT_PATH + "GameData/"; } }
        public static String MOD_FOLDER { get { return GAMEDATA_FOLDER + "net.lisias.ksp/TacFuelBalancer/"; } }
        public static string DATA_FOLDER { get { return MOD_FOLDER + "PluginData/"; } }

        void Awake()
        {
            Log.detail("Awake");
            //configFilename = IOUtils.GetFilePathFor(this.GetType(), "FuelBalancer.cfg");
            configFilename = DATA_FOLDER + "FuelBalancer.cfg";
            Log.dbg("Awake, configFilename: {0}", configFilename);
            // No need to check to see if the directory exists, will create it if it doesn't
            System.IO.Directory.CreateDirectory(DATA_FOLDER);
            if (settings == null)
                settings = new Settings();
            if (settings == null)
                Log.dbg("Error, settings is null");
            else
                Log.dbg("Settings inititalized");
#if false
            settingsWindow = new SettingsWindow(settings);
#endif
            helpWindow = new HelpWindow();
            mainWindow = new MainWindow(this, settings, /* settingsWindow, */ helpWindow);
			mainWindow.WindowClosed += OnWindowClosed;
			Log.dbg( "Making Buttons" );
			InitButtons( );
			Log.dbg( "Made Buttons" );
            
            vesselInfo = new VesselInfo( );
        }



        void Start()
        {
            Log.detail( "Start" );
            Load( );


			// Callbacks for F2
			GameEvents.onHideUI.Add( OnHideUI );
			GameEvents.onShowUI.Add( OnShowUI );
			UiHidden = false;

            // Make sure the resource/part list is correct after other mods, such as StretchyTanks, do their thing.
//			this.Log( "Need RebuildActiveVesselLists" );
			_nextListRebuild = DateTime.Now.AddSeconds( 2 );
        }

        void OnDestroy()
        {
            Log.detail("OnDestroy");
            mainWindow.WindowClosed -= OnWindowClosed;
            Save();
            RemoveButtons();
        }

        void Update()
        {
            foreach (ResourceInfo resourceInfo in vesselInfo.resources.Values)
            {
                foreach (ResourcePartMap partInfo in resourceInfo.parts)
                {
                    if (partInfo.isHighlighted || mainWindow.IsVisible() && resourceInfo.isShowing && partInfo.isSelected)
                    {
                        partInfo.part.SetHighlightColor(Color.blue);
                        partInfo.part.SetHighlight(true, false);
                    }
                }
            }
        }

        void FixedUpdate()
        {
            if (!FlightGlobals.ready)
            {
                Log.detail("FlightGlobals are not valid yet.");
                return;
            }

            Vessel activeVessel = FlightGlobals.fetch.activeVessel;
            if (activeVessel == null)
            {
                Log.detail("No active vessel yet.");
                return;
            }


			// This shouldn't prevent fuel being pumped around when the window is closed.
			// This update is scheduled within Start()
			// But we can't get to work immediatly because the ship gets modified by addons.
			// We need to wait until they have changed the size of all the tanks.
			// As we don't save our state on the ship all instructions to pump fuel happen via the FB window.
			// By the time a user has opened the window the ship will have finished being modified
			if( _nextListRebuild != null && _nextListRebuild.Value < DateTime.Now )
			{
				if( mainWindow.IsVisible( ) ) // Don't bother rebuilding the lists until the window is open
				{
					if( RebuildActiveVesselLists( ) )
						_nextListRebuild = null; // Don't do it again
					else
						return;  // Don't continue
				}
				else
					return; // Window isn't open
			}



			if( vesselInfo.vessel == null ) // Prevent exceptions
				RebuildLists( activeVessel );
			if( vesselInfo.vessel == null )
				return; // Really don't want exceptions



            if (activeVessel.isEVA)
            {
                toolbarControl.SetFalse();
                //button.SetDisabled( );
                //button.SetOff( );
                toolbarControl.Enabled = false;
                mainWindow.SetVisible(false);
                return;
            }
            //else if (!button.IsEnabled())
            else if (!toolbarControl.Enabled)
            {
                //button.SetEnabled( );
                toolbarControl.Enabled = true;
            }

            if (activeVessel != vesselInfo.vessel || activeVessel.situation != vesselInfo.lastSituation || activeVessel.Parts.Count != vesselInfo.lastPartCount)
            {
                RebuildLists(activeVessel);
            }

            if (!HasPower())
            {
                return;
            }

            // Do any fuel transfers
            double maxFuelFlow = settings.MaxFuelFlow * settings.RateMultiplier * TimeWarp.fixedDeltaTime;
            foreach (ResourceInfo resourceInfo in vesselInfo.resources.Values)
            {
                foreach (ResourcePartMap partInfo in resourceInfo.parts)
                {
                    SynchronizeFlowState(partInfo);
                }
                
                foreach (ResourcePartMap partInfo in resourceInfo.parts)
                {
                    if (partInfo.direction == TransferDirection.IN)
                    {
                        TransferIn(maxFuelFlow, resourceInfo, partInfo);
                    }
                    else if (partInfo.direction == TransferDirection.OUT)
                    {
                        TransferOut(maxFuelFlow, resourceInfo, partInfo);
                    }
                    else if (partInfo.direction == TransferDirection.DUMP)
                    {
                        DumpOut(maxFuelFlow, resourceInfo, partInfo);
                    }
                }

                BalanceResources(maxFuelFlow, resourceInfo.parts.FindAll(
                    rpm => rpm.direction == TransferDirection.BALANCE || (resourceInfo.balance && rpm.direction == TransferDirection.NONE)));

                if (settings.BalanceIn)
                {
                    BalanceResources(maxFuelFlow, resourceInfo.parts.FindAll(rpm => rpm.direction == TransferDirection.IN));
                }
                if (settings.BalanceOut)
                {
                    BalanceResources(maxFuelFlow, resourceInfo.parts.FindAll(rpm => rpm.direction == TransferDirection.OUT));
                }
            }
        }


		/// <summary>
		/// Called by Unity to draw the GUI - can be called many times per frame.
		/// </summary>
		public void OnGUI( )
		{
			if( !UiHidden )
			{
				if( vesselInfo.vessel != null ) // Prevent exceptions
				{
					mainWindow.DrawWindow( );
#if false
                    settingsWindow.DrawWindow( );
#endif
					helpWindow.DrawWindow( );
				}
			}
		}



        /*
         * Checks the PartResource's flow state (controlled from the part's right click menu), and makes our state match its state.
         */
        private static void SynchronizeFlowState(ResourcePartMap partInfo)
        {
            if (!partInfo.resource.Locked && partInfo.direction == TransferDirection.LOCKED)
            {
                partInfo.direction = TransferDirection.NONE;
            }
            else if (partInfo.resource.Locked && partInfo.direction != TransferDirection.LOCKED)
            {
                partInfo.direction = TransferDirection.LOCKED;
            }
        }

        public Dictionary<string, ResourceInfo> GetResourceInfo()
        {
            return vesselInfo.resources;
        }

        public bool IsPrelaunch()
        {
            return (vesselInfo.vessel.mainBody == FlightGlobals.Bodies[1]) &&
                (vesselInfo.vessel.situation == Vessel.Situations.PRELAUNCH || vesselInfo.vessel.situation == Vessel.Situations.LANDED);
        }

        public bool IsControllable()
        {
            return vesselInfo.vessel.IsControllable && HasPower();
        }

        public bool HasPower()
        {
            ResourceInfo electric;
            return vesselInfo.resources.TryGetValue("ElectricCharge", out electric) &&
                   electric.parts.Any(p => p.resource.Amount > 0.01);
        }

        public void SortParts(Comparison<ResourcePartMap> comparer)
        {
            foreach (ResourceInfo resource in vesselInfo.resources.Values)
            {
                // we need a stable sort, but the built-in .NET sorting methods are unstable, so we'll use insertion sort
                List<ResourcePartMap> parts = resource.parts;
                for (int i=1; i < parts.Count; i++)
                {
                    ResourcePartMap part = parts[i];
                    int j;
                    for (j=i; j > 0 && comparer(parts[j-1], part) > 0; j--)
                    {
                        parts[j] = parts[j-1];
                    }
                    parts[j] = part;
                }
            }
        }
        
        private void Load()
        {
            //if (File.Exists<FuelBalanceController>(configFilename))
            if (System.IO.File.Exists(configFilename))
            {
                ConfigNode config = ConfigNode.Load(configFilename);
                settings.Load(config);
                mainWindow.Load(config);
#if false
                settingsWindow.Load(config);
#endif
                helpWindow.Load(config);
            }
        }

        private void Save()
        {
            ConfigNode config = new ConfigNode();
            settings.Save(config);
            mainWindow.Save(config);
#if false
            settingsWindow.Save(config);
#endif
            helpWindow.Save(config);

            config.Save(configFilename);
        }

		private void OnWindowClosed( object sender, EventArgs e )
		{
            toolbarControl.SetFalse();
			//button.SetOff( );
		}

		private void OnIconOpen( object sender, EventArgs e  )
        {
            mainWindow.SetVisible( true );
            toolbarControl.SetTrue();
			//button.SetOn( );
        }
		private void OnIconClose( object sender, EventArgs e  )
		{
			mainWindow.SetVisible( false );
            toolbarControl.Enabled = false;
			//button.SetOff( );
		}



		// Actually this most often does a refresh of an already added tank
        private void AddResource(string resourceName, Part part, Dictionary<object,int> shipIds, Func<Part,string,PartResourceInfo> infoCreator)
        {
            ResourceInfo resourceInfo;
            if (!vesselInfo.resources.TryGetValue(resourceName, out resourceInfo))
            {
                vesselInfo.resources[resourceName] = resourceInfo = new ResourceInfo(GetResourceTitle(resourceName));
            }
            List<ResourcePartMap> resourceParts = resourceInfo.parts;
            ResourcePartMap partInfo = resourceParts.Find(info => info.part.Equals(part));
            if (partInfo == null)
            {
                resourceParts.Add(new ResourcePartMap(infoCreator(part, resourceName), part, shipIds[part]));

//				string ToLog = "ADD " + part.name + " - " + resourceName;
//				if( part.Resources.Contains( resourceName ) ) // _RocketFuel does't exist
//					ToLog += ":" + part.Resources[ resourceName ].amount.ToString( );
//				this.Log( ToLog );
            }
            else
            {
                // Make sure we are still pointing at the right resource instance. This is a fix for compatibility with StretchyTanks.
                partInfo.resource.Refresh(part);
//				string ToLog = "REFRESH " + part.name + " - " + resourceName;
//				if( part.Resources.Contains( resourceName ) ) // _RocketFuel does't exist
//					ToLog += ":" + part.Resources[ resourceName ].amount.ToString( );
//				this.Log( ToLog );
            }
        }
        


        private static string GetResourceTitle(string resourceName)
        {
            switch (resourceName)
            {
                case "_RocketFuel": return "Rocket";
                case "ElectricCharge": return "Electric";
                case "LiquidFuel": return "Liquid";
                case "MonoPropellant": return "RCS";
                case "XenonGas": return "Xenon";
                default: return resourceName;
            }
        }



        private void RebuildLists(Vessel vessel)
        {
            Log.detail("Rebuilding resource lists.");
            // try to restore the old vessel info if we're switching vessels
            if (vesselInfo.vessel != vessel)
            {
                recentVessels.RemoveAll(v => !FlightGlobals.Vessels.Contains(v.vessel)); // remove information about dead vessels
                int index = recentVessels.FindIndex(v => v.vessel == vessel);
                if (vesselInfo.vessel != null) // save the current data if it's not the initial, uninitialized state
                {
                    recentVessels.Add(vesselInfo);
                }
                if (index >= 0) // if we found the vessel in our memory, use it
                {
                    vesselInfo = recentVessels[index];
                    recentVessels.RemoveAt(index); // we'll add it back the next time we switch ships
                }
                else
                {
                    vesselInfo = new VesselInfo(vessel);
                }
                
                if (recentVessels.Count >= MaxRecentVessels) 
                {
                    recentVessels.RemoveAt(0);
                }
            }



		// Remove parts that don't exist any more
            List<string> toDelete = new List<string>();
            foreach (KeyValuePair<string, ResourceInfo> resourceEntry in vesselInfo.resources)
            {
//				this.Log( "Removing Parts for: " + resourceEntry.Key );
				resourceEntry.Value.parts.RemoveAll(partInfo => !vessel.parts.Contains(partInfo.part));

				if (resourceEntry.Value.parts.Count == 0)
				{
//					this.Log( "Remove: " + resourceEntry.Key );
					toDelete.AddUnique(resourceEntry.Key);
				}
            }
		// See if we have eliminated a whole resource
			foreach (string resource in toDelete)
            {
                vesselInfo.resources.Remove(resource);
//				this.Log( "Removed: " + resource );
            }



		// If tanks change their contents then we need to keep up.
			toDelete.Clear( );
            foreach( Part part in vessel.parts )
            {
				foreach( KeyValuePair<string, ResourceInfo> resourceEntry in vesselInfo.resources )
				{
					if( resourceEntry.Key == "_RocketFuel" ) // "_RocketFuel" isn't real
					{
						List<ResourcePartMap> PartsToRemove = new List<ResourcePartMap>( );
						foreach( ResourcePartMap pi in resourceEntry.Value.parts )
						{
							if( pi.part == part )
							{
								if( !part.Resources.Contains( "Oxidizer" ) || !part.Resources.Contains( "LiquidFuel" ) )
								{
									PartsToRemove.Add( pi );
//									this.Log( part.name + " changed" );
								}
							}
						}
						foreach( ResourcePartMap pi in PartsToRemove )
							resourceEntry.Value.parts.Remove( pi );
						if( resourceEntry.Value.parts.Count == 0 )
						{
//							this.Log( "Remove: " + resourceEntry.Key );
							toDelete.AddUnique( resourceEntry.Key );
						}
					}
					else
					{
						List<ResourcePartMap> PartsToRemove = new List<ResourcePartMap>( );
						foreach( ResourcePartMap pi in resourceEntry.Value.parts )
						{
							if( pi.part == part )
							{
								if( !part.Resources.Contains( resourceEntry.Key ) )
								{
									PartsToRemove.Add( pi );
//									this.Log( part.name + " changed" );
								}
							}
						}
						foreach( ResourcePartMap pi in PartsToRemove )
							resourceEntry.Value.parts.Remove( pi );
						if( resourceEntry.Value.parts.Count == 0 )
						{
//							this.Log( "Remove: " + resourceEntry.Key );
							toDelete.AddUnique( resourceEntry.Key );
						}
					}
				}
			}
		// See if we have eliminated a whole resource
			foreach( string resource in toDelete )
			{
				vesselInfo.resources.Remove( resource );
//				this.Log( "Removed: " + resource );
			}



		// Add all resources in all tanks
            Dictionary<object,int> shipIds = ComputeShipIds(vessel);
            foreach (Part part in vessel.parts)
            {
				if (part.Resources.Contains("Oxidizer") && part.Resources.Contains("LiquidFuel"))
                {
                    AddResource("_RocketFuel", part, shipIds, (p,n) => { var r = new RocketFuelResource(); r.Refresh(p); return r; });
                }
                foreach (PartResource resource in part.Resources)
                {
//this.Log( part.name + " - " + resource.resourceName + ":" + resource.amount.ToString( ) );
                    // skip the electric charge resource of engines with alternators, because they can't be balanced.
                    // any charge placed in an alternator just disappears
                    if (resource.resourceName == "ElectricCharge" && part.Modules.GetModules<ModuleAlternator>().Count != 0)
                    {
                        continue;
                    }
                    AddResource(resource.resourceName, part, shipIds, (p,n) => { var r = new SimplePartResource(n); r.Refresh(p); return r; });
                }
            }
            
            SortParts((a,b) => a.shipId - b.shipId); // make sure resources are grouped by ship ID

            vesselInfo.lastPartCount = vessel.parts.Count;
            vesselInfo.lastSituation = vessel.situation;
        }



		public bool RebuildActiveVesselLists( )
        {
//			this.Log( "RebuildActiveVesselLists" );
            if (FlightGlobals.ready && FlightGlobals.fetch.activeVessel != null)
            {
                RebuildLists(FlightGlobals.fetch.activeVessel);
//				this.Log( "RebuildActiveVesselLists OK" );
				return true;
            }
//			this.Log( "RebuildActiveVesselLists FAILED" );
			return false;
        }
        
        private void BalanceResources(double maxFlow, List<ResourcePartMap> balanceParts)
        {
            if(balanceParts.Count < 2) return;
            
            // sort the parts by percent full and figure out what the desired percentage is
            PartResourceInfo[] resources = new PartResourceInfo[balanceParts.Count];
            double totalAmount = 0, totalCapacity = 0;
            for(int i=0; i<balanceParts.Count; i++)
            {
                resources[i]   = balanceParts[i].resource;
                totalAmount   += resources[i].Amount;
                totalCapacity += resources[i].MaxAmount;
            }
            Array.Sort(resources, (a,b) => a.PercentFull.CompareTo(b.PercentFull));
            double desiredPercentage = totalAmount / totalCapacity;

            // if the difference between the fullest and emptiest tank is small, we're done
            if(resources[resources.Length-1].PercentFull - resources[0].PercentFull < PercentEpsilon) return;
            
            // work from both sides transferring from fuller tanks (near the end) to emptier tanks (near the beginning)
            for(int di=0, si=resources.Length-1; si > di && desiredPercentage - resources[di].PercentFull >= PercentEpsilon; di++)
            {
                PartResourceInfo dest = resources[di];
                double needed = (desiredPercentage - dest.PercentFull) * dest.MaxAmount;
                for(; si > di && resources[si].PercentFull - desiredPercentage >= PercentEpsilon; si--)
                {
                    PartResourceInfo src = resources[si];
                    double available = Math.Min(maxFlow, (src.PercentFull-desiredPercentage) * src.MaxAmount);
                    needed -= src.TransferTo(dest, Math.Min(available, needed));
                    if (needed < AmountEpsilon) break; // if the dest tank became full enough, move to the next without advancing the source tank
                }
            }
        }

        private void TransferIn(double maxFlow, ResourceInfo resourceInfo, ResourcePartMap destPart)
        {
            double required = destPart.resource.MaxAmount - destPart.resource.Amount;
            if(required < AmountEpsilon) return;
            required = Math.Min(required, maxFlow);

            var srcParts = resourceInfo.parts.FindAll(
                rpm => (rpm.resource.Amount >= AmountEpsilon) &&
                       (rpm.direction == TransferDirection.NONE || rpm.direction == TransferDirection.OUT || rpm.direction == TransferDirection.DUMP ||
                        rpm.direction == TransferDirection.BALANCE));
            if(srcParts.Count == 0) return;
            double takeFromEach = required / srcParts.Count;
            foreach (ResourcePartMap srcPart in srcParts)
            {
                if (destPart.part != srcPart.part)
                {
                    srcPart.resource.TransferTo(destPart.resource, takeFromEach);
                }
            }
        }

        private void TransferOut(double maxFlow, ResourceInfo resourceInfo, ResourcePartMap srcPart)
        {
            double available = srcPart.resource.Amount;
            if(available < AmountEpsilon) return;
            available = Math.Min(available, maxFlow);
            
            var destParts = resourceInfo.parts.FindAll(
                rpm => (rpm.resource.MaxAmount - rpm.resource.Amount) >= AmountEpsilon &&
                       (rpm.direction == TransferDirection.NONE || rpm.direction == TransferDirection.IN || rpm.direction == TransferDirection.BALANCE));
            if(destParts.Count == 0) return;
            double giveToEach = available / destParts.Count;
            foreach (ResourcePartMap destPart in destParts)
            {
                if (srcPart.part != destPart.part)
                {
                    srcPart.resource.TransferTo(destPart.resource, giveToEach);
                }
            }
        }

        private void DumpOut(double maxFlow, ResourceInfo resourceInfo, ResourcePartMap partInfo)
        {
            double available = partInfo.resource.Amount;
            if(available < AmountEpsilon) return;
            partInfo.resource.SetAmount(Math.Max(0, available - maxFlow));
        }
        
        private static Dictionary<object,int> ComputeShipIds(Vessel vessel)
        {
            Dictionary<object,int> shipIds = new Dictionary<object, int>(vessel.parts.Count);
            if (vessel.parts.Count != 0)
            {
                Part rootPart = vessel.parts[0];
                while(rootPart.parent != null) rootPart = rootPart.parent;
                
                int shipId = 1;
                ComputeShipIds(shipIds, rootPart, shipId, ref shipId);
            }
            return shipIds;
        }
        
        private static void ComputeShipIds(Dictionary<object,int> shipIds, Part part, int shipId, ref int shipCounter)
        {
            shipIds[part] = shipId;
            if(part.children.Count != 0)
            {
                // if the part is a docking node, its children belong to another ship (unless the node was the root,
                // in which case there's nothing on the other side, so it's not really two ships)
                if(part.parent != null && part.Modules.GetModules<ModuleDockingNode>().Count != 0)
                {
                    shipId = ++shipCounter;
                }
                foreach (Part child in part.children)
                {
                    ComputeShipIds(shipIds, child, shipId, ref shipCounter);
                }
            }
        }




		/// <summary>
		/// Initializes the toolbar button.
		/// </summary>
		private void InitButtons( )
		{
            Log.dbg( "InitButtons" );
			RemoveButtons( );
			AddButtons( );
            Log.dbg( "InitButtons Done" );
		}


        private ToolbarControl toolbarControl;
        internal const string MODID = "TAC";
        internal const string MODNAME = "Tac Fuel Balancer";

        void InitToolbarController()
        {
            if (toolbarControl == null)
            {
                GameObject gameObject = new GameObject();
                toolbarControl = gameObject.AddComponent<ToolbarControl>();
                toolbarControl.AddToAllToolbars(DoOnButtonOn, DoOnButtonOff,
                    ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW,
                    MODID,
                    "FB",
                    "net.lisias.ksp/TacFuelBalancer/PluginData/Icons/icon-tac-fuel",
                    "net.lisias.ksp/TacFuelBalancer/PluginData/Icons/icon-tac-fuel-small",
                    MODNAME);
            }
        }

        void DoOnButtonOn()
        {
            mainWindow.SetVisible(true);
            //button.SetOn();
        }
        void DoOnButtonOff()
        {
            mainWindow.SetVisible(false);
            //button.SetOff();
        }

        /// <summary>
        /// Add the buttons
        /// </summary>
        private void AddButtons( )
		{
            InitToolbarController();
		}



        /// <summary>
        /// Remove the buttons
        /// </summary>
		private void RemoveButtons( )
		{
            if (toolbarControl != null)
            {
                toolbarControl.OnDestroy();
                Destroy(toolbarControl);
                toolbarControl = null;
            }
		}


		// F2 support
		void OnHideUI( )
		{
			UiHidden = true;
		}
		void OnShowUI( )
		{
			UiHidden = false;
		}


    }
}
