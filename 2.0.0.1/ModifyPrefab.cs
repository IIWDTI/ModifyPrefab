using HarmonyLib;
using HarmonyLib.Public.Patching;
using HarmonyLib.Tools;
using System;
using System.IO;
using System.Reflection;
using InControl;
using Platform;
using UnityEngine;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Reflection.Emit;
using DynamicMusic;
using System.Linq;




public class ModifyPrefab : IModApi
{

    public void InitMod(Mod mod)
    {
        new Harmony(this.GetType().ToString()).PatchAll(Assembly.GetExecutingAssembly());
    }

    public ModifyPrefab() {

       BlockToolSelection blockToolSelection = BlockToolSelection.Instance;
        var guiActions = blockToolSelection.GetActions();
        foreach (var action in guiActions)
        {
            
            if(action.Value.text == Localization.Get("selectionToolsEditBlocksVolume"))
            {
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));         
            }
            else if (action.Value.text == Localization.Get("selectionToolsCopySleeperVolume"))
            {
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));
            }
            else if (action.Value.text == Localization.Get("selectionToolsCopyAirBlocks"))
            {
                action.Value.SetIsVisibleDelegate(new NGuiAction.IsVisibleDelegate(GetDebugAsStatic));
            }
            else if (action.Value.text == Localization.Get("selectionToolsClearSelection"))
            {
                action.Value.SetIsEnabledDelegate((NGuiAction.IsEnabledDelegate)(() => GetDebugAsStatic() && blockToolSelection.SelectionActive));
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));
            }
            else if (action.Value.text == Localization.Get("selectionToolsFillSelection"))
            {
                action.Value.SetIsEnabledDelegate((NGuiAction.IsEnabledDelegate)(() => GetDebugAsStatic() && blockToolSelection.SelectionActive));
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));
            }
            else if (action.Value.text == Localization.Get("selectionToolsRandomFillSelection"))
            {
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));
            }
            else if (action.Value.text == Localization.Get("selectionToolsUndo"))
            {
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));
            }
            else if (action.Value.text == Localization.Get("selectionToolsRedo"))
            {
                action.Value.SetIsVisibleDelegate((NGuiAction.IsVisibleDelegate)(() => GetDebugAsStatic()));
            }
        }

        
    }

    [HarmonyPatch(typeof(PlayerMoveController))]
    [HarmonyPatch(nameof(PlayerMoveController.Update))]
    class PlayerMoveController_Update
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;
                        
                    }

                }

                yield return instruction;

            }

        }

    }


    [HarmonyPatch(typeof(PlayerMoveController))]
    [HarmonyPatch(nameof(PlayerMoveController.updateDebugKeys))]
    class PlayerMoveController_updateDebugKeys
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;

                    }

                }

                yield return instruction;

            }

        }

    }



    [HarmonyPatch(typeof(PlayerMoveController))]
    [HarmonyPatch(nameof(PlayerMoveController.OnGUI))]
    class PlayerMoveController_OnGUI
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;

                    }

                }

                yield return instruction;

            }

        }

    }


    [HarmonyPatch(typeof(BlockToolSelection))]
    [HarmonyPatch(nameof(BlockToolSelection.CheckSpecialKeys))]
        class BlockToolSelection_CheckSpecialKeys
        {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {

                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;

                    }

                }

                yield return instruction;

            }

        }

        }

    [HarmonyPatch(typeof(BlockToolSelection))]
    [HarmonyPatch(nameof(BlockToolSelection.CheckKeys))]

    class BlockToolSelection_CheckKeys
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()") || Convert.ToString(instruction.operand).Contains("IsEditor()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;
                       

                    }

                }

                yield return instruction;

            }

        }

    }

    [HarmonyPatch(typeof(BlockToolSelection))]
    [HarmonyPatch(nameof(BlockToolSelection.ExecuteUseAction))]

    class BlockToolSelection_ExecuteUseAction
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;

                    }

                }

                yield return instruction;

            }

        }

    }

    [HarmonyPatch(typeof(BlockToolSelection))]
    [HarmonyPatch(nameof(BlockToolSelection.ExecuteAttackAction))]

    class BlockToolSelection_ExecuteAttackAction
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;

                    }

                }

                yield return instruction;

            }

        }

    }

    [HarmonyPatch(typeof(BlockToolSelection))]
    [HarmonyPatch(nameof(BlockToolSelection.decInventoryLater))]

    class BlockToolSelection_decInventoryLater
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditMode()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;

                    }

                }

                yield return instruction;

            }

        }

    }

    [HarmonyPatch(typeof(BlockToolSelection))]
    [HarmonyPatch(nameof(BlockToolSelection.RotateFocusedBlock))]
    class BlockToolSelection_RotateFocusedBlock
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> MyTranspiler(IEnumerable<CodeInstruction> instructions)
        {


            var getmethod = AccessTools.Method(typeof(ModifyPrefab), "GetDebug") as MethodBase;

            foreach (var instruction in instructions)
            {

                if (instruction.opcode != null || instruction.operand != null)
                {

                    if (instruction.opcode == OpCodes.Callvirt && Convert.ToString(instruction.operand).Contains("IsEditor()"))
                    {
                        instruction.opcode = OpCodes.Callvirt;
                        instruction.operand = getmethod;
                    }

                }

                yield return instruction;

            }

        }

    }

    [HarmonyPatch(typeof(NGuiWdwDebugPanels))]
    [HarmonyPatch("Awake")]
    public class NGuiWdwDebugPanels_Awake
    {
        public static void Postfix(ref NGuiWdwDebugPanels __instance)
        {
            string _enabledPanels = ",Ge,Fo,Pr,Ply,Sp,Ch,Ca,Ne,Se,St,Plx,Te";
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Player", "Ply", new Func<int, int, int>(__instance.showDebugPanel_Player), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("General", "Ge", new Func<int, int, int>(__instance.showDebugPanel_General), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Spawning", "Sp", new Func<int, int, int>(__instance.showDebugPanel_Spawning), _enabledPanels, SingletonMonoBehaviour<ConnectionManager>.Instance.IsServer));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Chunk", "Ch", new Func<int, int, int>(__instance.showDebugPanel_Chunk), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Cache", "Ca", new Func<int, int, int>(__instance.showDebugPanel_Cache), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Focused Block", "Fo", new Func<int, int, int>(__instance.showDebugPanel_FocusedBlock), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Network", "Ne", new Func<int, int, int>(__instance.showDebugPanel_Network), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Selection", "Se", new Func<int, int, int>(__instance.showDebugPanel_Selection), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Prefab", "Pr", new Func<int, int, int>(__instance.showDebugPanel_Prefab), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Stealth", "St", new Func<int, int, int>(__instance.showDebugPanel_Stealth), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Player Extended - Buffs and CVars", "Plx", new Func<int, int, int>(__instance.showDebugPanel_PlayerEffectInfo), _enabledPanels));
            __instance.Panels.Add(new NGuiWdwDebugPanels.PanelDefinition("Texture", "Te", new Func<int, int, int>(__instance.showDebugPanel_Texture), _enabledPanels));
        }

    }


    [HarmonyPatch(typeof(PlayerMoveController))]
    [HarmonyPatch("Init")]
    public class PlayerMoveController_Init
    {
        public static void Postfix()
        {
            PlayerMoveController playerMoveController = PlayerMoveController.Instance;
            NGuiAction.IsEnabledDelegate menuIsEnabled = () => !XUiC_SpawnSelectionWindow.IsOpenInUI(LocalPlayerUI.primaryUI) && playerMoveController.gameManager.gameStateManager.IsGameStarted() && GameStats.GetInt(EnumGameStats.GameState) == 1 && !LocalPlayerUI.primaryUI.windowManager.IsModalWindowOpen() && !playerMoveController.windowManager.IsFullHUDDisabled();
            NGuiAction NguiSelectionMode = playerMoveController.globalActions.Find(x => x.text == "SelectionMode");
            NguiSelectionMode.SetClickActionDelegate(delegate
            {
                if (InputUtils.AltKeyPressed)
                {
                    GamePrefs.Set(EnumGamePrefs.SelectionOperationMode, 4);
                    return;
                }
                if (InputUtils.ShiftKeyPressed)
                {
                    GamePrefs.Set(EnumGamePrefs.SelectionOperationMode, 2);
                    GameManager.Instance.SetCursorEnabledOverride(true, true);
                    return;
                }
                if (InputUtils.ControlKeyPressed && GetDebugAsStatic())
                {
                    GamePrefs.Set(EnumGamePrefs.SelectionOperationMode, 3);
                    GameManager.Instance.SetCursorEnabledOverride(true, true);
                    return;
                }
                GamePrefs.Set(EnumGamePrefs.SelectionOperationMode, 1);
                GameManager.Instance.SetCursorEnabledOverride(true, true);
            });


            NGuiAction NguiPrefab = playerMoveController.globalActions.Find(x => x.text == "Prefab");
            NguiPrefab.SetIsEnabledDelegate(() => menuIsEnabled() && GetDebugAsStatic());


            NGuiAction NguiDetachCamera = playerMoveController.globalActions.Find(x => x.text == "DetachCamera");
            NguiDetachCamera.SetIsEnabledDelegate(() => playerMoveController.gameManager.gameStateManager.IsGameStarted() && GameStats.GetInt(EnumGameStats.GameState) == 1 && !playerMoveController.entityPlayerLocal.AimingGun && (GetDebugAsStatic() || GamePrefs.GetBool(EnumGamePrefs.DebugMenuEnabled)) && !InputUtils.ControlKeyPressed);

            NGuiAction NguiToggleDCMove = playerMoveController.globalActions.Find(x => x.text == "ToggleDCMove");
            NguiToggleDCMove.SetIsEnabledDelegate(() => playerMoveController.gameManager.gameStateManager.IsGameStarted() && GameStats.GetInt(EnumGameStats.GameState) == 1 && !playerMoveController.entityPlayerLocal.AimingGun && (GetDebugAsStatic() || GamePrefs.GetBool(EnumGamePrefs.DebugMenuEnabled)));

            NGuiAction NguiLockCamera = playerMoveController.globalActions.Find(x => x.text == "LockCamera");
            NguiLockCamera.SetIsEnabledDelegate(() => playerMoveController.gameManager.gameStateManager.IsGameStarted() && GameStats.GetInt(EnumGameStats.GameState) == 1 && !playerMoveController.entityPlayerLocal.AimingGun && (GetDebugAsStatic() || GamePrefs.GetBool(EnumGamePrefs.DebugMenuEnabled)));
        }

    }




    public static bool GetDebugAsStatic()
    {
        return GamePrefs.GetBool(EnumGamePrefs.DebugMenuEnabled);
    }



    public bool GetDebug()
        {
        return GamePrefs.GetBool(EnumGamePrefs.DebugMenuEnabled);
        }

}
