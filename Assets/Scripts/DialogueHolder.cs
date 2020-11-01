using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    public string characterName;
    public Sprite characterSprite;
    public string[] portugues;
    public string[] kaingang;
    public float triggerDistance = 1;

    private Transform player;
    private DialogueManager dialogueManager;
    private GameObject interactionBtn;

    private bool triggered = false; // controla se o dialogo ja foi acionado

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueManager = GameObject.Find("/Canvas/Dialogue System").GetComponent<DialogueManager>();
        interactionBtn = GameObject.Find("/Canvas/Dialogue System/Interaction Btn").gameObject;
    }

    private void OnBecameVisible() // comeca a conferir a distancia para o player
    {
        InvokeRepeating("CheckDistance", 0.1f, 0.1f);
    }

    private void OnBecameInvisible() // para de conferir a distancia
    {
        CancelInvoke("CheckDistance");
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        if (!triggered && distance < triggerDistance) // proximo do personagem e ainda nao enviou as sentencas
        {
            SendSentences();
            interactionBtn.SetActive(true);
            triggered = true;
        } 
        else if (triggered && distance > triggerDistance) // distante do personagem
        {
            triggered = false;
            interactionBtn.SetActive(false);
        }
    }

    private void SendSentences()
    {
        dialogueManager.sentences.Clear(); // limpa a lista
        dialogueManager.characterSprite.sprite = characterSprite;
        dialogueManager.characterName.text = characterName;
        interactionBtn.SetActive(false); // esconde o botao de iniciar dialogo

        if (PlayerPrefs.GetInt("Idioma") == 0) // portugues
        {
            for (int i = 0; i < portugues.Length; i++)
            {
                dialogueManager.sentences.Add(portugues[i]);
            }
        }
        else if(PlayerPrefs.GetInt("Idioma") == 1) // kaingang
        {
            for (int i = 0; i < kaingang.Length; i++)
            {
                dialogueManager.sentences.Add(kaingang[i]);
            }
        }
    }
}
