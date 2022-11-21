using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public class PuzzleGameScript : MonoBehaviour
{
    public static bool Check = false;
    public static Transform SlotTransform;
    public GameObject[] desactivateAtStart;
    public GameObject[] activateAtStart;
    public GameObject[] Slots6;
    [SerializeField]
    private GameObject[] Pieces; // Vetor que ira manipular as pecas
    [SerializeField]
    private GameObject[] Slots; // Vetor que ira manipular os slots
    public GameObject[] Slots_3;
    public GameObject[] Slots_4;
    public GameObject[] Slots_5;
    public GameObject[] Slots_6;
    public GameObject[] Pieces_3;
    public GameObject[] Pieces_4;
    public GameObject[] Pieces_5;
    public GameObject[] Pieces_6;
    public GameObject WinnerScreen;
    public Sprite[] MiniBackgroundSprites;
    private Sprite[] Pieces_Images;
    public Image MiniBackground;
    private int[] ListaJaForam;
    private int Complete = 0;
    private float TempoInicial;
    //public Text TimeText;
    private bool OnlyOnce = false;
    public static Transform Left_reference;
    public static Transform Right_reference;
    public Image finalImage;
    private int offset;

    private static string desenho;
    private static bool exibirTelaSelecionarDesenho = true;
    public GameObject telaSelecionarDesenho;
    public GameObject telaInicial;

    public int tamanhoGrade;
    public static int tamanhoSlot;
    private void Start()
    {
        foreach (GameObject g in desactivateAtStart)
            g.SetActive(false);
        foreach (GameObject g in activateAtStart)
            g.SetActive(true);
        Input.multiTouchEnabled = false;
        Left_reference = GameObject.FindGameObjectWithTag("Esquerda").GetComponent<Transform>(); // Impede que as pecas ja colocadas sejam movidas
        Right_reference = GameObject.FindGameObjectWithTag("Direita").GetComponent<Transform>();

        if (!exibirTelaSelecionarDesenho)
        {
            telaSelecionarDesenho.SetActive(false);
            telaInicial.SetActive(false);
        }
        else
        {
            telaSelecionarDesenho.SetActive(true);
            telaInicial.SetActive(true);
        }
    }

    void Update()
    {
        CheckSlot();
    }

    public void SetarDesenho(string nome)
    {
        desenho = nome;
    }

    public void SetarDificuldade(int valor)
    {
        tamanhoGrade = valor; // Tamanho da grade passa a ser o valor escolhido pelo jogador
        offset = (int)((300 / valor) * Camera.main.aspect); // Ajusta o offset para detectar a posicao das pecas
        MiniBackground.sprite = MiniBackgroundSprites[valor-3]; // Troca a imagem do miniBackground para a correspondente a dificuldade escolhida
        switch (valor)
        {
            case 3:
                Pieces = Pieces_3; // As pecas a serem utilizadas passam a ser as do campo 3x3
                Slots = Slots_3;
                tamanhoSlot = 330;
                MiniBackground.transform.localPosition = new Vector2(0.68f, -40.34f);
                break;
            case 4:
                Pieces = Pieces_4; // As pecas a serem utilizadas passam a ser as do campo 3x3
                Slots = Slots_4;
                tamanhoSlot = 246;
                MiniBackground.transform.localPosition = new Vector2(-0.3f, -38.6f);
                break;
            case 5:
                Pieces = Pieces_5;
                Slots = Slots_5;
                tamanhoSlot = 196;
                MiniBackground.transform.localPosition = new Vector2(2.9f, -39.5f);
                break;
            case 6:
                Pieces = Pieces_6;
                Slots = Slots_6;
                tamanhoSlot = 160;
                MiniBackground.transform.localPosition = new Vector2(2.57f, -41.94f);
                break;
        }
        for(short i=3; i<=6; i++)
        {
            GameObject PiecesGroup = GameObject.Find("Pieces_" + i.ToString());
            if(i != valor) // Deixa ativado apenas o conjunto de pecas da dificuldade escolhida
                PiecesGroup.SetActive(false);
        }
        SetarImagens(valor);
        ListaJaForam = new int[(int)(Mathf.Pow(valor,2))];
        for (int k = 0; k < Mathf.Pow(tamanhoGrade,2); k++)
            ListaJaForam[k] = 0;
        TempoInicial = Time.realtimeSinceStartup;
    }

    private void SetarImagens(int tam)
    {
        this.GetComponent<TextureManager>().CutTexture(tam, Pieces, desenho);

        for (int i = 0; i < Pieces.Length; i++) // Embaralha o vetor
        {
            Vector3 temp = Pieces[i].transform.position;
            int randomIndex = Random.Range(0, (int)Mathf.Pow(tamanhoGrade, 2));

            Pieces[i].transform.position = Pieces[randomIndex].transform.position;
            Pieces[randomIndex].transform.position = temp;
        }
    }

    public void ReiniciarMesmoDesenho()
    {
        exibirTelaSelecionarDesenho = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Reiniciar()
    {
        exibirTelaSelecionarDesenho = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CheckSlot()
    {
        for (int i = 0; i < Mathf.Pow(tamanhoGrade, 2); i++)
        {
            if (Slots[i].activeSelf && (Mathf.Abs((Mathf.Abs(Slots[i].transform.position.x) - Mathf.Abs(Pieces[i].transform.position.x))) <= offset) && (Mathf.Abs((Mathf.Abs(Slots[i].transform.position.y) - Mathf.Abs(Pieces[i].transform.position.y))) <= offset) && ListaJaForam[i] != 1)
            {
                //print("Lugar correto");
                Check = true;
                SlotTransform = Slots[i].transform;
                if (DragAndDropPieces.Acertou == true) // Se soltou no lugar certo la no outro script
                {
                    //print("Acertou");
                    ListaJaForam[i] = 1;
                    Complete++;
                }
                return;
            }
            else Check = false;
            Check_FimDeJogo();
        }
    }

    private void Check_FimDeJogo() // Confere se chegou ao fim do jogo
    {
        if ((Complete == Mathf.Pow(tamanhoGrade, 2)) && !OnlyOnce) // Caso o numero de pecas corretas seja igual ao tamanho da grade ao quadrado
        {
            finalImage.sprite = Resources.Load<Sprite>(desenho); // A imagem final sera a escolhida pelo usuario la no comeco
            WinnerScreen.SetActive(true);
            //TimeText.text = "Tempo decorrido: " + Math.Round(Time.realtimeSinceStartup - TempoInicial, 2) + " segundos";
            OnlyOnce = true; // Para que a tela final seja chamada apenas uma vez
        }
    }

}
