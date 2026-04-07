using Unity.VisualScripting;
using UnityEngine;

public class PaintChecking : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PaintAttack"))
        {
            Debug.Log("Painted");
            gameObject.tag = "PaintedWall";
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
