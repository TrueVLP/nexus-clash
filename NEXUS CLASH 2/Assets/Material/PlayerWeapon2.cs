using Fusion;
using Fusion.Addons.SimpleKCC;
using System.Collections;
using UnityEngine;

public class PlayerWeapon2 : NetworkBehaviour
{
    public GameObject bulletPrefab; 
    public Transform spawnPoint; 

    private NetworkRunner networkRunner;

    public Transform cameraposition;

    public SimpleKCC kcc;

    public LayerMask hitmask;

    private bool lastpressed = false;

    public GameObject yourGameObject; 
    public Transform originalPosition; 
    public Transform newPosition;

    private Coroutine resetPositionCoroutine;

    public override void Spawned()
    {
        networkRunner = FindObjectOfType<NetworkRunner>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out GameplayInput input))
        {
            ProcessInput(input);
        }
    }

    private void ProcessInput(GameplayInput input)
    {
        if (input.actions.IsSet(GameplayInput.SHOOT_BUTTON))
        {
            if (!lastpressed)
            {
                lastpressed = true;
                MoveGameObjectToNewPosition();
                Fire();

                if (resetPositionCoroutine != null)
                {
                    StopCoroutine(resetPositionCoroutine);
                }
            }
        }
        else
        {
            if (lastpressed)
            {
                lastpressed = false;
                resetPositionCoroutine = StartCoroutine(ResetPositionAfterDelay(1f));
            }
        }
    }

    private void MoveGameObjectToNewPosition()
    {
        Debug.Log("Moving to new position: " + newPosition.position + ", rotation: " + newPosition.rotation);
        yourGameObject.transform.position = newPosition.position;
        yourGameObject.transform.rotation = newPosition.rotation;
    }

    private void Fire()
    {
        Vector3 spawnPosition = spawnPoint.position;
        Quaternion spawnRotation = Quaternion.LookRotation(kcc.LookDirection);

        if (Runner.IsServer)
        {
            NetworkObject bulletInstance = Runner.Spawn(bulletPrefab, spawnPosition, spawnRotation, Object.InputAuthority);
            if (bulletInstance != null)
            {
                Bullet bullet = bulletInstance.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.Initialize(kcc.LookDirection);
                }
            }
        }
    }

    private IEnumerator ResetPositionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Resetting to original position: " + originalPosition.position + ", rotation: " + originalPosition.rotation);
        yourGameObject.transform.position = originalPosition.position;
        yourGameObject.transform.rotation = originalPosition.rotation;
    }
}
