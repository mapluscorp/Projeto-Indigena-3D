using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskMaker : MonoBehaviour
{
    public GameObject taskPrefab;
    public Sprite icon;
    public string description;
    public int target;
    private Transform parent;

    private void Start()
    {
        parent = GameObject.Find("/Canvas/Task System/Background").transform;
        if(parent == null) { Debug.LogError("TaskMaker nao encontrou o parent das tasks, conferir nome"); }
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
        GameObject task = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity);
        task.transform.parent = parent;
        task.transform.localScale = Vector3.one;
        TaskSlot taskScript = task.GetComponent<TaskSlot>();
        taskScript.icon.sprite = icon;
        taskScript.descriptionText.text = description;
        taskScript.target = target;
    }
}
