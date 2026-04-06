using UnityEngine;

public class PaintAttack : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PaintedWall"))
        {
            Destroy(gameObject);
        }
    }
}
