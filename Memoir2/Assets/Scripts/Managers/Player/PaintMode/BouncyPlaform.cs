using UnityEngine;
using StarterAssets;

/// <summary>
/// Bounces the player when they step on this platform.
/// </summary>
[RequireComponent(typeof(Collider))]
public class BouncyPlatform : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    private void Reset()
    {
        // trigger is required since the CharacterController wont activate OnCollisionEnter
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        ThirdPersonController player = other.GetComponent<ThirdPersonController>();
        if (player == null)
        {
            return;
        }

        player.ApplyBounce(bounceForce);
        Debug.Log("bounced " + bounceForce);
    }
}