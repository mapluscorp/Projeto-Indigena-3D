using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatManager : MonoBehaviour
{
    [Header("References")]
    public Animator playerAnim;
    public Camera mainCamera;
    public DynamicJoystick joytick;
    public float boatSpeed = 3f;
    public float boatTurnSpeed = 2f;
    public Transform targetTransform;

    private CharacterController controller;
    [HideInInspector]
    public Vector3 stickDirection;

    public bool IsEnabled { get; set; }

    private void Start()
    {
        IsEnabled = false;
        controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (IsEnabled)
        {
            playerAnim.transform.root.parent = this.transform.parent;
            playerAnim.transform.position = targetTransform.position;
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        float horizontal = Mathf.Clamp(Input.GetAxis("Horizontal") + joytick.Horizontal, -1, 1);
        float vertical = Mathf.Clamp(Input.GetAxis("Vertical") + joytick.Vertical, -1, 1);

        stickDirection = new Vector3(horizontal, 0, vertical);

        playerAnim.SetBool("Paddling", stickDirection.magnitude > 0.1f);
        playerAnim.SetFloat("Horizontal", horizontal);

        float x = this.transform.TransformDirection(stickDirection).x;
        float z = this.transform.TransformDirection(stickDirection).z;
        if (x > 1) x = 1; // assegura que o jogador nao ira se mover mais rapido em diagonal
        if (z > 1) z = 1;

        float multiplier = Input.GetKey(KeyCode.LeftShift) && Application.isEditor ? 2 : 1; // acelera o player no editor com o shift pressionado

        controller.Move(new Vector3(x, 0, z) * Time.deltaTime * -boatSpeed * multiplier);
    }

    private void Rotate()
    {
        Vector3 rotationOffset = this.transform.TransformDirection(stickDirection) * boatTurnSpeed;
        rotationOffset.y = 0;
        controller.transform.forward += Vector3.Lerp(controller.transform.forward, rotationOffset, Time.deltaTime * 0.2f);
    }
}
