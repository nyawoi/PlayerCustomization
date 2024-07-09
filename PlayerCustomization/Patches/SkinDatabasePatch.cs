using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace AetharNet.Mods.ZumbiBlocks2.PlayerCustomization.Patches;

[HarmonyPatch(typeof(SkinDatabase))]
public static class SkinDatabasePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(SkinDatabase.Init))]
    public static void AddCustomSkins(SkinDatabase __instance)
    {
        var allColors = __instance.allColors.ToList();
        var defaultColorMaterial = allColors[0].material;
        
        foreach (var skinTexture in CustomSkinDatabase.GetJsonSkins())
        {
            // Add new color
            allColors.Add(new SkinDatabase.SkinColor
            {
                // Create new material based on default
                material = new Material(defaultColorMaterial)
                {
                    mainTexture = skinTexture,
                    name = skinTexture.name
                },
                // Get pixel color for ShirtPrimary cell
                refColor1 = skinTexture.GetPixel(78, 110),
                // Get pixel color for BottomPrimary cell
                refColor2 = skinTexture.GetPixel(46, 78)
            });
        }
        
        foreach (var skinTexture in CustomSkinDatabase.GetPaletteSkins())
        {
            // Add new color
            allColors.Add(new SkinDatabase.SkinColor
            {
                // Create new material based on default
                material = new Material(defaultColorMaterial)
                {
                    mainTexture = skinTexture,
                    name = skinTexture.name
                },
                // Get pixel color for ShirtPrimary cell
                refColor1 = skinTexture.GetPixel(78, 110),
                // Get pixel color for BottomPrimary cell
                refColor2 = skinTexture.GetPixel(46, 78)
            });
        }
        
        // Assign modified color list
        __instance.allColors = allColors.ToArray();
    }
}
