using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

/// <summary>
/// Controls paint mode toggling, shape selection and spawning.
/// </summary>
public class PaintModeController : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform spawnAnchor;
    [SerializeField] private int maxPerType = 5;

    [Header("Primitive Prefabs")]
    [SerializeField] private List<PrimitivePrefabEntry> primitiveEntries;

    // converts Inspector list into dictionary for quick lookup
    private Dictionary<PaintPrimitiveType, PrimitivePrefabEntry> primitiveMap;
    private Dictionary<PaintPrimitiveType, int> placedCounts;
    private PaintPrimitiveType currentType;
    private bool inPaintMode;

    private PaintPrimitiveType[] allTypes; 
    private int currentTypeIndex;
    private GameObject currentGhost;

    private void Awake()
    {
        primitiveMap = new Dictionary<PaintPrimitiveType, PrimitivePrefabEntry>();
        foreach (PrimitivePrefabEntry entry in primitiveEntries)
        {
            primitiveMap[entry.type] = entry;
        }

        allTypes = (PaintPrimitiveType[])System.Enum.GetValues(typeof(PaintPrimitiveType));
        placedCounts = new Dictionary<PaintPrimitiveType, int>();
        foreach (PaintPrimitiveType type in allTypes)
        {
            placedCounts[type] = 0;
        }

        currentTypeIndex = 0;
        currentType = allTypes[currentTypeIndex];
        currentGhost = null;
    }

    private void SpawnGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
        if (primitiveMap.ContainsKey(currentType) && primitiveMap[currentType].ghostPrefab != null)
        {
            currentGhost = Instantiate(primitiveMap[currentType].ghostPrefab, spawnAnchor.position, Quaternion.identity);
        }
    }

    private void DestroyGhost()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
            currentGhost = null;
        }
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        Mouse mouse = Mouse.current;
        if (keyboard == null || mouse == null)
        {
            return;
        }

        if (keyboard.qKey.wasPressedThisFrame)
        {
            // Toggle paint mode that lets the next part of the code can run
            inPaintMode = !inPaintMode;
            if (inPaintMode)
            {
                SpawnGhost();
            }
            else
            {
                DestroyGhost();
            }
            Debug.Log("Paint mode toggled: " + inPaintMode);
        }
        // Nothing below this will run unless we're in paint mode, handles placing and cycling between shapes
        if (!inPaintMode)
        {
            return;
        }

        if (currentGhost != null)
        {
            currentGhost.transform.position = spawnAnchor.position;
        }

        float scroll = mouse.scroll.y.ReadValue();
        if (scroll > 0f)
        {
            CycleShape(1);
        }
        else if (scroll < 0f)
        {
            CycleShape(-1);
        }
        if (mouse.leftButton.wasPressedThisFrame)
        {
            TryPlace();
        }
    }

    private void CycleShape(int direction)
    {
        currentTypeIndex = (currentTypeIndex + direction + allTypes.Length) % allTypes.Length;
        currentType = allTypes[currentTypeIndex];
        if (inPaintMode)
        {
            SpawnGhost();
        }
        Debug.Log("Selected shape: " + currentType);
    }

    private void TryPlace()
    {
        // Checks to make sure placing is valid
        if (placedCounts[currentType] >= maxPerType)
        {
            Debug.Log("Max count reached for " + currentType);
            return;
        }
        if (!primitiveMap.ContainsKey(currentType))
        {
            Debug.LogError("No prefab found for " + currentType);
            return;
        }
        
        // Destroys the ghost when placing and respawns it after
        DestroyGhost();
        
        PrimitivePrefabEntry entry = primitiveMap[currentType];
        Instantiate(entry.prefab, spawnAnchor.position, Quaternion.identity);
        placedCounts[currentType]++;
        Debug.Log("Placed " + currentType + " (" + placedCounts[currentType] + "/" + maxPerType + ")");
        
        SpawnGhost();
    }
}
