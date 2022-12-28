using HarmonyLib;
using System;

namespace AlwaysRequestSuitLocker
{
    [HarmonyPatch(typeof(SuitLocker), nameof(SuitLocker.DropSuit), new Type[] { })]
    internal class DropSuitPatch
    {
        private static void Postfix(SuitLocker __instance)
        {
            AlwaysRequestSuitLockerUtil.requestSuit(__instance);
        }
    }

    [HarmonyPatch(typeof(SuitLocker), nameof(SuitLocker.EquipTo), new Type[] { typeof(Equipment) })]
    internal class EqiopToPatch
    {
        private static void Postfix(Equipment equipment, SuitLocker __instance)
        {
            AlwaysRequestSuitLockerUtil.requestSuit(__instance);
        }
    }
}

class AlwaysRequestSuitLockerUtil
{
    public static SuitLocker requestSuit(SuitLocker sl)
    {
        sl.smi.sm.isWaitingForSuit.Set(true, sl.smi);
        return sl;
    }

}