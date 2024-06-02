using UnityEngine;
using Fusion;
using Unity.VisualScripting;

public class Bullet : NetworkBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    public void Initialize(Vector3 direction)
    {
        this.direction = direction;
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            transform.Translate(direction * speed * Runner.DeltaTime, Space.World);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Log.Debug("aua");
        if (HasStateAuthority)
        {
            // Logik für das Treffen eines Ziels
            Log.Debug("weg");
            Runner.Despawn(Object);

            PlayerHealth health = other.gameObject.GetComponentInParent<PlayerHealth>();

            if (health != null)
            {
                health.Change(-15);
            }
        }
    }
}
