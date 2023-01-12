using AlwaysRequestSuitLocker;
using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using PeterHan.PLib.PatchManager;
using System;
using System.Linq;

namespace AlwaysRequestSuitLocker
{
    [HarmonyPatch(typeof(SuitLocker), nameof(SuitLocker.DropSuit), new Type[] { })]
    internal class DropSuitPatch
    {
        private static void Postfix(StateMachineComponent __instance)
        {
            AlwaysRequestSuitLockerUtil.requestSuit(__instance);
        }
    }

    [HarmonyPatch(typeof(SuitLocker), nameof(SuitLocker.EquipTo), new Type[] { typeof (Equipment) })]
    internal class EqiopToPatch
    {
        private static void Postfix(Equipment equipment, StateMachineComponent __instance)
        {
            AlwaysRequestSuitLockerUtil.requestSuit(__instance);
        }
    }

    public sealed class AlwaysRequestSuitLockerOptions : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new POptions().RegisterOptions((UserMod2)this, typeof(Options));
            new PPatchManager(harmony).RegisterPatchClass(typeof(AlwaysRequestSuitLockerOptions));
        }

        [HarmonyPatch(typeof(PlayerController), "OnPrefabInit")]
        public static class PlayerControllerOnPrefabInitPatch
        {
            public static void Postfix(PlayerController __instance)
            {
                Globals.opt = POptions.ReadSettings<Options>() ?? new Options();
            }
        }
    }
}

class AlwaysRequestSuitLockerUtil
{
    public static StateMachineComponent requestSuit(StateMachineComponent smc)
    {
        if (smc is SuitLocker sl)
        {
            if (
              (sl.OutfitTags.First() == GameTags.OxygenMask && Globals.opt.OxygenMask) ||
              (sl.OutfitTags.First() == GameTags.AtmoSuit && Globals.opt.AtmoSuit) ||
              (sl.OutfitTags.First() == GameTags.JetSuit && Globals.opt.JetSuit))
            {
                sl.smi.sm.isWaitingForSuit.Set(true, sl.smi);

            }
        }
        return smc;
    }

}