using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [Header("References")]
    public Button plantInteractionBtn;

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

    public void PullPlant()
    {
        anim.SetTrigger("PullPlant");
        StartCoroutine(HoldMovement(4.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        identifier = other.GetComponentInParent<Identifier>();
        if(identifier == null) { return; }
        if (identifier.type == "Plant")
        {
            plantInteractionBtn.gameObject.SetActive(true);
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

    IEnumerator HoldMovement(float time)
    {
        playerManager.CanMove = false;
        yield return new WaitForSeconds(time);
        playerManager.CanMove = true;
    }

    public void CollectPlant()
    {
        foreach(Transform task in taskGroup)
        {
            Identifier task_ID = task.GetComponent<Identifier>();
            if (task_ID.name.Contains(identifier.name))
            {
                task.GetComponent<TaskSlot>().IncrementProgress(1);
                Destroy(identifier.gameObject); // some com a planta que foi coletada
            }
        }
    }

}
