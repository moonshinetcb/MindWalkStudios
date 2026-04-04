using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class PlayerCameraLink : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget; // assign PlayerCameraRoot in prefab for safety

    private IEnumerator Start()
    {
        yield return null; // wait 1 frame

        // Find the CinemachineCamera in the scene
        CinemachineCamera cam = FindAnyObjectByType<CinemachineCamera>();

        if (cam == null)
        {
            Debug.LogError("No CinemachineCamera found in scene!");
            yield break;
        }

        // If cameraTarget is not assigned, try to find by name in prefab
        if (cameraTarget == null)
        {
            cameraTarget = transform.Find("PlayerCameraRoot");

            if (cameraTarget == null)
            {
                Debug.LogError("PlayerCameraRoot not found on player!");
                yield break;
            }
        }

        // Assign camera follow/LookAt
        cam.Follow = cameraTarget;
        cam.LookAt = cameraTarget;
    }
}