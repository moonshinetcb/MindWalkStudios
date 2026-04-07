using StarterAssets;
//using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerStickToWall : MonoBehaviour
{
    ThirdPersonController movementScript;
    private StarterAssetsInputs _input;
    public bool IsOnWall = false;
    private void Start()
    {
        movementScript = GetComponent<ThirdPersonController>();
        _input = GetComponent<StarterAssetsInputs>();
        Debug.Log(movementScript.Grounded);
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PaintedWall"))
        {
            movementScript.Gravity = 0;
            movementScript._verticalVelocity = 0;
            Debug.Log("OnPaintedWall");

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PaintedWall"))
        {
            movementScript.Gravity = -15;
            Debug.Log("ExitPaintedWall");


        }
    }
} 
