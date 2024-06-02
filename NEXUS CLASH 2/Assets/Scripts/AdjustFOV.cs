using Fusion;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Networking;

public class AdjustFOV : NetworkBehaviour
{
    public Camera playerCamera; // Die Kamera des Spielers
    public float normalFOV = 60f; // Normales FOV
    public float zoomedFOV = 90f; // Vergrößertes FOV

    public override void Spawned()
    {

        playerCamera = FindFirstObjectByType<Camera>();
    }

    void Update()
    {


        // Wenn die rechte Maustaste gedrückt wird
        if (Input.GetMouseButtonDown(1) && HasInputAuthority)
        {
            // Vergrößern Sie das FOV
            playerCamera.fieldOfView = zoomedFOV;
        }
        // Wenn die rechte Maustaste losgelassen wird
        else if (Input.GetMouseButtonUp(1) && HasInputAuthority)
        {
            // Setzen Sie das FOV auf normal zurück
            playerCamera.fieldOfView = normalFOV;
        }
    }
}
