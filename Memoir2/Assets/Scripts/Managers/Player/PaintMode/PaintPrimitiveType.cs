using UnityEngine;

public enum PaintPrimitiveType
{
    Square,
    Semicircle,
    Triangle,
    WallPaint,
}
[System.Serializable]
public struct PrimitivePrefabEntry
{
    public PaintPrimitiveType type;
    public GameObject prefab;
}
