using UnityEngine;

[CreateAssetMenu]
public class ScalingConfig :ScriptableObject {
    [Tooltip("0: multiply, 1: add")]
    public int scalingMode = 0;

    public float scalingMultOnEndTurn = 1;
    public float scalingMultOnMerge = 1;
    public float scalingMultOnDestroy = 1;

    public Config conf;
}
