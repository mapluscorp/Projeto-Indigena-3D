using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour // esse script guarda as falas e envia para o DialogueManager quando o player estiver proximo
{
    public string characterName;
    public Sprite characterSprite;
    public string[] portugues;
    public string[] kaingang;
    public float triggerDistance = 1;

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = GameObject.Find("/Canvas/Dialogue System").GetComponent<DialogueManager>();
    }

    public void SendSentences()
    {
        dialogueManager.sentences.Clear(); // limpa a lista
        dialogueManager.characterSprite.sprite = characterSprite;
        dialogueManager.characterName.text = characterName;

        //TaskMaker taskMaker = this.GetComponent<TaskMaker>(); // confere se ha um task generator neste NPC
        //if (taskMaker != null) { dialogueManager.taskMaker = taskMaker; } // Envia o script contendo as informacoes da task

        dialogueManager.taskMaker = this.GetComponents<TaskMaker>(); // Envia o script contendo as informacoes da task

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
