using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour // responsavel pela movimentacao do player
{
    [Header("References")]
    public Camera mainCamera;
    public DynamicJoystick joytick;
    public GameObject waterRipple;

    [Header("Control")]
    public float playerSpeed = 1;

    private CharacterController controller;
    private Animator anim;
    [HideInInspector]
    public Vector3 stickDirection;

    RaycastHit groundHit;

    private float jumpForce = 0.1f;

    public bool CanInteract { get; set; }
    public bool CanUseMachete { get; set; }
    public bool IsGravityOn { get; set; }
    public string GroundTag { get; set; }
    public bool IsJumping { get; set; }
    public bool IsAlive { get; set; }
    void Start()
    {
        controller = this.GetComponentInChildren<CharacterController>();
        anim = this.GetComponentInChildren<Animator>();
        CanInteract = true;
        CanUseMachete = true;
        IsGravityOn = true;
        IsAlive = true;
        if(PlayerPrefs.HasKey("SpawnPosition"))
        {
            Spawn();
        }
    }

    void FixedUpdate()
    {
        RayCaster();
        Move();
        Rotate();
        Death();
    }

    void Update()
    {
        Jump();
    }

    private void Death()
    {
        /*if(anim.transform.position.y < -2 && IsAlive)
        {
            IsAlive = false;
            print("Dead");
            StartCoroutine(Respawn());
        }*/
    }

    IEnumerator Respawn()
    {
        CanInteract = false;
        Vector3 pos = anim.transform.position; pos.y = -2f;
        Instantiate(waterRipple, pos, Quaternion.identity);
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        anim.transform.position = CheckPointSystem.CurrentSpawnPosition;
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        IsAlive = true;
        CanInteract = true;
    }

    private void Spawn()
    {
        CanInteract = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        anim.transform.position = CheckPointSystem.CurrentSpawnPosition;
        this.transform.GetChild(0).gameObject.SetActive(true);
        IsAlive = true;
        CanInteract = true;
    }

    private void Jump()
    {
        if(InputManager.Jump() && !IsJumping && controller.isGrounded)
        {
            anim.SetTrigger("Jump");
            StartCoroutine(JumpingStateManager());
        }
    }

    IEnumerator JumpingStateManager()
    {
        IsJumping = true;
        yield return new WaitForSeconds(0.5f);
        IsJumping = false;
    }

    private void Move()
    {
        if(!IsAlive || !CanInteract) { return; }

        //if(!IsAlive) { controller.Move(new Vector3(0, -groundHit.distance, 0) * Time.deltaTime * playerSpeed); return; }

        //if (!CanInteract) { return; } // Nao move caso nao possa interagir com os controles

        float horizontal = Mathf.Clamp(InputManager.Horizontal() + joytick.Horizontal, -1, 1);
        float vertical = Mathf.Clamp(InputManager.Vertical() + joytick.Vertical, -1, 1);

        stickDirection = new Vector3(horizontal, 0, vertical);

        float x = mainCamera.transform.TransformDirection(stickDirection).x;
        float z = mainCamera.transform.TransformDirection(stickDirection).z;
        if (x > 1) x = 1; // assegura que o jogador nao ira se mover mais rapido em diagonal
        if (z > 1) z = 1;

        float multiplier = Input.GetKey(KeyCode.LeftShift) && Application.isEditor ? 2 : 1; // acelera o player no editor com o shift pressionado

        IsGravityOn = !anim.GetCurrentAnimatorStateInfo(0).IsName("Jump");

        controller.Move(new Vector3(x, !IsJumping ? (-groundHit.distance) : Mathf.Sqrt(jumpForce), z) * Time.deltaTime * playerSpeed * multiplier);
        anim.SetFloat("PlayerSpeed", Vector3.ClampMagnitude(stickDirection, 1).magnitude, 0.05f, Time.deltaTime); // clamp para limitar a 1, visto que a diagonal seria de 1.4
    }

    private void Rotate()
    {
        if (!CanInteract) { return; }

        Vector3 rotationOffset = mainCamera.transform.TransformDirection(stickDirection) * 4f;
        rotationOffset.y = 0;
        controller.transform.forward += Vector3.Lerp(controller.transform.forward, rotationOffset, Time.deltaTime * 30f);
    }

    private void RayCaster()
    {
        if (Physics.Raycast(transform.GetChild(0).position, transform.TransformDirection(Vector3.down), out groundHit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.GetChild(0).position, transform.TransformDirection(Vector3.down) * groundHit.distance, Color.yellow);
            GroundTag = groundHit.collider.tag;
        }
    }

    public void SavePlayerPosition()
    {
        PlayerPrefsX.SetVector3("SpawnPosition", anim.transform.position);
    }

}
