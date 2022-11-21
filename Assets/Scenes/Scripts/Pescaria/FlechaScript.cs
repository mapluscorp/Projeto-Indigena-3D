using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlechaScript : MonoBehaviour
{
    public Transform targetAngle;
    public Collider2D WaterCollider;
    public float Force;
    private Vector3 v_diff;
    private Vector3 InitialPosition;
    public static float atan2;
    private bool InAir = false;
    private float Dist;
    private bool Draggable = true;
    private bool Draggando = false;
    private Collider2D col;
    public PescariaScript pescariaScript;
    public GameObject handCursor;
    //private bool hasStarted = false; // vira bool na primeira vez que a flecha eh arrastada, para desativar o handCursor
    [Header("Sounds")]
    public AudioSource FlechaSource;
    public AudioSource ScoreSource;
    public AudioSource WaterDrop;
    public AudioClip IncorrectFish;
    public AudioClip CorrectFish;

    private void Start()
    {
        col = this.GetComponent<CapsuleCollider2D>();
        InAir = false; // Diz se a flecha esta no ar ou nao. Nesse caso, ela esta parada
        this.GetComponent<Rigidbody2D>().gravityScale = 0; // Quando a flecha esta parada, nao deve usar a gravidade
        InitialPosition = this.gameObject.transform.position; // Posicao inicial da flecha
    }

    private void FixedUpdate()
    {
        //if (!Draggando) WaterCollider.enabled = false; // Caso o jogador nao esteja puxando a flecha, o colisor da flecha com a agua eh desativado
        //else WaterCollider.enabled = true; // Ativa a colisao da flecha com a agua
        Dist = Vector2.Distance(this.gameObject.transform.position, targetAngle.position);
        v_diff = (targetAngle.position - this.transform.position);
        atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        if (InAir == false)
            this.transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
        if (!GetComponent<Renderer>().isVisible) // caso a flecha nao esteja mais na tela
        {
            WaterDrop.Play(); // toca o som da flecha caindo na agua
            Voltar_Flecha();
        }
        if(Draggando && this.gameObject.transform.position.y < 1.3f) // Caso a posicao da flecha encoste na agua
        {
            Draggable = false;
            Voltar_Flecha();
        }
    }

    private void OnMouseUp()
    {
        if (Draggable)
        {
            FlechaSource.Play(); // Toca o som da flecha
            this.GetComponent<Rigidbody2D>().fixedAngle = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
            this.GetComponent<Rigidbody2D>().AddForce(this.GetComponent<Rigidbody2D>().transform.right * Force * Dist, ForceMode2D.Impulse); // Impulsiona a flecha para a direcao correta
            InAir = true;
        }
        Cursor.visible = true; // torna a seta invisivel
        Draggable = true; Draggando = false; // Nao esta mais arrastando
    }

    void OnMouseDrag()
    {
        if (Draggable && GetComponent<Renderer>().isVisible) // Confere se a flecha esta apta a ser arrastada
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
            Cursor.visible = false; // Cursor eh invisivel enquanto a flecha esta sendo arrastada
            Draggando = true; // Esta arrastando a flecha
            col.enabled = false;
        }
        else
        {
            Draggable = false;
            Voltar_Flecha();
        }
        handCursor.SetActive(false); // faz com que o handCursor pare de aparecer
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fish") && GetComponent<Renderer>().isVisible) // Compara se o objeto acertado eh um peixe
        {
            if (InAir) // Se a flecha estiver no ar destroi o peixe
            {
                Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
                otherRB.isKinematic = true; // Faz com que o peixe nao se mova
                otherRB.velocity = new Vector2(0, 0); // Torna a velocidade zero
                StartCoroutine(PiscarPeixe(other, 5)); // Comeca a piscar o peixe
            }
            else Draggable = false;
            pescariaScript.AtualizarPeixinho(false);
            ScoreSource.clip = IncorrectFish;
            ScoreSource.Play();
        }
        else if (other.gameObject.CompareTag("CorrectFish") && GetComponent<Renderer>().isVisible) // Compara se eh o peixe objetivo
        {
            if (InAir)
            {
                Rigidbody2D otherRB = other.gameObject.GetComponent<Rigidbody2D>();
                otherRB.isKinematic = true;
                otherRB.velocity = new Vector2(0, 0);
                StartCoroutine(PiscarPeixe(other, 5));
            }
            else Draggable = false;
            pescariaScript.AtualizarPeixinho(true);
            ScoreSource.clip = CorrectFish;
            ScoreSource.Play();
        }
        Voltar_Flecha();
    }

    IEnumerator PiscarPeixe(Collision2D other, int cont)
    {
        if (cont % 2 == 1 && cont > 0)
            other.gameObject.GetComponent<Renderer>().enabled = false;
        else if(cont % 2 == 0 && cont > 0)
            other.gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        if (cont == 0)
            Destroy(other.gameObject);
        else StartCoroutine(PiscarPeixe(other, cont - 1));
    }

    void Voltar_Flecha() // Volta a flecha para a posicao inicial
    {
        this.transform.position = InitialPosition;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        InAir = false;
        col.enabled = true;
        Draggando = false;
        Cursor.visible = true;
    }
}
