using UnityEngine;

public enum PaintPrimitiveType
{
    Square,
    Semicircle,
    Triangle,
}
[System.Serializable]
public struct PrimitivePrefabEntry
{
    public PaintPrimitiveType type;
    public GameObject prefab;
    public GameObject ghostPrefab;
}
