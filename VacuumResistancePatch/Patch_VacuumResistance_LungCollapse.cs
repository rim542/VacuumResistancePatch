using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace VacuumResistancePatch
{
    public static class Patch_VacuumResistance_LungCollapse
    {
        public static bool Prefix(object __instance, ref DamageInfo dinfo)
        {
            Pawn pawn = Traverse.Create(__instance)
                .Property("Pawn")
                .GetValue<Pawn>();

            if (pawn == null)
                return true;

            float vacuumResistance = Mathf.Clamp01(
                pawn.GetStatValue(StatDefOf.VacuumResistance));

            // If the pawn succeeds the resistance roll,
            // skip the More Injuries lung collapse logic.
            if (Rand.Chance(vacuumResistance))
                return false;

            return true;
        }
    }
}