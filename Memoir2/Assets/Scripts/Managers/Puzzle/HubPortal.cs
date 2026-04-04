using UnityEngine;

public class HubPortal : MonoBehaviour
{
    [Header("Level Index this portal represents")]
    public int levelIndex;

    [Header("Portal Puzzle Pieces (4)")]
    public GameObject[] puzzlePieces; // assign in Inspector

    private void Start()
    {
        if (puzzlePieces.Length != 4)
            Debug.LogWarning("Portal must have exactly 4 puzzle pieces!");
    }

    private void Update()
    {
        RefreshVisuals();
    }

    public void RefreshVisuals()
    {
        if (PlayerData.Instance == null) return;
        var progress = PlayerData.Instance.levels[levelIndex];

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool collected = progress.piecesCollected[i];
            var rend = puzzlePieces[i].GetComponent<Renderer>();
            if (rend != null)
            {
                Color c = rend.material.color;
                c.a = collected ? 1f : 0.2f; // filled or blank
                rend.material.color = c;
            }
        }
    }

    // Static helper to update portals on piece collection
    public static void UpdatePortal(int levelIndex, int pieceIndex)
    {
        // Modern Unity API to replace obsolete FindObjectsOfType
        var portals = Object.FindObjectsByType<HubPortal>(FindObjectsSortMode.None);

        foreach (var p in portals)
        {
            if (p.levelIndex == levelIndex)
                p.RefreshVisuals();
        }
    }
}