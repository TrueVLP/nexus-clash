using UnityEngine;
using Fusion;
using Fusion.Addons.SimpleKCC;

/// <summary>
/// Player implementation, processes input and controls KCC.
/// </summary>
[DefaultExecutionOrder(-5)]
public sealed class PlayerController : NetworkBehaviour
{
    public SimpleKCC kcc;
    public PlayerInput input;
    public Transform cameraPosition;
    public GameObject thirdPersonVisual;
    public GameObject firstPersonVisual;

    [Header("Movement")]
    public float moveSpeed = 7;
    public float jumpImpulse = 8;
    public float upGravity = -25.0f;
    public float downGravity = -40.0f;
    public float groundAcceleration = 55.0f;
    public float groundDeceleration = 25.0f;
    public float airAcceleration = 25.0f;
    public float airDeceleration = 1.3f;

    [Networked]
    private Vector3 MoveVelocity { get; set; }

    public override void Spawned()
    {
        // Set visuals 
        if (thirdPersonVisual != null)
        {
            thirdPersonVisual.SetActive(HasInputAuthority == false);
        }
        if (firstPersonVisual != null)
        {
            firstPersonVisual.SetActive(HasInputAuthority == true);
        }
    }

    public override void FixedUpdateNetwork()
    {
        // Apply look rotation delta. This propagates to Transform component immediately.
        kcc.AddLookRotation(input.currentInput.lookRotationDelta);

        // Set default world space input direction and jump impulse.
        Vector3 inputDirection = kcc.TransformRotation * new Vector3(input.currentInput.moveDirection.x, 0.0f, input.currentInput.moveDirection.y);
        float jumpImpulse = default;

        // Comparing current input to previous input - this prevents glitches when input is lost.
        if (input.currentInput.actions.WasPressed(input.previousInput.actions, GameplayInput.JUMP_BUTTON) == true)
        {
            if (kcc.IsGrounded == true)
            {
                // Set world space jump vector.
                jumpImpulse = this.jumpImpulse;
            }
        }

        // It feels better when the player falls quicker.
        kcc.SetGravity(kcc.RealVelocity.y >= 0.0f ? upGravity : downGravity);

        Vector3 desiredMoveVelocity = inputDirection * moveSpeed;

        float acceleration;
        if (desiredMoveVelocity == Vector3.zero)
        {
            // No desired move velocity - we are stopping.
            acceleration = kcc.IsGrounded == true ? groundDeceleration : airDeceleration;
        }
        else
        {
            acceleration = kcc.IsGrounded == true ? groundAcceleration : airAcceleration;
        }

        MoveVelocity = Vector3.Lerp(MoveVelocity, desiredMoveVelocity, acceleration * Runner.DeltaTime);

        kcc.Move(MoveVelocity, jumpImpulse);
    }

    private void LateUpdate()
    {
        // Only InputAuthority needs to update camera.
        if (HasInputAuthority == false)
            return;

        // Update camera pivot and transfer properties from camera handle to Main Camera.
        // Render() is executed before KCC because of [OrderBefore(typeof(KCC))].
        // So we have to do it from LateUpdate() - which is called after Render().

        Vector2 pitchRotation = kcc.GetLookRotation(true, false);
        cameraPosition.localRotation = Quaternion.Euler(pitchRotation);

        Camera.main.transform.SetPositionAndRotation(cameraPosition.position, cameraPosition.rotation);
    }

}