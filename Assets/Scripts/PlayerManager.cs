using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;

    [Header("Control")]
    public float playerSpeed = 1;

    private CharacterController controller;
    private Animator anim;
    private Vector3 stickDirection;

    RaycastHit hit;

    public bool CanMove { get; set; }

    void Start()
    {
        controller = this.GetComponentInChildren<CharacterController>();
        anim = this.GetComponentInChildren<Animator>();
        CanMove = true;
    }

    void Update()
    {
        RayCaster();
        Move();
        Rotate();
    }

    private void Move()
    {
        if (!CanMove) { return; }

        stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        float x = mainCamera.transform.TransformDirection(stickDirection).x;
        float z = mainCamera.transform.TransformDirection(stickDirection).z;
        if (x > 1) x = 1; // assegura que o jogador nao ira se mover mais rapido em diagonal
        if (z > 1) z = 1;

        controller.Move(new Vector3(x,-hit.distance, z) * Time.deltaTime * playerSpeed);
        anim.SetFloat("PlayerSpeed", Vector3.ClampMagnitude(stickDirection, 1).magnitude, 0.05f, Time.deltaTime); // clamp para limitar a 1, visto que a diagonal seria de 1.4
    }

    private void Rotate()
    {
        if (!CanMove) { return; }

        Vector3 rotationOffset = mainCamera.transform.TransformDirection(stickDirection) * 4f;
        rotationOffset.y = 0;
        controller.transform.forward += Vector3.Lerp(controller.transform.forward, rotationOffset, Time.deltaTime * 30f);
    }

    private void RayCaster()
    {
        if (Physics.Raycast(transform.GetChild(0).position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.GetChild(0).position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
        }
    }
}
