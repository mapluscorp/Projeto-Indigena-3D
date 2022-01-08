using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour, IInteractable
{
    private Transform dialogueGroup;

    private DialogueHolder dialogueHolder = null;
    private GameObject interactionBtn;

    private GameObject talkIcon;

    private bool justValidate;

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

    public void JustValidate()
    {
        justValidate = true;
    }

    
    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void TriggerEnter()
    {
        GetDialogue(); // pega o dialogo correto
        if (dialogueHolder == null) { return; } // nao ha dialogos neste npc
        dialogueHolder.SendSentences();
        print("NPC Trigger enter");
        interactionBtn.SetActive(true);
        gameObject.AddComponent<Outline>();
    }

    public void TriggerExit()
    {
        interactionBtn.SetActive(false);
        Outline outline = transform.GetComponent<Outline>();
        if (outline != null) { Destroy(outline); }
    }

    public void SetTalkIconVisibility(bool stat)
    {
        talkIcon.SetActive(stat);
    }

    private void GetDialogue() // pega o dialogo atual
    {
        if (dialogueGroup.childCount == 0) { return; } // nao ha dialogos neste NPC
        foreach (Transform dialogue in dialogueGroup)
        {
            TaskVerifier dialogue_verifier = dialogue.GetComponent<TaskVerifier>();
            if (dialogue.gameObject.activeSelf && (dialogue_verifier.Verify() || justValidate)) // dialogo ativo e com os requisitos cumpridos
            {
                dialogueHolder = dialogue.GetComponent<DialogueHolder>();
                //talkIcon.SetActive(true); // exibe o icone de que ha um dialogo disponivel
                return;
            }
        }
        dialogueHolder = null; // nenhum dialogo neste npc
        if (!justValidate)
        {
            SetTalkIconVisibility(false); // esconde o icone de dialogo
        }
    }

    public bool CheckForDialogueExistence()
    {
        if (dialogueGroup.childCount == 0) { return false; } // nao ha dialogos neste NPC
        foreach (Transform dialogue in dialogueGroup)
        {
            TaskVerifier dialogue_verifier = dialogue.GetComponent<TaskVerifier>();
            if (dialogue.gameObject.activeSelf && (dialogue_verifier.Verify() || justValidate)) // dialogo ativo e com os requisitos cumpridos
            {
                SetTalkIconVisibility(true);
                return true;
            }
        }
        if (!justValidate)
        {
            SetTalkIconVisibility(false); // esconde o icone de dialogo
        }
        return false;
    }
}
