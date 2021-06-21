using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    public GameObject[] ActivateAtStart;
    public GameObject[] cards;
    public GameObject[] cards_12;
    public GameObject[] cards_20;
    public GameObject[] cards_28;
    public Sprite[] fruitSprites;
    public Sprite[] nameSprites;
    public string[] portuguesTexts;
    public string[] kaingangTexts;
    public GameObject WinnerScreen;
    public AudioSource correctSource;
    private int fruit = 0;
    private int name = 0;
    private int binary = 0;
    private Card[] cardsClicked;
    private int cardClickedOffset = 0;
    public bool canPlay = true;
    private int acertos = 0;
    private int maxAcertos = 0;
    void Start()
    {
        QualitySettings.SetQualityLevel(2);
        Time.timeScale = 1;
        Input.multiTouchEnabled = false;
        cardsClicked = new Card[2]; // vetor que ira guardar as duas ultimas cartas clicadas
        foreach (GameObject g in ActivateAtStart)
            g.SetActive(true);
    }

    private void Initialize(Card card) // metodo que dara a imagem para as cartas
    {
        if(binary == 0) // carta recebera uma imagem de fruta
        {
            card.cardFront = fruitSprites[fruit];
            card.cardNumber = fruit; // carta recebe o numero dela
            binary = 1; fruit++; // revesa o binario e incrementa o int da fruta
        } else if (binary == 1) // carta recebera uma imagem de nome
        {
            //card.cardFront = nameSprites[name]; /// deixa desativado, a carta usara a frente vazia que ja esta nela
            card.cardNumber = name; // carta recebe o numero dela
            if(PlayerPrefs.GetInt("Idioma") == 0 ) // 0 == portugues
                card.GetComponentInChildren<Text>().text = portuguesTexts[name]; // seta o texto com a palavra
            else if (PlayerPrefs.GetInt("Idioma") == 1) // 1 == kaingang
                card.GetComponentInChildren<Text>().text = kaingangTexts[name]; // seta o texto com a palavra
            card.transform.GetChild(0).gameObject.SetActive(false); // deixa o texto desativado
            binary = 0; name++; // revesa o binario e incrementa o int do nome
        }
    }

    private void ScrambleCards() // embaralha as cartas
    {
        /*foreach (GameObject card in cards) // cada carta recebe uma imagem
        {
            print("Embaralhando carta: " + card);
            int rand = Random.Range(0, cards.Length);
            Vector3 pos = card.GetComponentInParent<Transform>().position;
            card.GetComponentInParent<Transform>().position = cards[rand].GetComponentInParent<Transform>().position;
            cards[rand].GetComponentInParent<Transform>().position = pos;
        }*/
        foreach (GameObject card in cards) // cada carta recebe uma imagem
        {
            print("Embaralhando carta: " + card);
            int rand = Random.Range(0, cards.Length);
            Vector3 pos = card.transform.position;
            card.transform.position = cards[rand].transform.position;
            cards[rand].transform.position = pos;
        }
    }

    public void CardsClicked(Card card) // recebe as duas ultimas cartas clicadas
    {
        cardsClicked[cardClickedOffset] = card; // recebe o script Card da ultima carta clicada
        if(cardClickedOffset == 0) // caso seja a primeira carda das duas, incrementa o offset
            cardClickedOffset++;
        else // duas cartas foram clicadas
        {
            CompareCards(); // compara as duas ultimas cartas clicadas
            cardClickedOffset = 0; // retorna o offset para zero
        }
    }

    private void CompareCards() // compara as duas ultimas cartas clicadas
    {
        if(cardsClicked[0].cardNumber == cardsClicked[1].cardNumber) // compara se as duas cartas sao equivalentes
        {
            StartCoroutine(PlayCorrectSound());
            acertos++; // incrementa a quantia de acertos
            if (acertos == maxAcertos) // confere se o jogo acabou
                StartCoroutine(ShowWinnerScreen()); // espera 1seg antes de exibir a tela de vitoria
        } else
        {
            canPlay = false; // faz o jogador esperar ateh as duas cartas virarem
            StartCoroutine(TurnCardsFaceDown()); // Espera 1 segundo antes de virar as cartas para baixo
        }
    }

    IEnumerator PlayCorrectSound()
    {
        yield return new WaitForSeconds(0.5f);
        correctSource.Play();
    }

    IEnumerator ShowWinnerScreen() // exibe a tela de vitoria apos 1 seg, para dar tempo da ultima carta virar pra cima
    {
        yield return new WaitForSeconds(1);
        WinnerScreen.SetActive(true);
    }

    IEnumerator TurnCardsFaceDown() // vira as cartas para baixo novamente pois o jogador errou
    {
        yield return new WaitForSeconds(1);
        cardsClicked[0].BackFlip();
        cardsClicked[1].BackFlip();
        canPlay = true; // jogador pode escolher cartas novamente
    }

    public void SelectDifficulty(int valor)
    {
        switch (valor)
        {
            case 0:
                cards = cards_12;
                maxAcertos = 6;
                break;
            case 1:
                cards = cards_20;
                maxAcertos = 10;
                break;
            case 2:
                cards = cards_28;
                maxAcertos = 14;
                break;
            default:
                Debug.LogError("Valor de dificuldade invalido");
                break;
        }
        foreach (GameObject card in cards) // cada carta recebe uma imagem
        {
            Initialize(card.GetComponent<Card>());
        }
        ScrambleCards(); // embaralha as cartas
    }

    public void Restart() // Reinicia o jogo da memoria
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*public void Home() // Vai para o menu principal do jogo
    {
        PlayerPrefs.SetInt("HomeController", 1);
        SceneManager.LoadScene("Menu");
    }*/

    void Update()
    {
        RaycastHit2D hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (hit = Physics2D.Raycast(ray.origin, new Vector2(0, 0)))
            Debug.Log(hit.collider.name);
    }

}
