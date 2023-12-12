using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Linq;

namespace no00ob.Mod.LethalCompany.SimpleSettings
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, SimpleSettingsMod.PluginInfo.PLUGIN_VERSION)]
    public class SimpleSettingsBase : BaseUnityPlugin
    {
        private const string PLUGIN_GUID = "no00ob.SimpleSettings";
        private const string PLUGIN_NAME = "Simple Settings";
        //private const string PLUGIN_VERSION = "1.0.0";

        private ConfigEntry<int> configResolutionX;
        private ConfigEntry<int> configResolutionY;
        private ConfigEntry<int> configFullscreen;
        //private ConfigEntry<RefreshRate> configRefreshRate;

        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        public static SimpleSettingsBase Instance;

        internal ManualLogSource logger;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            configResolutionX = Config.Bind("Display", "Resolution Width", Screen.currentResolution.width, "The desired horizontal screen resolution.");
            configResolutionY = Config.Bind("Display", "Resolution Height", Screen.currentResolution.height, "The desired vertical screen resolution.");
            configFullscreen = Config.Bind("Display", "Fullscreen Mode", 1, "0 = Windowed, 1 = Borderless, 2 = Exclusive");
            //configRefreshRate = Config.Bind("Display", "Refresh Rate", Screen.currentResolution.refreshRateRatio, "The desired screen refresh rate.");

            logger = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            logger.LogInfo($"Plugin {PLUGIN_GUID} is loaded!");
        }

        private void Start()
        {
            FullScreenMode screenMode = FullScreenMode.FullScreenWindow;

            if (configFullscreen.Value < 1)
            {
                screenMode = FullScreenMode.Windowed;
            }
            if (configFullscreen.Value == 1)
            {
                screenMode = FullScreenMode.FullScreenWindow;
            }
            if (configFullscreen.Value > 1)
            {
                screenMode = FullScreenMode.ExclusiveFullScreen;
            }

            Screen.SetResolution(configResolutionX.Value, configResolutionY.Value, screenMode);
        }
    }
}
