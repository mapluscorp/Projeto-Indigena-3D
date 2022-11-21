using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndDrop_Letras : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private WordGameScript gameScript;
    public Transform[] destinos; // slots para onde essa peca deve ir
    private Vector3 startPosition; // posicao inicial da peca
    private bool canDrag; // controla se a peca pode ser arrastada ou nao
    private int offset = (int)((Screen.width * Screen.height) / 30000); // seta o offset de acordo com a resolucao 

    private Color defaultColor = new Color32(0xFF, 0xE0, 0x05, 0xFF);
    private Color redColor = new Color32(0xDE, 0x2F, 0x2B, 0xFF);

    private Animator anim;

    private void Start()
    {
        gameScript = GameObject.Find("GameManager").GetComponent<WordGameScript>();
        startPosition = this.transform.position;
        anim = this.GetComponent<Animator>();
        //slots = GameObject.Find("Canvas").transform.Find("Slots").GetComponentsInChildren<Transform>();
    }

    public void OnPointerEnter(PointerEventData eventData) // Mouse sobre o botao. Animacao over da Letra
    {
        if (!anim.GetBool("Locked") && !anim.GetBool("Over"))
            anim.SetBool("Over", true);
    }

    public void OnPointerExit(PointerEventData eventData) // Mouse saiu do botao. Sprite volta ao normal
    {
        if(anim.GetBool("Over"))
            anim.SetBool("Over", false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.mousePosition.x > Screen.width / 2) // Confere se o jogador esta tentando arrastar uma peca das alternativas
            canDrag = true;
        else canDrag = false;
        WordGameScript.selected = this.gameObject;
        startPosition = transform.position;
        transform.SetParent(transform.parent);
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(canDrag)
            transform.position = Input.mousePosition;
        else transform.position = startPosition;
        transform.SetParent(transform.parent);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (Transform dest in destinos)
        {
            if (Vector3.Distance(this.transform.position, dest.position) < offset) // peca esta proxima
            {
                this.transform.position = dest.position;
                dest.GetComponent<WordSlotScript>().activated = true; // seta o slot como correto
                gameScript.RegisterCorrect(); // adiciona 1 nas letras acertadas
                anim.SetBool("Locked", true); // animacao de que acertou a posicao da peca
                return;
            } else if (Input.mousePosition.x > Screen.width / 2 || Input.mousePosition.y > Screen.height / 3) // peca nao estava proxima
            {
                WordGameScript.overSlot = null; // peca nao foi soltada proxima de slot, nao ha slot over
            }
        }

        transform.position = startPosition;

        transform.SetParent(transform.parent);

        if (WordGameScript.overSlot != null)
        {
            StartCoroutine(RedLightBlinking(WordGameScript.overSlot));
            WordGameScript.RegistrarErro(); // incrementa 1 erro
        }

    }

    public void ResetarPosicao() // Metodo chamado para por as pecas de volta onde estavam ao reiniciar a fase
    {
        transform.position = startPosition;
    }

    IEnumerator RedLightBlinking(GameObject slot)
    {
        slot.GetComponent<WordSlotScript>().blinking = true; // impede que o outro script sobrescreva a cor
        yield return new WaitForSeconds(0.2f);
        slot.transform.GetChild(0).GetComponent<Image>().color = redColor;
        yield return new WaitForSeconds(0.2f);
        slot.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        yield return new WaitForSeconds(0.2f);
        slot.transform.GetChild(0).GetComponent<Image>().color = redColor;
        yield return new WaitForSeconds(0.2f);
        slot.transform.GetChild(0).GetComponent<Image>().color = defaultColor;
        yield return new WaitForSeconds(0.2f);
        slot.GetComponent<WordSlotScript>().blinking = false; // volta o outro script para a rotida normal
        WordGameScript.overSlot = null;
    }

}