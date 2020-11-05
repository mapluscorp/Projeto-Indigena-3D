using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour // esse script detecta itens no cenario e ativa o botao de interacao
{
    [Header("References")]
    public Button plantInteractionBtn;
    public Image collectableItemAnimation;

    private PlayerManager playerManager;
    private Animator anim;
    private Transform taskGroup;

    private Identifier identifier; // identifier do objeto que esta em trigger no momento

    private void Start()
    {
        playerManager = this.GetComponentInParent<PlayerManager>();
        anim = this.GetComponentInChildren<Animator>();
        taskGroup = GameObject.Find("/Canvas/Task System/Task Group").transform;
    }

    public void PullPlant() // chamado pelo botao de interacao na UI
    {
        anim.SetTrigger("PullPlant");
        StartCoroutine(HoldMovement(4.5f)); // impede o movimento do player enquanto ele a animacao de coletar esta em execucao
    }

    private void OnTriggerEnter(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>(); // identificador do objeto
        if(identifier == null) { return; }
        if (identifier.type == "Plant") // confere se eh uma planta
        {
            plantInteractionBtn.gameObject.SetActive(true); // exibe o botao de interagir com plantas
        }
    }

    private void OnTriggerStay(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>();
        if (identifier == null) { return; }
        if (identifier.type == "Plant")
        {
            plantInteractionBtn.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>();
        if (identifier == null) { return; }
        if (identifier.type == "Plant")
        {
            plantInteractionBtn.gameObject.SetActive(false);
        }
        identifier = null;
    }

    IEnumerator HoldMovement(float time) // impede o movimento por um determinado periodo de tempo
    {
        playerManager.CanMove = false;
        yield return new WaitForSeconds(time);
        playerManager.CanMove = true;
    }

    public void CollectPlant() // chamado pela animacao de coletar planta
    {
        foreach(Transform task in taskGroup) // confere as tasks existentes
        {
            Identifier task_ID = task.GetComponent<Identifier>();
            if (task_ID.name.Contains(identifier.name))
            {
                TaskSlot taskSlot = task.GetComponent<TaskSlot>();
                if(taskSlot.isCompleted) { return; } // ja coletou tudo que precisava
                taskSlot.IncrementProgress(1); // incrementa o progresso da task
                collectableItemAnimation.transform.position = new Vector2(Screen.width / 2, Screen.height / 2); // posicao do jogador
                collectableItemAnimation.sprite = taskSlot.icon.sprite; // icone da planta sendo coletada
                collectableItemAnimation.gameObject.SetActive(true); // exibe
                StartCoroutine(CollectableAnimator(taskSlot.transform)); // percorre o trajeto ate a janelinha de tasks
            }
            Destroy(identifier.gameObject); // some com a planta que foi coletada
            plantInteractionBtn.gameObject.SetActive(false);
        }
    }

    IEnumerator CollectableAnimator(Transform targetPos) // animacao do item sendo coletado
    {
        float timeElapsed = 0;
        float lerpDuration = 0.5f;

        Vector2 startPos = collectableItemAnimation.transform.position;
        Vector2 endPos = targetPos.position;
        Vector2 valueToLerp;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Vector2.Lerp(startPos, endPos, timeElapsed / lerpDuration);
            collectableItemAnimation.transform.position = valueToLerp;
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        collectableItemAnimation.gameObject.SetActive(false);
    }

}
