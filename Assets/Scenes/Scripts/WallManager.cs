using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{

    public TaskMaker taskverify;

    private bool iscomplete;

    void Update()
    {
        taskverify = taskverify.GetComponent<TaskMaker>();
        iscomplete = taskverify.taskCompleted; //verifica se a quest foi completa
        if (iscomplete) // se completa, destrói o objeto
        {
            Destroy(this.gameObject);
        }

    }
}
