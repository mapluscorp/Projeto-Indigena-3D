using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskMaker : MonoBehaviour // esse script gera uma task, o metodo GenerateTask deve ser chamado por outro script
{
    public GameObject taskPrefab;
    public string taskName;
    public Sprite icon;
    public string description;
    public int target;
    private Transform parent;
    public bool taskCompleted = false;

    private bool HasCreated { get; set; }

    private void Start()
    {
        parent = GameObject.Find("/Canvas/Task System/Task Group").transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            GenerateTask();
        }
    }

    public void GenerateTask()
    {
        if(HasCreated) { return; } // para nao criar essa task de novo
        HasCreated = true;
        GameObject task = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity); // instancia ela na UI
        task.transform.parent = parent; // poe a task no lugar correto na hierarquia
        task.transform.localScale = Vector3.one; // ajusta a escala
        TaskSlot taskScript = task.GetComponent<TaskSlot>(); // referencia do script da task criada
        taskScript.icon.sprite = icon; // atualiza o icone dela
        taskScript.descriptionText.text = description; // atualiza o texto dela
        taskScript.target = target; // atualiza o valor objetivo da task
        taskScript.GetComponent<Identifier>().name = taskName; // atualiza o nome da task
        taskScript.whoMadeTheTask = this.GetComponent<TaskMaker>(); // marca quem criou a task
    }
}
