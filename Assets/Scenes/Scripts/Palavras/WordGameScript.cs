using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordGameScript : MonoBehaviour
{
    public GameObject[] activateAtStart;
    public GameObject[] fases;
    public static GameObject winnerScreen;
    public static GameObject finalScreen;
    public static GameObject selected;
    public static GameObject overSlot;
    public GameObject[] stars;
    public static GameObject[] staticStars;
    public GameObject[] estrelasProgresso;
    public GameObject[] miniStarsGroups;

    public static int objective;
    public static int corretos;
    public static int erros = 0;

    private int currentFase = 0;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        winnerScreen = GameObject.Find("Canvas").transform.Find("WinnerScreen").gameObject;
        finalScreen = GameObject.Find("Canvas").transform.Find("FinalScreen").gameObject;
        staticStars = stars;
        corretos = 0;
    }

    private void Start()
    {
        winnerScreen.SetActive(false);
        foreach (GameObject fase in fases) // desativa todas as fases ao iniciar o jogo
            fase.SetActive(false);
        foreach (GameObject g in activateAtStart)
            g.SetActive(true);
    }

    public void RegisterCorrect()
    {
        corretos++;
        if(corretos == objective) // numero de acertos corresponde ao numero de slots
        {
            winnerScreen.SetActive(true); // ativa a tela de fim de fase
            foreach (GameObject star in staticStars) // desativa todas as estrelas
                star.SetActive(false);
            StartCoroutine(ShowStars()); // comeca a exibir as estrelas
        }
    }

    IEnumerator ShowStars() // exibe as estrelas com intervalo entre uma e outra
    {
        if (erros >= 3) yield break; // caso errou mais de 3, apenas retorna

        if (erros == 1)
        {
            staticStars[0].SetActive(true);
            yield return new WaitForSeconds(0.4f);
            staticStars[1].SetActive(true);

        } else if (erros == 2)
        {
            staticStars[0].SetActive(true);
        } else if (erros == 0)
        {
            staticStars[0].SetActive(true);
            yield return new WaitForSeconds(0.4f);
            staticStars[1].SetActive(true);
            yield return new WaitForSeconds(0.4f);
            staticStars[2].SetActive(true);
        }
    }

    public void ProximaFase() // quando clica para a proxima fase na tela de final de fase
    {
        fases[currentFase].SetActive(false); // desativa a fase que acabou de passar
        SetMiniStarsGroup(currentFase); // seta es estrelinhas na dela final do jogo
        currentFase++;
        winnerScreen.SetActive(false);
        if (currentFase < 10) // caso essa nao foi a ultima fase
        {
            fases[currentFase].SetActive(true);
            estrelasProgresso[currentFase].SetActive(true); // progresso la em cima
        }
        else // caso foi a ultima fase
        {
            finalScreen.SetActive(true);
            return;
        }
    }

    private void SetMiniStarsGroup(int current) // ja seta as estrelinhas da tela final a cada final de fase
    {
        if (erros == 0) return; // caso nao errou, todas as estrelas ficam ativas

        if(erros == 1)
        {
            miniStarsGroups[current].transform.GetChild(2).gameObject.SetActive(false);
            return;
        }

        if (erros == 2)
        {
            miniStarsGroups[current].transform.GetChild(2).gameObject.SetActive(false);
            miniStarsGroups[current].transform.GetChild(1).gameObject.SetActive(false);
            return;
        }

        if (erros >= 3)
        {
            miniStarsGroups[current].transform.GetChild(2).gameObject.SetActive(false);
            miniStarsGroups[current].transform.GetChild(1).gameObject.SetActive(false);
            miniStarsGroups[current].transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

    }

    public void ReiniciarFase() // reinicia a fase que acabou de ser jogada
    {
        foreach (GameObject fase in fases) // desativa todas as fases
            fase.SetActive(false);
        fases[currentFase].SetActive(true);

        DragAndDrop_Letras[] pecas = fases[currentFase].transform.Find("Letras").transform.GetComponentsInChildren<DragAndDrop_Letras>();
        foreach (DragAndDrop_Letras peca in pecas)
            peca.ResetarPosicao();

        WordSlotScript[] slots = fases[currentFase].transform.Find("Slots").transform.GetComponentsInChildren<WordSlotScript>();
        foreach (WordSlotScript slot in slots)
            slot.ResetarBarrinha();

        winnerScreen.SetActive(false);
        corretos = 0;
    }

    public static void RegistrarErro()
    {
        erros++;
    }

}
