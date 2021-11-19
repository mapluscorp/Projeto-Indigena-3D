using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [Header("References")]
    public Animator playerAnim;
    public Camera mainCamera;
    public DynamicJoystick joytick;
    public float boatMaxSpeed = 3f;
    public float boatTurnSpeed = 2f;
    public float boatSpeed = 0f;
    public float acceleration = 0.4f;

    public Transform targetTransform;
    public GameObject paddle;

    private CharacterController controller;
    [HideInInspector]
    public Vector3 stickDirection;

    public bool IsEnabled { get; set; }

    private float vertical;
    private float horizontal;

    private float lastAccelTime;

    private void Start()
    {
        IsEnabled = false;
        controller = this.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (IsEnabled)
        {
            playerAnim.transform.root.parent = this.transform.parent;
            playerAnim.transform.position = targetTransform.position;
            Move();
            Rotate();
        }
    }

    public void SetBoatPaddleState(bool state)
    {
        paddle.SetActive(state);
    }

    private void Move()
    {
        horizontal = Mathf.Clamp(InputSystem.Horizontal() + joytick.Horizontal, -1, 1);
        vertical = Mathf.Clamp(InputSystem.Vertical() + joytick.Vertical, 0, 1);

        stickDirection = new Vector3(horizontal, 0, vertical);

        playerAnim.SetBool("Paddling", stickDirection.magnitude > 0.1f);
        playerAnim.SetFloat("Horizontal", horizontal);

        float x = this.transform.TransformDirection(stickDirection).x;
        float z = this.transform.TransformDirection(stickDirection).z;
        if (x > 1) x = 1; // assegura que o jogador nao ira se mover mais rapido em diagonal
        if (z > 1) z = 1;

        float multiplier = Input.GetKey(KeyCode.LeftShift) && Application.isEditor ? 2 : 1; // acelera o player no editor com o shift pressionado

        if(stickDirection.magnitude > 0.9f)
        {
            if (boatSpeed < boatMaxSpeed)
            {
                boatSpeed += acceleration * Time.deltaTime;
            }

            controller.Move(new Vector3(x, 0, z) * Time.deltaTime * -boatSpeed * multiplier);
        }
        else
        {
            if (boatSpeed > 0)
            {
                boatSpeed -= acceleration * Time.deltaTime;
            }

            controller.Move(this.transform.TransformDirection(Vector3.forward) * -boatSpeed * Time.deltaTime);
        }
    }

    private void Rotate()
    {
        Vector3 rotationOffset = this.transform.TransformDirection(stickDirection) * boatTurnSpeed;
        rotationOffset.y = 0;
        controller.transform.forward += Vector3.Lerp(controller.transform.forward, rotationOffset, Time.deltaTime * 0.2f);
    }
}
