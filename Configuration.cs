using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ieishi.mod.yotogimichi
{
    public class Configuration
    {
        private ConfigEntry<bool> _configDOFFix;
        private ConfigEntry<float> _configMaxZoom;
        private ConfigEntry<bool> _configShowLovePointsInConsole;
        private ConfigEntry<bool> _configUndressUnderwareInPublic;
        private ConfigEntry<bool> _configCumBeforeConfession;
        private ConfigEntry<bool> _configRoundBellyWithoutPenetration;
        private ConfigEntry<bool> _configFromBeginning_AllWaypoints;
        private ConfigEntry<bool> _configFromBeginning_Shops;
        private ConfigEntry<bool> _configFromBeginning_AllCloths;
        private ConfigEntry<bool> _configFromBeginning_Work;
        private ConfigEntry<bool> _configFromBeginning_Travel;
        private ConfigEntry<bool> _configFromBeginning_MultipleH;
        private ConfigEntry<bool> _configFromBeginning_ShowGirlStats;
        private ConfigEntry<bool> _configFromBeginning_AllPositions;
        private ConfigEntry<bool> _configFromBeginning_AllSkills;

        public Configuration()
        {
            ConfigFile file = Plugin.Instance.Config;

            _configDOFFix = file.Bind("General",
                "DOFFix",
                true,
                "Fixes wrong DOF Distance");

            _configMaxZoom = file.Bind("General",
                "MaxZoom",
                1f,
                "change max zoom distance. e.g. 1.3, 3 ");

            _configShowLovePointsInConsole = file.Bind("General",
                "ShowLovePointsInConsole",
                false,
                "Shows lovepoints in console/logfile after each talk");

            _configUndressUnderwareInPublic = file.Bind("General",      // The section under which the option is shown
                 "UndressUnderwareInPublic",  // The key of the configuration option in the configuration file
                 false, // The default value
                 "Enable undress button for underware"); // Description of the option 

            _configCumBeforeConfession = file.Bind("General",
                "CumBeforeConfession",
                false,
                "Let her cum before confession. Lovepoints must be >800");

            _configRoundBellyWithoutPenetration = file.Bind("General",
                "RoundBellyWithoutPenetration",
                false,
                "Make belly round with cum in entrace without full penetration");

            _configFromBeginning_AllWaypoints = file.Bind("AllowFromStart",
                "AllWaypoints",
                false,
                "Eanble all waypoints");

            _configFromBeginning_AllCloths = file.Bind("AllowFromStart",
                "AllCloths",
                false,
                "Get all cloths. Cloths will be saved by game once you save!");

            _configFromBeginning_Shops = file.Bind("AllowFromStart",
                "Shops",
                false,
                "Enable all shops");

            _configFromBeginning_Work = file.Bind("AllowFromStart",
                "Work",
                false,
                "Enables to work");

            _configFromBeginning_Travel = file.Bind("AllowFromStart",
                "Travel",
                false,
                "Enables to travel");

            _configFromBeginning_MultipleH = file.Bind("AllowFromStart",
                "MultipleH",
                false,
                "H more then once a day");

            _configFromBeginning_ShowGirlStats = file.Bind("AllowFromStart",
                "ShowGirlStats",
                false,
                "Show girl stats in notes from beginning");

            _configFromBeginning_AllPositions = file.Bind("AllowFromStart",
                "AllPositions",
                false,
                "Enables all positions");

            _configFromBeginning_AllSkills = file.Bind("AllowFromStart",
                "AllSkills",
                false,
                "Enables all skills");
        }


        public bool DOF_Fix => _configDOFFix.Value;
        public float MaxZoom => _configMaxZoom.Value;
        public bool ShowLovePointsInConsole => _configShowLovePointsInConsole.Value;
        public bool UndressUnderwareInPublic => _configUndressUnderwareInPublic.Value;
        public bool CumBeforeConfession => _configCumBeforeConfession.Value;
        public bool RoundBellyWithoutPenetration => _configRoundBellyWithoutPenetration.Value;
        public bool FromBeginning_AllWaypoints => _configFromBeginning_AllWaypoints.Value;
        public bool FromBeginning_Shops => _configFromBeginning_Shops.Value;
        public bool FromBeginning_AllCloths => _configFromBeginning_AllCloths.Value;
        public bool FromBeginning_Work => _configFromBeginning_Work.Value;
        public bool FromBeginning_Travel => _configFromBeginning_Travel.Value;
        public bool FromBeginning_MultipleH => _configFromBeginning_MultipleH.Value;
        public bool FromBeginning_ShowGirlStats => _configFromBeginning_ShowGirlStats.Value;
        public bool FromBeginning_AllPositions => _configFromBeginning_AllPositions.Value;
        public bool FromBeginning_AllSkills => _configFromBeginning_AllSkills.Value;
    }
}
