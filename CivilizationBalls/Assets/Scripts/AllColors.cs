using System;
using UnityEngine;

[CreateAssetMenu]
public class AllColors : ScriptableObject {
    public int[] possibleColors;
    public BallColor[] settingsPerColor;
    public Color[] colors;
    public int maxColor;

    public BallColor GetBallColorSettings(int ballColor)
    {

        for (int i = 0; i < possibleColors.Length; i++)
        {
            if (possibleColors[i] == ballColor)
            {
                return settingsPerColor[i];
            }
        }
        Debug.Log("missing color "+ ballColor);
        return null;
    }

    internal Color GetColor(int ballColor)
    {
        for (int i = 0; i < possibleColors.Length; i++)
        {
            if (possibleColors[i] == ballColor)
            {
                return colors[i];
            }
        }
        Debug.Log("missing color " + ballColor);
        return Color.black;
    }
}
