using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskMaker : MonoBehaviour
{
    public GameObject taskPrefab;
    public string taskName;
    public Sprite icon;
    public string description;
    public int target;
    private Transform parent;
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
        if(HasCreated) { return; }
        HasCreated = true;
        GameObject task = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity);
        task.transform.parent = parent;
        task.transform.localScale = Vector3.one;
        TaskSlot taskScript = task.GetComponent<TaskSlot>();
        taskScript.icon.sprite = icon;
        taskScript.descriptionText.text = description;
        taskScript.target = target;
        taskScript.GetComponent<Identifier>().name = taskName;
    }
}
