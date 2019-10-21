using System;
using UnityEngine;

[CreateAssetMenu]
public class BallColor : ScriptableObject {

    public int[] canMerge;
    public int[] mergeNewColor;
    public int[] canDestroy;

    internal bool CanMerge(int otherBallColor)
    {
        for (int i = 0; i < canMerge.Length; i++)
        {
            if (canMerge[i] == otherBallColor)
            {
                return true;
            }
        }
        return false;
    }
    internal int GetMergedColorId(int otherBallColor)
    {
        for (int i = 0; i < canMerge.Length; i++)
        {
            if (canMerge[i] == otherBallColor)
            {
                return mergeNewColor[i];
            }
        }
        Debug.Log("No merger color for "+otherBallColor);
        return -1;
    }

    internal bool CanDestroy(int otherBallColor)
    {
        for (int i = 0; i < canDestroy.Length; i++)
        {
            if (canDestroy[i] == otherBallColor)
            {
                return true;
            }
        }
        return false;
    }
}
