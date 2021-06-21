using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumeralManager : MonoBehaviour
{
    public GameObject[] enableAtStart;
    public GameObject[] deactivateAtStart;
    public GameObject[] stagesTransitions;

    public int[] answers; // para cada questao, armazena se foi erro ou acerto
    public GameObject finalResultsScreen; // tela final, exibe os resultados
    public GameObject blocker; // ativado apos cada resposta selecionada, para impedir multipla escolha

    public Image[] imageSlots; // resultados
    public Sprite correctSprite; // certinho para por na tela final
    public Sprite incorrectSprite; // x para por na tela final

    [Header("Sounds")]
    public AudioClip correctClip;
    public AudioClip incorrectClip;
    public AudioSource source;

    private int currentStage = 0;

    void Start()
    {
        answers = new int[5];
        foreach (GameObject g in enableAtStart)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in deactivateAtStart)
        {
            g.SetActive(false);
        }
    }

    public void RegisterAnswer(bool value)
    {
        source.clip = value == true ? correctClip : incorrectClip;
        source.Play();
        answers[currentStage] = value ? 1 : 0;
        NextStage();
    }

    private void NextStage()
    {
        currentStage++;
        StartCoroutine(CallNextStageTransition());
    }

    IEnumerator CallNextStageTransition()
    {
        blocker.SetActive(true);
        imageSlots[currentStage - 1].sprite = answers[currentStage - 1] == 1 ? correctSprite : incorrectSprite;
        yield return new WaitForSeconds(1);
        if (currentStage < 5)
        {
            stagesTransitions[currentStage].SetActive(true);
        }
        else
        {
            blocker.SetActive(false);
            finalResultsScreen.SetActive(true);
        }
    }

}
