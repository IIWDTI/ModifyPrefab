using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

public class ModifyPrefab : IModApi
{
	public void InitMod(Mod mod)
	{
		Log.Out("PrefabPatcher: " + base.GetType().ToString());
		new Harmony(base.GetType().ToString()).PatchAll(Assembly.GetExecutingAssembly());
	}

	//Makes the game believe its in EditMode
	[HarmonyPatch(typeof(GameManager))]
	[HarmonyPatch("IsEditMode")]
	public class GameManager_IsEditMode
	{
		public static void Postfix(ref bool __result)
		{
			//If the file funkymode.txt is found the game runs in full EditMode where the graphics looks funky.
			if (!__result)
			{
				if (File.Exists(Environment.CurrentDirectory + "\\Mods\\ModifyPrefab\\funkymode.txt"))
				{
					__result = true;
					return;
				}
				__result = GamePrefs.GetBool(EnumGamePrefs.DebugMenuEnabled);
			}
		}
	}

	//Removes some unneeded checks
	[HarmonyPatch(typeof(MultiBlockManager))]
	[HarmonyPatch("DoCurrentModeSanityChecks")]
	public class MultiBlockManager_DoCurrentModeSanityChecks
	{
		public static bool Prefix()
		{
			return false;
		}
	}

	//Removes Pause in singleplayer mode, causing problems
	[HarmonyPatch(typeof(GameManager))]
	[HarmonyPatch("Pause")]
	public class GameManager_Pause
	{
		public static bool Prefix()
		{
			return false;
		}
	}
}
