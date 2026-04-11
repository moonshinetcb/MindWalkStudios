using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    [Header("Level & Piece Index")]
    public int levelIndex;
    public int pieceIndex;

    [Header("Optional Visual")]
    public GameObject visualObject;

    private bool collected = false;

    private void Start()
    {
        // Hide/fade piece if already collected
        if (PlayerData.Instance != null)
        {
            if (PlayerData.Instance.levels[levelIndex].piecesCollected[pieceIndex])
            {
                collected = true;
                SetCollectedVisual();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Update PlayerData
        PlayerData.Instance.CollectPiece(levelIndex, pieceIndex);

        SetCollectedVisual();
    }

    private void SetCollectedVisual()
    {
        if (visualObject != null)
        {
            var rend = visualObject.GetComponent<Renderer>();
            if (rend != null)
            {
                Color c = rend.material.color;
                c.a = 0.25f; // semi-transparent in-level
                rend.material.color = c;
            }
        }
    }
}
