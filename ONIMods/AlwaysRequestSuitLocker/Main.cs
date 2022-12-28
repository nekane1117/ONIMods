using HarmonyLib;

namespace HelloWorld
{
    [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
    internal class HelloWorld_GeneratedBuildings_LoadGeneratedBuildings
    {
        private static void Postfix()
        {
            Debug.Log("Hello World");
        }
    }
}