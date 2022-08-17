using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TaskMaker taskverify;
    private bool iscomplete;
    // Update is called once per frame
    void Update()
    {
        taskverify = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<TaskMaker>();
        iscomplete = taskverify.taskCompleted;
        if (iscomplete)
        {
            Destroy(this.gameObject);
        }

    }
}
