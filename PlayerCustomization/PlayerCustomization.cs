using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AetharNet.Mods.ZumbiBlocks2.PlayerCustomization;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class PlayerCustomization : BaseUnityPlugin
{
    public const string PluginGUID = "AetharNet.Mods.ZumbiBlocks2.PlayerCustomization";
    public const string PluginAuthor = "wowi";
    public const string PluginName = "PlayerCustomization";
    public const string PluginVersion = "0.1.0";

    internal new static ManualLogSource Logger;
    
    private void Awake()
    {
        Logger = base.Logger;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), PluginGUID);
    }
}
