using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FishingDialogueManager : MonoBehaviour
{
    public string[] portugues;
    public string[] kaingang;
    public string[] english;
    public string[] arrayText;
    public GameObject[] imagesToShow;
    public Text dialogueText;
    public Text indexText;
    //private Queue<string> sentences;
    public GameObject ContinueButton;
    public GameObject playButton;
    public GameObject backButton;
    private bool beingHandled;
    private short currentIndex;

    void Start()
    {
        currentIndex = 0;
        LanguageManager();
    }

    private void Update()
    {
        if (!beingHandled && ContinueButton.activeSelf && (Input.GetKeyDown("return") || Input.GetKeyDown("enter")))
        {
            Debug.Log("Pressionou enter");
            DisplayNextSentence();
        }
    }

    private void LanguageManager()
    {
        if (PlayerPrefs.GetInt("Idioma") == 0)
            arrayText = portugues;
        else if (PlayerPrefs.GetInt("Idioma") == 1)
            arrayText = kaingang;
        else if (PlayerPrefs.GetInt("Idioma") == 2)
            arrayText = english;
    }

    public void DisplayNextSentence()
    {
        indexText.text = (currentIndex + 1).ToString() + "/" + arrayText.Length;
        if (currentIndex == arrayText.Length-1) // Caso essa seja a ultima mensagem a ser exibida
        {
            playButton.SetActive(true);
            ContinueButton.SetActive(false);
        }
        if (currentIndex > 0) // Caso essa nao seja a primeira mensagem
            backButton.SetActive(true);
        StopAllCoroutines(); // Para de exibir todas as mensagens

        StartCoroutine(TypeSentence(arrayText[currentIndex])); // Comeca a exibir a mensagem atual letra por letra

        foreach (GameObject image in imagesToShow) // Certifica-se de desativar todas as imagens anteriores
            image.SetActive(false);
        imagesToShow[currentIndex].SetActive(true); // Ativa a imagem atual a ser exibida no tutorial

        currentIndex++; // Incrementa o numero da mensagem atual
    }

    public void DisplayPreviousSentence()
    {
        currentIndex -= 2; // Decrementa o index para a mensagem anterior
        indexText.text = (currentIndex+1).ToString() + "/" + arrayText.Length;
        if (currentIndex > 0) // Caso essa nao seja a primeira mensagem, ativa o botao de voltar
            backButton.SetActive(true);
        else if (currentIndex == 0)
            backButton.SetActive(false);

        if (playButton.activeSelf) // Caso o botao de play estava ativado, desativa-o
        {
            playButton.SetActive(false);
            ContinueButton.SetActive(true);
        }

        StopAllCoroutines(); // Para de exibir todas as mensagens

        foreach (GameObject image in imagesToShow) // Certifica-se de desativar todas as imagens anteriores
            image.SetActive(false);
        imagesToShow[currentIndex].SetActive(true); // Ativa a imagem atual a ser exibida no tutorial

        StartCoroutine(TypeSentence(arrayText[currentIndex])); // Comeca a exibir a mensagem atual letra por letra

        currentIndex++; // Incrementa o numero da mensagem atual

    }

    IEnumerator TypeSentence(string sentence) // Exibe a mensagem letra por letra
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f); // Intervalo de dempo entre cada letra
        }
    }

    public void ResetSentences()
    {
        currentIndex = 0;
        indexText.text = (currentIndex + 1).ToString() + "/" + arrayText.Length;
    }

}
