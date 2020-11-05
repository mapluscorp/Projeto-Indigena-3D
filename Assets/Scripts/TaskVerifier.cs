using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskVerifier : MonoBehaviour
{
    public string[] tasks;

    private Transform tasksParent;
    private DialogueManager dialogueManager;

    public bool dialogueEnabled { get; set; }

    void Start()
    {
        tasksParent = GameObject.Find("/Canvas/Task System/Task Group").transform;
        dialogueManager = GameObject.Find("/Canvas/Dialogue System").GetComponent<DialogueManager>();
        dialogueEnabled = true;
    }

    public bool Verify()
    {
        int current = 0;

        if(tasks.Length == 0) { return true; } // nao requisita nenhuma task

        foreach (Transform task in tasksParent) // confere cada task atual
        {
            Identifier task_ID = task.GetComponent<Identifier>(); // identificacao da task
            TaskSlot taskSlot = task.GetComponent<TaskSlot>(); // progresso da task

            if (CheckName(task_ID.name)) // confere se essa task eh requisitada por esse NPC
            {
                if (!taskSlot.isCompleted) { return false; }
                else
                {
                    current++; // task completa
                    dialogueManager.completedTasks.Add(taskSlot); // adiciona essa task verificada na lista das completadas, so sera morta ao iniciar dialogo
                }
            }

            if(current == tasks.Length) { return true; }
        }

        return false; // nao eh suposto chegar aqui
    }

    private bool CheckName(string taskName) // confere se o nome da task eh um dos nomes requisitados
    {
        for(int i = 0; i < tasks.Length; i++)
        {
            if(tasks[i] == taskName) { return true; } // encontrou o nome
        }
        return false; // nao tem esse nome
    }
}
