using System.Reflection;
using HarmonyLib;
using Verse;

namespace VacuumResistancePatch
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            var harmony = new Harmony("ray.vacuumresistancepatch");

            var targetType = AccessTools.TypeByName(
                "MoreInjuries.HealthConditions.LungCollapse.LungCollapseThermobaricWorker");

            if (targetType == null)
            {
                Log.Error("[VacuumResistancePatch] Failed to find LungCollapseThermobaricWorker.");
                return;
            }

            MethodInfo targetMethod = AccessTools.Method(targetType, "PostPostApplyDamage");

            if (targetMethod == null)
            {
                Log.Error("[VacuumResistancePatch] Failed to find PostPostApplyDamage.");
                return;
            }

            harmony.Patch(
                targetMethod,
                prefix: new HarmonyMethod(typeof(Patch_VacuumResistance_LungCollapse), nameof(Patch_VacuumResistance_LungCollapse.Prefix))
            );

            Log.Message("[VacuumResistancePatch] Loaded successfully.");
        }
    }
}