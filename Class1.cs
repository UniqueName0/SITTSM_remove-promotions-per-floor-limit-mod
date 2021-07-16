using BepInEx;
using HarmonyLib;
using UnityEngine;
using System;
using System.Reflection;
using HarmonyLib.Tools;
using System.Globalization;
using TMPro;
using System.Collections.Generic;

namespace remove_sleep
{
    [BepInProcess("Stick It To The (Stick) Man.exe")]
    [BepInPlugin("uniquename.sittsm.remove-promotion-limit", "remove-promotion-limit", "0.0.0.0")]
    public class remove_sleep_effect : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("uniquename.sittsm.remove-promotion-limit");
            harmony.PatchAll();
        }

        public const string modID = "uniquename.sittsm.remove-promotion-limit";
        public const string modName = "remove-promotion-limit";
    }

    [HarmonyPatch(typeof(SettingsScreen), "HandlePromotionsPerFloorButtonClicked")]
    public class remove_sleep_Patch
    {
        public static bool Prefix(SettingsScreen __instance, ref CustomGameData ____customGameData, ref TMP_Text ____promotionsPerFloorText)
        {
            float num = ____customGameData.promotionsPerFloor;
            num += (float)0.05;
            ____customGameData.promotionsPerFloor = num;
            CustomGameManager.SaveCustomGameData(____customGameData);
            ____promotionsPerFloorText.text = num.ToString("#0.##", CultureInfo.InvariantCulture);
            return false;
        }
    }
    [HarmonyPatch(typeof(SettingsScreen), "Setup")]
    public class yes_Patch
    {
        public static void Postfix(CustomGameData customGameData, ref TMP_Text ____promotionsPerFloorText)
        {
            customGameData.promotionsPerFloor = 0f;
            ____promotionsPerFloorText.text = customGameData.promotionsPerFloor.ToString();
        }
    }
}