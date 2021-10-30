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
using Log = TacFuelBalancer.Log;

namespace Tac
{
    class Settings
    {
        public double MaxFuelFlow { get; set; }
        public double RateMultiplier { get; set; }
        public double FuelWarningLevel { get; set; }
        public double FuelCriticalLevel { get; set; }

        public bool OneTabOnly { get; set; }
        public bool ShowShipNumber { get; set; }
        public bool ShowStageNumber { get; set; }
        public bool ShowMaxAmount { get; set; }
        public bool ShowCurrentAmount { get; set; }
        public bool ShowPercentFull { get; set; }
        public bool ShowDump { get; set; }
        public bool ShowToggles { get; set; }
        public bool ShowTooltips { get; set; }

        public bool BalanceIn { get; set; }
        public bool BalanceOut { get; set; }

        public bool Debug { get; set; }

        public Settings()
        {
            Log.dbg("Settings");
            MaxFuelFlow = 10.0;
            RateMultiplier = 1.0;
            FuelWarningLevel = 25.0;
            FuelCriticalLevel = 5.0;

            ShowShipNumber = true;
            ShowStageNumber = true;
            ShowMaxAmount = true;
            ShowCurrentAmount = true;
            ShowPercentFull = true;
            ShowDump = true;
            OneTabOnly = true;
            ShowToggles = true;
            ShowTooltips = true;
            BalanceIn = false;
            BalanceOut = false;

            Debug = false;

            LoadFromStock();
        }

        public void LoadFromStock()
        {
            Log.dbg("LoadFromStock");
            if (HighLogic.CurrentGame != null)
            {
                MaxFuelFlow = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().MaxFuelFlow;
                RateMultiplier = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().RateMultiplier;
                FuelWarningLevel = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().FuelWarningLevel;
                FuelCriticalLevel = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().FuelCriticalLevel;

                ShowShipNumber = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowShipNumber;
                ShowStageNumber = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowStageNumber;
                ShowMaxAmount = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowMaxAmount;
                ShowCurrentAmount = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowCurrentAmount;
                ShowPercentFull = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowPercentFull;
                ShowDump = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowDump;
                OneTabOnly = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().OneTabOnly;
                ShowToggles = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowToggles;
                ShowTooltips = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowTooltips;
                BalanceIn = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().BalanceIn;
                BalanceOut = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().BalanceOut;

                Debug = HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().Debug;
            }
        }


        public void Load(ConfigNode config)
        {
            MaxFuelFlow = Utilities.GetValue(config, "MaxFuelFlow", MaxFuelFlow);
            RateMultiplier = Utilities.GetValue(config, "RateMultiplier", RateMultiplier);
            FuelWarningLevel = Utilities.GetValue(config, "FuelWarningLevel", FuelWarningLevel);
            FuelCriticalLevel = Utilities.GetValue(config, "FuelCriticalLevel", FuelCriticalLevel);

            OneTabOnly = Utilities.GetValue(config, "OneTabOnly", OneTabOnly);
            ShowShipNumber = Utilities.GetValue(config, "ShowShipNumber", ShowShipNumber);
            ShowStageNumber = Utilities.GetValue(config, "ShowStageNumber", ShowStageNumber);
            ShowMaxAmount = Utilities.GetValue(config, "ShowMaxAmount", ShowMaxAmount);
            ShowCurrentAmount = Utilities.GetValue(config, "ShowCurrentAmount", ShowCurrentAmount);
            ShowPercentFull = Utilities.GetValue(config, "ShowPercentFull", ShowPercentFull);
            ShowDump = Utilities.GetValue(config, "ShowDump", ShowDump);
            ShowToggles = Utilities.GetValue(config, "ShowToggles", ShowToggles);
            ShowTooltips = Utilities.GetValue(config, "ShowTooltips", ShowTooltips);

            BalanceIn = Utilities.GetValue(config, "BalanceIn", BalanceIn);
            BalanceOut = Utilities.GetValue(config, "BalanceOut", BalanceOut);

            Debug = Utilities.GetValue(config, "Debug", Debug);

            SaveToStock();
        }

        public void SaveToStock()
        {
            if (HighLogic.CurrentGame != null)
            {
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().MaxFuelFlow = (float)MaxFuelFlow;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().RateMultiplier = (float)RateMultiplier;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().RateMultiplerStr = RateMultiplier.ToString();
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().FuelWarningLevel = (float)FuelWarningLevel;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_2>().FuelCriticalLevel = (float)FuelCriticalLevel;

                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowShipNumber = ShowShipNumber;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowStageNumber = ShowStageNumber;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowMaxAmount = ShowMaxAmount;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowCurrentAmount = ShowCurrentAmount;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowPercentFull = ShowPercentFull;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowDump = ShowDump;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().OneTabOnly = OneTabOnly;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowToggles = ShowToggles;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().ShowTooltips = ShowTooltips;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().BalanceIn = BalanceIn;
                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_1>().BalanceOut = BalanceOut;

                HighLogic.CurrentGame.Parameters.CustomParams<TacSettings_3>().Debug = Debug;
            }
        }

        public void Save(ConfigNode config)
        {
            SaveToStock();
            config.AddValue("MaxFuelFlow", MaxFuelFlow);
            config.AddValue("RateMultiplier", RateMultiplier);
            config.AddValue("FuelWarningLevel", FuelWarningLevel);
            config.AddValue("FuelCriticalLevel", FuelCriticalLevel);

            config.AddValue("OneTabOnly", OneTabOnly);
            config.AddValue("ShowShipNumber", ShowShipNumber);
            config.AddValue("ShowStageNumber", ShowStageNumber);
            config.AddValue("ShowMaxAmount", ShowMaxAmount);
            config.AddValue("ShowCurrentAmount", ShowCurrentAmount);
            config.AddValue("ShowPercentFull", ShowPercentFull);
            config.AddValue("ShowDump", ShowDump);

            config.AddValue("BalanceIn", BalanceIn);
            config.AddValue("BalanceOut", BalanceOut);

            config.AddValue("Debug", Debug);
        }
    }
}
