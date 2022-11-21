using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour // script responsavel por exibir o dialogo na tela
{
    public List<string> sentences;
    public Image characterSprite;
    public Text characterName;
    public GameObject talkIcon;

    public Text dialogueText;
    public GameObject ContinueButton;
    public GameObject backButton;

    public Button talkBtn;

    private short currentIndex;
    private Animator anim;
    private GameObject display;
    private AudioSource source;

    [HideInInspector]
    public TaskMaker[] taskMaker;
    [HideInInspector]
    public List<TaskSlot> completedTasks;

    public PlayerManager playerManager;

    [HideInInspector]
    public UnityEvent onEndDialogue;

    void Start()
    {
        currentIndex = 0;
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        anim = this.transform.Find("Display/Dialogue Display").GetComponent<Animator>();
        display = this.transform.Find("Display").gameObject;
        source = this.GetComponent<AudioSource>();
    }

    public void Begin() // chamado pelo botao de iniciar dialogo na UI
    {
        Animator playerAnim = playerManager.GetComponentInChildren<Animator>();
        playerAnim.SetFloat("PlayerSpeed", 0);
        playerAnim.SetTrigger("Idle");
        playerManager.CanInteract = false;
        anim.SetTrigger("Open");
        talkBtn.gameObject.SetActive(false);
        Continue();
        foreach(TaskSlot task in completedTasks) // manda as tasks ja completadas embora
        { 
            task.GetComponent<Animator>().SetTrigger("Complete");
        }
        completedTasks.Clear();
        talkIcon.SetActive(false); // desativa o icone de dialogo ao lado do NPC
    }

    public void Continue() // chamado cada vez que o player clica no balao de dialogo
    {
        if (currentIndex < sentences.Count) { DisplayNextSentence(); }
        else { StartCoroutine(Close()); }
    }

    IEnumerator Close() // chamado ao fim de todas as falas
    {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        playerManager.CanInteract = true;
        ResetSentences();
        display.SetActive(false);
        if(taskMaker != null) 
        { 
            foreach(TaskMaker task in taskMaker)
                task.GenerateTask(); 
        } // gera a task na UI
        taskMaker = null;
        onEndDialogue.Invoke();
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
            if(dialogueText.text.Length % 2 == 0) { source.Play(); }
            yield return new WaitForSeconds(0.03f); // Intervalo de dempo entre cada letra
        }
    }

    public void ResetSentences()
    {
        currentIndex = 0;
    }

}
