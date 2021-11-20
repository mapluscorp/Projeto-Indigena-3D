using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityForce;

    private PlayerMovementManager movement;
    private PlayerAnimationManager animation;

    public bool IsAlive { get; set; }
    public bool IsGrounded { get; set; }
    public bool IsMoving { get { return animation.AnimSpeed >= 0.1f; } }
    public bool CanInteract { get; set; }
    public float Speed { get { return playerSpeed; } }
    public float JumpForce { get { return jumpForce; } }

    private static PlayerController _instance;
    public static PlayerController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogError("Not supposed to have more than one PlayerController on scene");
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        movement = this.GetComponent<PlayerMovementManager>();
        animation = this.GetComponent<PlayerAnimationManager>();
    }
    void FixedUpdate()
    {
        //RayCaster();
        movement.Move();
        movement.Gravity(gravityForce);
        movement.Rotate();
        //Death();
    }

    void Update()
    {
        movement.Jump();
    }

    /*
    
    private void SetBody()
    {
        kameGirl.SetActive(false);
        kameBoy.SetActive(false);
        kairuGirl.SetActive(false);
        kairuBoy.SetActive(false);

        int selectedSlot = PlayerPrefs.GetInt("SelectedSlot");
        if(PlayerPrefs.GetString("Type" + selectedSlot) == "Kame")
        {
            if (PlayerPrefs.GetString("Sex" + selectedSlot) == "Menino")
            {
                kameBoy.SetActive(true);
            }
            else if (PlayerPrefs.GetString("Sex" + selectedSlot) == "Menina")
            {
                kameGirl.SetActive(true);
            }
        }
        else if (PlayerPrefs.GetString("Type" + selectedSlot) == "Kairu")
        {
            if (PlayerPrefs.GetString("Sex" + selectedSlot) == "Menino")
            {
                kairuBoy.SetActive(true);
            }
            else if (PlayerPrefs.GetString("Sex" + selectedSlot) == "Menina")
            {
                kairuGirl.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Body Error");
        }
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
    
     private void RayCaster()
    {
        if (Physics.Raycast(transform.GetChild(0).position, transform.TransformDirection(Vector3.down), out groundHit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.GetChild(0).position, transform.TransformDirection(Vector3.down) * groundHit.distance, Color.yellow);
            GroundTag = groundHit.collider.tag;
        }
    }
     
     */
}
