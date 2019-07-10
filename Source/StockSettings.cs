using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using KSP.Localization;

namespace Tac
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings
    // HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().


    public class TacSettings_1 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Options"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Tac Fuel Balancer"; } }
        public override string DisplaySection { get { return "Tac Fuel Balancer"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }



        [GameParameters.CustomParameterUI("Auto-deselect Tabs")]
        public bool OneTabOnly = true;

        [GameParameters.CustomParameterUI("Show Ship Number")]
        public bool ShowShipNumber = true;

        [GameParameters.CustomParameterUI("Show Stage Number")]
        public bool ShowStageNumber = true;

        [GameParameters.CustomParameterUI("Show Maximum Amount")]
        public bool ShowMaxAmount = true;

        [GameParameters.CustomParameterUI("Show Current Amount")]
        public bool ShowCurrentAmount = true;

        [GameParameters.CustomParameterUI("Show Percent Full")]
        public bool ShowPercentFull = true;

        [GameParameters.CustomParameterUI("Show Dump")]
        public bool ShowDump = true;

        [GameParameters.CustomParameterUI("Show Toggles")]
        public bool ShowToggles = true;

        [GameParameters.CustomParameterUI("Show Tooltips")]
        public bool ShowTooltips = true;

        [GameParameters.CustomParameterUI("Balance In's")]
        public bool BalanceIn = false;

        [GameParameters.CustomParameterUI("Balance Out's")]
        public bool BalanceOut = false;

     



        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {

        }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            // if (member.Name == "DefaultSettings" && DefaultSettings)
            //SetDifficultyPreset(parameters.preset);


            return true;
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }

    public class TacSettings_2 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "General"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Tac Fuel Balancer"; } }
        public override string DisplaySection { get { return "Tac Fuel Balancer"; } }
        public override int SectionOrder { get { return 2; } }
        public override bool HasPresets { get { return false; } }


        [GameParameters.CustomFloatParameterUI("Maximum Fuel Flow Rate", minValue = 0.1f, maxValue = 30.0f, asPercentage = false, displayFormat = "0.0")]
        public float MaxFuelFlow = 10f;

        [GameParameters.CustomFloatParameterUI("Rate Multiplier", minValue = 0.1f, maxValue = 100f, asPercentage = false, displayFormat = "0.0")]
        public float RateMultiplier = 1f;

        [GameParameters.CustomFloatParameterUI("Fuel Warning Level", minValue = 0.0f, maxValue = 99f, asPercentage = false, displayFormat = "0.0")]
        public float FuelWarningLevel = 25f;

        [GameParameters.CustomFloatParameterUI("Fuel Critical Level", minValue = 0.2f, maxValue = 5.0f, asPercentage = false, displayFormat = "0.0")]
        public float FuelCriticalLevel = 5f;


        [GameParameters.CustomParameterUI("Rate Multiplier")]
        public string RateMultiplerStr = "1";

        static List<string> rateMultiplierList = null;

        public override IList ValidValues(MemberInfo member)
        {
            if (member.Name == "RateMultiplerStr")
            {
                if (rateMultiplierList == null)
                {
                    rateMultiplierList = new List<string>();
                    rateMultiplierList.Add("0.1");
                    rateMultiplierList.Add("1");
                    rateMultiplierList.Add("10");
                    rateMultiplierList.Add("100");
                }
                return rateMultiplierList;
            }
            else
            {
                return null;
            }
        }


        public override void SetDifficultyPreset(GameParameters.Preset preset)        {         }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            RateMultiplier = (float)Double.Parse(RateMultiplerStr);
            if (HighLogic.CurrentGame != null)
            {
                if (FuelBalanceController.settings != null)
                    FuelBalanceController.settings.RateMultiplier = RateMultiplier;
            }
            return (member.Name != "RateMultiplier");
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

     
    }

    public class TacSettings_3 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Misc"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Tac Fuel Balancer"; } }
        public override string DisplaySection { get { return "Tac Fuel Balancer"; } }
        public override int SectionOrder { get { return 3; } }
        public override bool HasPresets { get { return false; } }



        [GameParameters.CustomParameterUI("Use KSP skin")]
        public bool useKSPskin = true;

        [GameParameters.CustomParameterUI("Disable old settings page")]
        public bool disableOldSettings = false;

        [GameParameters.CustomParameterUI("Debug Mode")]
        public bool Debug = false;


        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {

        }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }
}
