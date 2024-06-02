using Fusion;
using Fusion.Addons.SimpleKCC;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerWeapon : NetworkBehaviour
{

    public Transform cameraposition;

    public SimpleKCC kcc;

    public LayerMask hitmask;

    private bool lastpressed = false;

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out GameplayInput input))
        {
            ProcessInput(input);
        }
    }

    private void ProcessInput(GameplayInput input)
    {
        if(input.actions.IsSet(GameplayInput.SHOOT_BUTTON))
        {
            if(!lastpressed)
            {
                lastpressed = true;
                Fire();
            }
        }
        else
        {
            lastpressed = false;
        }
    }

    private void Fire()
    {
        var hitOptions = HitOptions.IncludePhysX | HitOptions.IgnoreInputAuthority;

        if(Runner.LagCompensation.Raycast(cameraposition.position, kcc.LookDirection, 50, Object.InputAuthority, out var hit, hitmask, hitOptions))
        {
            PlayerHealth health = hit.GameObject.GetComponentInParent<PlayerHealth>();

            if(health != null)
            {
                health.Change(-15);
            }
        }
    }
}
