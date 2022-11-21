using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Tooltip("Imagem das costas da carta")] public Sprite cardBack;
    [Tooltip("Imagem da frente da carta")] public Sprite cardFront;
    private MemoryManager memoryManager;
    private int cardState = 0; // 0 = virada, 1 = revelada
    private Image cardImage; // imagem desta carta
    private Animator cardAnim; // imagem desta carta
    [HideInInspector]
    public int cardNumber;

    private void Start()
    {
        cardImage = this.GetComponentInChildren<Image>();
        cardAnim = this.GetComponentInChildren<Animator>();
        memoryManager = GameObject.Find("GameManager").GetComponent<MemoryManager>();
    }

    public void FlipBtn() // Metodo chamado ao tocar na carta
    {
        if (cardState == 0 && memoryManager.canPlay) // Confere se a carta esta virada para baixo
        {
            cardState = 1; // seta a carta para revelada
            cardAnim.SetTrigger("turnFaceUp"); // toca animacao para a carta virar para cima
            memoryManager.CardsClicked(this.gameObject.GetComponent<Card>());
        }
    }

    public void FlipCard() // metodo chamado na metade da animacao de virar a carta
    {
        if (cardState == 1) // caso a carta estava virada, agora estara revelada
        {
            cardImage.sprite = cardFront;
            cardImage.transform.GetChild(0).gameObject.SetActive(true); // ativa o texto pois a carta virou
        }
        else if (cardState == 0) // caso a carta estava revelada, agora estara normal
        {
            cardImage.sprite = cardBack;
            cardImage.transform.GetChild(0).gameObject.SetActive(false); // desativa o texto novamente
        }
    }

    public void BackFlip() // gira a carta para baixo, chamado quando o jogador erra o par de cartas
    {
        cardAnim.SetTrigger("turnFaceDown");
        cardState = 0; // seta a carta para virada
    }

}
