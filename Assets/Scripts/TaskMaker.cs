using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TaskMaker : MonoBehaviour // esse script gera uma task, o metodo GenerateTask deve ser chamado por outro script
{
    public bool emptyTask;
    private NPCManager manager;

    public GameObject taskPrefab;
    public string taskName;
    public Sprite icon;
    public string description;
    public int target;
    private Transform parent;
    public bool taskCompleted = false;
    public UnityEvent onCreateTask;
    private Transform NPCGroupParent;

    private bool HasCreated { get; set; }

    private void Start()
    {
        parent = GameObject.Find("/Canvas/Task System/Task Group").transform;
        manager = this.transform.GetComponentInParent<NPCManager>();
        NPCGroupParent = this.transform.root;
    }
    public void GenerateTask()
    {
        if(emptyTask) { onCreateTask.Invoke(); return; } // para apenas invokar e nao gerar task
        if (HasCreated) { return; } // para nao criar essa task de novo
        HasCreated = true;
        GameObject task = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity); // instancia ela na UI
        task.transform.SetParent(parent); // poe a task no lugar correto na hierarquia
        task.transform.localScale = Vector3.one; // ajusta a escala
        TaskSlot taskScript = task.GetComponent<TaskSlot>(); // referencia do script da task criada
        taskScript.icon.sprite = icon; // atualiza o icone dela
        taskScript.descriptionText.text = description; // atualiza o texto dela
        taskScript.target = target; // atualiza o valor objetivo da task
        taskScript.GetComponent<Identifier>().name = taskName; // atualiza o nome da task
        taskScript.whoMadeTheTask = this.GetComponent<TaskMaker>(); // marca quem criou a task
        taskScript.NPCGroupParent = NPCGroupParent;
        onCreateTask.Invoke();
    }

    public void MarkTaskAsCompleted()
    {
        taskCompleted = true;
        if(manager.CheckForDialogueExistence()) // caso haja um proximo dialogo
        {
            manager.SetTalkIconVisibility(true); // ativa o icone de dialogo
        }
    }
}
