/*
	This file is part of Thunder Aerospace Corporation's (TAC) Fuel Balancer /L Unleashed
		© 2019-2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2019 linuxgurugamer
		© 2016-2018 thwebbooth
		© 2013-2015 Taranis Elsu

	TAC Fuel Balancer /L Unleashed is licensed as follows:

		* CC BY-NC-SA 3.0 : http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode

	TAC Fuel Balancer /L Unleashed is distributed in the hope that
	it will be useful, but WITHOUT ANY WARRANTY; without even the implied
	warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

*/
#if false
using System;
using System.Linq;
using UnityEngine;

namespace Tac
{
    class SettingsWindow : Window<TacFuelBalancer>
    {
        private readonly Settings settings;
        private readonly string version;

        private GUIStyle labelStyle;
        private GUIStyle editStyle;
        private GUIStyle versionStyle;

        public SettingsWindow(Settings settings)
            : base("TAC Fuel Balancer Settings", 240, 360)
        {
            Log.dbg("SettingsWindow");
            this.settings = settings;
            version = Utilities.GetDllVersion(this);
        }

        protected override void ConfigureStyles()
        {
            base.ConfigureStyles();

            if (labelStyle == null)
            {
                Log.dbg("SettingsWindow.ConfigureStyles");
                FuelBalanceController.settingsWindow.WindowClosed += OnWindowClosed;

                labelStyle = new GUIStyle(_skin.label);
                labelStyle.wordWrap = false;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.normal.textColor = Color.white;

                editStyle = new GUIStyle(_skin.textField);

                versionStyle = Utilities.GetVersionStyle();
            }
        }

        protected override void DrawWindowContents(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Maximum Fuel Flow Rate", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.MaxFuelFlow = Utilities.ShowTextField(settings.MaxFuelFlow, 10, editStyle, GUILayout.MinWidth(50));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Fuel Warning Level", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.FuelWarningLevel = Utilities.ShowTextField(settings.FuelWarningLevel, 10, editStyle, GUILayout.MinWidth(50));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Fuel Critical Level", labelStyle, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            settings.FuelCriticalLevel = Utilities.ShowTextField(settings.FuelCriticalLevel, 10, editStyle, GUILayout.MinWidth(50));
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            settings.OneTabOnly = GUILayout.Toggle(settings.OneTabOnly, "Auto-deselect Tabs");
            settings.ShowShipNumber = GUILayout.Toggle(settings.ShowShipNumber, "Show Ship Number");
            settings.ShowStageNumber = GUILayout.Toggle(settings.ShowStageNumber, "Show Stage Number");
            settings.ShowMaxAmount = GUILayout.Toggle(settings.ShowMaxAmount, "Show Maximum Amount");
            settings.ShowCurrentAmount = GUILayout.Toggle(settings.ShowCurrentAmount, "Show Current Amount");
            settings.ShowPercentFull = GUILayout.Toggle(settings.ShowPercentFull, "Show Percent Full");
            settings.ShowDump = GUILayout.Toggle(settings.ShowDump, "Show Dump in popup-menu");

            GUILayout.Space(10);

            settings.ShowToggles = GUILayout.Toggle(settings.ShowToggles, "Show Toggles");
            settings.ShowTooltips = GUILayout.Toggle(settings.ShowTooltips, "Show Tooltips");

            GUILayout.Space(10);

            settings.BalanceIn = GUILayout.Toggle(settings.BalanceIn, "Balance In's");
            settings.BalanceOut = GUILayout.Toggle(settings.BalanceOut, "Balance Out's");

            GUILayout.EndVertical();

            GUILayout.Space(4);
            GUI.Label(new Rect(4, windowPos.height - 13, windowPos.width - 20, 12), "TAC Fuel Balancer v" + version, versionStyle);
        }


        private void OnWindowClosed(object sender, EventArgs e)
        {
            Log.dbg("SettingsWindow.OnWindowClosed");
            settings.SaveToStock();
            FuelBalanceController.settingsWindow.WindowClosed -= OnWindowClosed;
        }
    }
}
 #endif