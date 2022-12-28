using HarmonyLib;
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

    [HarmonyPatch(typeof(SuitLocker), nameof(SuitLocker.EquipTo), new Type[] { typeof(Equipment) })]
    internal class EqiopToPatch
    {
        private static void Postfix(Equipment equipment, StateMachineComponent __instance)
        {
            AlwaysRequestSuitLockerUtil.requestSuit(__instance);
        }
    }
}

class AlwaysRequestSuitLockerUtil
{
    public static StateMachineComponent requestSuit(StateMachineComponent smc)
    {

        if (smc is SuitLocker sl && sl.OutfitTags.First() != GameTags.JetSuit)
        {
            sl.smi.sm.isWaitingForSuit.Set(true, sl.smi);
        }
        return smc;
    }

}