using Fusion;
using Fusion.Addons.SimpleKCC;
using UnityEngine;

public class PlayerAnimation : NetworkBehaviour
{
    public SimpleKCC kcc;

    public Animator animator;

    public override void Render()
    {
        float magnitude = new Vector3(kcc.RealVelocity.x, 0, kcc.RealVelocity.z).magnitude;
        animator.SetBool("walking", magnitude > 0.4f);
    }
}
