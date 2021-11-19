using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerMovementManager : MonoBehaviour
{
    public CharacterController characterController;
    public float rotationSpeed;

    private Camera mainCamera;
    private Vector3 stickDirection;
    private PlayerController controller;
    private PlayerAnimationManager animation;
    private bool IsJumping { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        animation = this.GetComponent<PlayerAnimationManager>();
    }

    private void Start()
    {
        controller = PlayerController.Instance;
    }

    public void Move()
    {
        ///if (!controller.IsAlive || !controller.CanInteract) { return; }

        //if(!IsAlive) { controller.Move(new Vector3(0, -groundHit.distance, 0) * Time.deltaTime * playerSpeed); return; }

        //if (!CanInteract) { return; } // Nao move caso nao possa interagir com os controles

        float horizontal = Mathf.Clamp(InputSystem.Horizontal() + DynamicJoystick.Instance.Horizontal, -1, 1);
        float vertical = Mathf.Clamp(InputSystem.Vertical() + DynamicJoystick.Instance.Vertical, -1, 1);

        stickDirection = new Vector3(horizontal, 0, vertical);

        float x = mainCamera.transform.TransformDirection(stickDirection).x;
        float z = mainCamera.transform.TransformDirection(stickDirection).z;
        if (x > 1) x = 1; // assegura que o jogador nao ira se mover mais rapido em diagonal
        if (z > 1) z = 1;

        float multiplier = Input.GetKey(KeyCode.LeftShift) && Application.isEditor ? 2 : 1; // acelera o player no editor com o shift pressionado

        //IsGravityOn = !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump");

        /// Mathf.Sqrt(controller.JumpForce)
        characterController.Move(new Vector3(x, 0, z) * Time.deltaTime * controller.Speed * multiplier);
        animation.SetPlayerSpeed(Vector3.ClampMagnitude(stickDirection, 1).magnitude);
    }

    public void Rotate()
    {
        ///if (!controller.CanInteract) { return; }

        Vector3 rotationOffset = mainCamera.transform.TransformDirection(stickDirection);
        rotationOffset.y = 0;
        characterController.transform.forward += Vector3.Lerp(characterController.transform.forward, rotationOffset, Time.deltaTime * rotationSpeed);
    }

    public void Jump()
    {
        if (InputSystem.Jump() && !IsJumping && controller.IsGrounded)
        {
            animation.Jump();
        }
    }
}
