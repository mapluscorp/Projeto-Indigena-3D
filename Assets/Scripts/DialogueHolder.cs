﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueHolder : MonoBehaviour // esse script guarda as falas e envia para o DialogueManager quando o player estiver proximo
{
    public string characterName;
    public Sprite characterSprite;
    public string[] portugues;
    public string[] kaingang;
    public string[] english;
    public float triggerDistance = 1;

    private DialogueManager dialogueManager;

    public UnityEvent onEndDialogue;

    void Start()
    {
        dialogueManager = GameObject.Find("/Canvas/Dialogue System").GetComponent<DialogueManager>();
    }

    public void SendSentences()
    {
        dialogueManager.sentences.Clear(); // limpa a lista
        dialogueManager.characterSprite.sprite = characterSprite;
        dialogueManager.characterName.text = characterName;
        dialogueManager.talkIcon = transform.parent.parent.Find("Talk Icon").gameObject; // envia referencia do icone de dialogo
        dialogueManager.onEndDialogue = onEndDialogue;
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
        else if(PlayerPrefs.GetInt("Idioma") == 2) // kaingang
        {
            for (int i = 0; i < english.Length; i++)
            {
                dialogueManager.sentences.Add(english[i]);
            }
        }
    }

    public void ExecuteAllTasks()
    {
        TaskMaker[] taskMaker = this.GetComponents<TaskMaker>();
        if (taskMaker != null)
        {
            foreach (TaskMaker task in taskMaker)
                task.GenerateTask();
        }
    }

}
