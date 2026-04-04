using UnityEngine;

public class CollectiblePiece : MonoBehaviour
{
    [Header("Level & Piece Index")]
    public int levelIndex;
    public int pieceIndex;

    [Header("Optional Visual")]
    public GameObject visualObject;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        if (!other.CompareTag("Player")) return;

        collected = true;

        // Update PlayerData
        PlayerData.Instance.CollectPiece(levelIndex, pieceIndex);

        // Make piece semi-transparent in the world
        if (visualObject != null)
        {
            var rend = visualObject.GetComponent<Renderer>();
            if (rend != null)
            {
                Color c = rend.material.color;
                c.a = 0.25f;
                rend.material.color = c;
            }
        }
    }
}
