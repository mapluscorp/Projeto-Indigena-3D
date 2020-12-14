using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private Transform dialogueGroup;

    private DialogueHolder dialogueHolder = null;
    private GameObject interactionBtn;

    [HideInInspector]
    public GameObject talkIcon;

    private void Awake()
    {
        dialogueGroup = transform.Find("Dialogue Group").transform;
        interactionBtn = GameObject.Find("/Canvas/Interaction HUD Area/Talk Btn").gameObject;
        talkIcon = transform.Find("Talk Icon").gameObject;
    }

    private void OnEnable()
    {
        CheckForDialogueExistence();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") { return; } // apenas pode ser triggado pelo jogador
        GetDialogue(); // pega o dialogo correto
        if(dialogueHolder == null) { return; } // nao ha dialogos neste npc
        dialogueHolder.SendSentences();
        interactionBtn.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactionBtn.SetActive(false);
    }

    private void GetDialogue() // pega o dialogo atual
    {
        if(dialogueGroup.childCount == 0) { return; } // nao ha dialogos neste NPC
        foreach(Transform dialogue in dialogueGroup)
        {
            TaskVerifier dialogue_verifier = dialogue.GetComponent<TaskVerifier>();
            if(dialogue.gameObject.activeSelf && dialogue_verifier.Verify()) // dialogo ativo e com os requisitos cumpridos
            { 
                dialogueHolder = dialogue.GetComponent<DialogueHolder>();
                //talkIcon.SetActive(true); // exibe o icone de que ha um dialogo disponivel
                return; 
            }
        }
        dialogueHolder = null; // nenhum dialogo neste npc
        talkIcon.SetActive(false); // esconde o icone de dialogo
    }

    public bool CheckForDialogueExistence()
    {
        if (dialogueGroup.childCount == 0) { return false; } // nao ha dialogos neste NPC
        foreach (Transform dialogue in dialogueGroup)
        {
            TaskVerifier dialogue_verifier = dialogue.GetComponent<TaskVerifier>();
            if (dialogue.gameObject.activeSelf && dialogue_verifier.Verify()) // dialogo ativo e com os requisitos cumpridos
            {
                talkIcon.SetActive(true);
                return true;
            }
        }
        talkIcon.SetActive(false);
        return false;
    }
}
