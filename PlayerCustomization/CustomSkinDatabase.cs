using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;

namespace AetharNet.Mods.ZumbiBlocks2.PlayerCustomization;

public static class CustomSkinDatabase
{
    public static IEnumerable<Texture2D> GetPaletteSkins()
    {
        foreach (var pluginDirectoryPath in Directory.EnumerateDirectories(BepInEx.Paths.PluginPath))
        {
            foreach (var skinFilePath in Directory.EnumerateFiles(pluginDirectoryPath, "*.skin.png"))
            {
                var imageBytes = File.ReadAllBytes(skinFilePath);
                var texture = new Texture2D(128, 128);
                
                texture.LoadImage(imageBytes);
                texture.name = skinFilePath;
                
                yield return texture;
            }
        }
    }
    
    public static IEnumerable<Texture2D> GetJsonSkins()
    {
        foreach (var pluginDirectoryPath in Directory.EnumerateDirectories(BepInEx.Paths.PluginPath))
        {
            foreach (var skinFilePath in Directory.EnumerateFiles(pluginDirectoryPath, "*.skin.json"))
            {
                var jsonText = File.ReadAllText(skinFilePath);
                var jsonObject = JsonConvert.DeserializeObject<CustomColors>(jsonText);
                
                var texture = new Texture2D(128, 128, TextureFormat.ARGB32, false);
                var colorFill = new Color[CellSize * CellSize];

                for (int y = 0; y < TextureGrid.Length; y++)
                {
                    for (int x = 0; x < TextureGrid[y].Length; x++)
                    {
                        var cellName = TextureGrid[y][x];
                        var cellColor = jsonObject[cellName];
                        
                        for (int index = 0; index < colorFill.Length; index++)
                        {
                            colorFill[index] = cellColor;
                        }
                        
                        texture.SetPixels(
                            x * CellSize, (TextureGrid.Length - y) * CellSize,
                            CellSize, CellSize,
                            colorFill);
                    }
                }
                
                for (int index = 0; index < colorFill.Length; index++)
                {
                    colorFill[index] = jsonObject["Eye"];
                }
                
                texture.SetPixels(
                    96, 0,
                    CellSize, CellSize,
                    colorFill);
                
                for (int index = 0; index < colorFill.Length; index++)
                {
                    colorFill[index] = jsonObject["Pupil"];
                }
                
                texture.SetPixels(
                    103, 1,
                    18, 26,
                    colorFill);
                
                texture.name = skinFilePath;
                texture.Apply();
                
                yield return texture;
            }
        }
    }

    private const byte CellSize = 32;

    private static readonly string[][] TextureGrid =
    [
        [ "Skin", "Mouth", "ShirtPrimary", "ShirtSecondary" ],
        [ "BottomSecondary", "BottomPrimary", "BeltSecondary", "BeltPrimary" ],
        [ "HairPrimary", "HairSecondary", "BootsPrimary", "BootsSecondary" ]
    ];
    
    public class CustomColors
    {
        public string Skin = "#F1C48F";
        public string Mouth = "#F18B5F";
        public string ShirtPrimary = "#202020";
        public string ShirtSecondary = "#393A39";
        public string BottomPrimary = "#25346B";
        public string BottomSecondary = "#354998";
        public string BeltPrimary = "#313031";
        public string BeltSecondary = "#676767";
        public string HairPrimary = "#894D39";
        public string HairSecondary = "#331C15";
        public string BootsPrimary = "#554F47";
        public string BootsSecondary = "#444039";
        public string Eye = "#FFFFFF";
        public string Pupil = "#36241B";

        public Color this[string cellName]
        {
            get
            {
                var cellNameField = GetType().GetField(cellName, BindingFlags.Instance | BindingFlags.Public);

                if (cellNameField == null) return Color.magenta;

                var hexCode = cellNameField.GetValue(this) as string;

                if (hexCode == null || hexCode.Trim().Length < 6) return Color.magenta;

                if (hexCode[0] != '#') hexCode = '#' + hexCode;
                
                return ColorUtility.TryParseHtmlString(hexCode, out var color)
                    ? color
                    : Color.magenta;
            }
        }
    }
}
