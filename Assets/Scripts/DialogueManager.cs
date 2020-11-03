using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public List<string> sentences;
    public Image characterSprite;
    public Text characterName;

    public Text dialogueText;
    public GameObject ContinueButton;
    public GameObject backButton;

    private short currentIndex;
    private Animator anim;
    private GameObject display;

    [HideInInspector]
    public TaskMaker taskMaker;

    void Start()
    {
        currentIndex = 0;
        anim = this.transform.Find("Display/Dialogue Display").GetComponent<Animator>();
        display = this.transform.Find("Display").gameObject;
    }

    private void Update()
    {
        /*if (!beingHandled && ContinueButton.activeSelf && (Input.GetKeyDown("return") || Input.GetKeyDown("enter")))
        {
            Debug.Log("Pressionou enter");
            DisplayNextSentence();
        }*/
    }

    public void Begin()
    {
        anim.SetTrigger("Open");
        Continue();
    }

    public void Continue()
    {
        if (currentIndex < sentences.Count) { DisplayNextSentence(); }
        else { StartCoroutine(Close()); }
    }

    IEnumerator Close()
    {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        ResetSentences();
        display.SetActive(false);
        if(taskMaker != null) { taskMaker.GenerateTask(); } // gera a task na UI
        taskMaker = null;
    }

    public void DisplayNextSentence()
    {
        backButton.SetActive(currentIndex > 0); // nao exibe o botao de voltar na primeira mensagem

        StopAllCoroutines(); // Para de exibir todas as mensagens

        StartCoroutine(TypeSentence(sentences[currentIndex])); // Comeca a exibir a mensagem atual letra por letra

        currentIndex++; // Incrementa o numero da mensagem atual
    }

    public void DisplayPreviousSentence()
    {
        currentIndex -= 2; // Decrementa o index para a mensagem anterior
        if (currentIndex > 0) // Caso essa nao seja a primeira mensagem, ativa o botao de voltar
            backButton.SetActive(true);
        else if (currentIndex == 0)
            backButton.SetActive(false);

        backButton.SetActive(currentIndex > 0); // nao exibe o botao de voltar na primeira mensagem

        StopAllCoroutines(); // Para de exibir todas as mensagens

        StartCoroutine(TypeSentence(sentences[currentIndex])); // Comeca a exibir a mensagem atual letra por letra

        currentIndex++; // Incrementa o numero da mensagem atual

    }

    IEnumerator TypeSentence(string sentence) // Exibe a mensagem letra por letra
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f); // Intervalo de dempo entre cada letra
        }
    }

    public void ResetSentences()
    {
        currentIndex = 0;
    }

}
