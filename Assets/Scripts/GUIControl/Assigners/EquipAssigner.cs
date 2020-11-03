using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class EquipAssigner
{
    private static Dictionary<int, GraphicEquipAttributes> equpmentDictionary = new Dictionary<int, GraphicEquipAttributes>();
    static EquipAssigner()
    {
        equpmentDictionary.Add(0, new GraphicEquipAttributes("Fantasy 05", new Color(0.26f, 0.82f, 0.96f), new Color(0.18f, 0.18f, 0.21f), new Color(0.26f, 0.82f, 0.96f)));
        equpmentDictionary.Add(1, new GraphicEquipAttributes("Sword 00", GetColor(68, 48, 34), GetColor(68, 48, 34), GetColor(68, 48, 34)));
    }

    public static string GetEquipName(int id)
    {
        return equpmentDictionary[id].Name;
    }

    public static Color GetEquipColor(int id, int colorNumber)
    {
        switch(colorNumber)
        {
            case 1:
                return equpmentDictionary[id].Color1;
            case 2:
                return equpmentDictionary[id].Color2;
            case 3:
                return equpmentDictionary[id].Color3;
            default:
                return equpmentDictionary[id].Color1;
        }
    }

    private static Color GetColor (float r, float g, float b)
    {
        return new Color((r / 255), g / 255, b / 255);
    }

    public class GraphicEquipAttributes
    {
        public string Name { get; set; }
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
        public Color Color3 { get; set; }

        public GraphicEquipAttributes(string name, Color color1, Color color2, Color color3)
        {
            Name = name;
            Color1 = color1;
            Color2 = color2;
            Color3 = color3;
        }

    }
}
