using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private Transform dialogueGroup;

    private DialogueHolder dialogueHolder = null;
    private GameObject interactionBtn;

    private void Start()
    {
        dialogueGroup = transform.Find("Dialogue Group").transform;
        interactionBtn = GameObject.Find("/Canvas/Interaction HUD Area/Talk Btn").gameObject;
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
        if(dialogueGroup.childCount == 0) { return; }
        foreach(Transform dialogue in dialogueGroup)
        {
            TaskVerifier dialogue_verifier = dialogue.GetComponent<TaskVerifier>();
            if(dialogue_verifier.dialogueEnabled && dialogue_verifier.Verify()) // dialogo ativo e com os requisitos cumpridos
            { 
                dialogueHolder = dialogue.GetComponent<DialogueHolder>(); 
                return; 
            }
        }
        dialogueHolder = null; // nenhum dialogo neste npc
    }
}
