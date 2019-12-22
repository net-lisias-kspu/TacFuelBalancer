/**
 * TacFuelBalancer.cs
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

namespace Tac
{
    class TacFuelBalancer : PartModule
    {
        // Kept for future expansion.
        //public override void OnAwake()
        //{
        //    base.OnAwake();
        //    Log.dbg("TAC Fuel Balancer [" + this.GetInstanceID().ToString("X") + "][" + Time.time + "]: OnAwake");
        //}

        //public override void OnLoad(ConfigNode node)
        //{
        //    base.OnLoad(node);
        //    Log.dbg("TAC Fuel Balancer [" + this.GetInstanceID().ToString("X") + "][" + Time.time + "]: OnLoad");
        //}

        //public override void OnSave(ConfigNode node)
        //{
        //    base.OnSave(node);
        //    Log.dbg("TAC Fuel Balancer [" + this.GetInstanceID().ToString("X") + "][" + Time.time + "]: OnSave");
        //}

        //public override void OnStart(PartModule.StartState state)
        //{
        //    base.OnStart(state);
        //    Log.dbg("TAC Fuel Balancer [" + this.GetInstanceID().ToString("X") + "][" + Time.time + "]: OnStart: " + state);

        //    if (state != StartState.Editor)
        //    {
        //        vessel.OnJustAboutToBeDestroyed += CleanUp;
        //        part.OnJustAboutToBeDestroyed += CleanUp;
        //    }
        //}

        //public override void OnUpdate()
        //{
        //    base.OnUpdate();
        //}

        //private void CleanUp()
        //{
        //    Log.dbg("TAC Fuel Balancer [" + this.GetInstanceID().ToString("X") + "][" + Time.time + "]: CleanUp");
        //}

        //[KSPEvent(guiActive = true, guiName = "Show Fuel Balancer", active = true)]
        //public void ShowFuelBalancerWindow()
        //{
        //    mainWindow.SetVisible(true);
        //}

        //[KSPEvent(guiActive = true, guiName = "Hide Fuel Balancer", active = false)]
        //public void HideFuelBalancerWindow()
        //{
        //    mainWindow.SetVisible(false);
        //}

        //[KSPAction("Toggle Fuel Balancer")]
        //public void ToggleFuelBalancerWindow(KSPActionParam param)
        //{
        //    mainWindow.SetVisible(!mainWindow.IsVisible());
        //}
    }

    enum TransferDirection
    {
        NONE = 0,
        IN,
        OUT,
        BALANCE,
        DUMP,
        LOCKED,
        VARIOUS // used by UI when multiple parts with varying directions are selected
    }

    abstract class PartResourceInfo
    {
        public abstract void Refresh(Part part);

        public abstract double Amount { get; }
        public abstract bool Locked { get; set; }
        public double PercentFull
        {
            get { return Amount / MaxAmount; }
        }
        public abstract ResourceTransferMode TransferMode { get; }
        public abstract double MaxAmount { get; }
        
        public void SetAmount(double amount)
        {
            if(amount < 0 || amount > MaxAmount) throw new ArgumentOutOfRangeException();
            SetAmountCore(amount);
        }
        
        /// <summary>Attempts to transfer the given amount of resource to another <see cref="PartResourceInfo"/> object,
        /// which must be of the same type.
        /// </summary>
        /// <returns>Returns the amount actually transferred, which may be less if the destination became full or
        /// otherwise cannot accept more resources, or the source doesn't have enough.
        /// </returns>
        public abstract double TransferTo(PartResourceInfo destination, double amount);
        
        protected abstract void SetAmountCore(double amount);
    }
    
    sealed class SimplePartResource : PartResourceInfo
    {
        public SimplePartResource(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public override double Amount
        {
            get { return resource.amount; }
        }

        public override bool Locked
        {
            get { return !resource.flowState; }
            set { resource.flowState = !value; }
        }
        
        public override double MaxAmount
        {
            get { return resource.maxAmount; }
        }

        public override ResourceTransferMode TransferMode
        {
            get { return resource.info.resourceTransferMode; }
        }
        
        public override void Refresh(Part part)
        {
            // we can't just save the PartResource in the constructor because the StretchyTanks mod might change it
            resource = part.Resources[resourceName];
        }
        
        public override double TransferTo(PartResourceInfo destination, double amount)
        {
            SimplePartResource dest = (SimplePartResource)destination;
            amount = Math.Min(Math.Min(amount, Amount), dest.MaxAmount - dest.Amount);
            dest.resource.amount += amount;
            resource.amount -= amount;
            return amount;
        }
        
        protected override void SetAmountCore(double amount)
        {
            resource.amount = amount;
        }
        
        readonly string resourceName;
        PartResource resource;
    }
    
    sealed class RocketFuelResource : PartResourceInfo
    {
        public override double Amount
        {
            get { return Math.Min(liquidFuel.amount*(10d/9), oxidizer.amount*(10d/11)); }
        }
        
        public override bool Locked
        {
            get { return !liquidFuel.flowState || !oxidizer.flowState; }
            set { liquidFuel.flowState = oxidizer.flowState = !value; }
        }
        
        public override double MaxAmount
        {
            get { return Math.Min(liquidFuel.maxAmount*(10d/9), oxidizer.maxAmount*(10d/11)); }
        }
       
        public override ResourceTransferMode TransferMode
        {
            get { return liquidFuel.info.resourceTransferMode; } // assume both have the same transfer mode
        }
        
        public override void Refresh(Part part)
        {
            liquidFuel = part.Resources["LiquidFuel"];
            oxidizer   = part.Resources["Oxidizer"];
        }
        
        public override double TransferTo(PartResourceInfo destination, double amount)
        {
            RocketFuelResource dest = (RocketFuelResource)destination;
            double liquidSpace = dest.liquidFuel.maxAmount-dest.liquidFuel.amount, oxidizerSpace = dest.oxidizer.maxAmount-dest.oxidizer.amount;
            amount = Math.Min(Math.Min(amount, Amount), Math.Min(liquidSpace*(10d/9), oxidizerSpace*(10d/11)));
            double fuel = amount*(9d/10), oxygen = amount*(11d/10);
            dest.liquidFuel.amount += fuel;
            liquidFuel.amount      -= fuel;
            dest.oxidizer.amount   += oxygen;
            oxidizer.amount        -= oxygen;
            return amount;
        }
        
        protected override void SetAmountCore(double amount)
        {
            liquidFuel.amount = amount*(9d/10);
            oxidizer.amount   = amount*(11d/10);
        }
        
        PartResource liquidFuel, oxidizer;
    }
    
    sealed class ResourcePartMap
    {
        public readonly PartResourceInfo resource;
        public readonly Part part;
        public readonly int shipId;
        public TransferDirection direction = TransferDirection.NONE;
        public bool isHighlighted;
        public bool isSelected;

        public ResourcePartMap(PartResourceInfo resource, Part part, int shipId)
        {
            this.resource = resource;
            this.part = part;
            this.shipId = shipId;
        }
    }

    sealed class ResourceInfo
    {
        public ResourceInfo(string title)
        {
            this.title = title;
        }
        
        public readonly List<ResourcePartMap> parts = new List<ResourcePartMap>();
        public readonly string title;
        public bool balance = false;
        public bool isShowing = false;
    }
}
