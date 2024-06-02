using Fusion;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Networking;

public class AdjustFOV : NetworkBehaviour
{
    public Camera playerCamera;
    public float normalFOV = 60f;
    public float zoomedFOV = 90f;

    public override void Spawned()
    {

        playerCamera = FindFirstObjectByType<Camera>();
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(1) && HasInputAuthority)
        {
            playerCamera.fieldOfView = zoomedFOV;
        }
        else if (Input.GetMouseButtonUp(1) && HasInputAuthority)
        {
            playerCamera.fieldOfView = normalFOV;
        }
    }
}
