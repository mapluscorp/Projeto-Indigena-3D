using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSlot : MonoBehaviour // esse script eh a barrinha da task na UI, computa o progresso
{
    [HideInInspector]
    public TaskMaker whoMadeTheTask;

    public Image icon; // icone da task
    public Text descriptionText; // descricao da task
    public int target; // valor objetivo

    public int current = 0; // valor atual
    private RectTransform progressBar; // a barra de progresso na UI
    private int progressBarMaxSize; // tamanho maximo da barra em pixels

    public bool isCompleted { get; set; }

    void Start()
    {
        progressBar = this.transform.Find("Visual/Progress Bar").GetComponent<RectTransform>();
        progressBarMaxSize = (int)progressBar.sizeDelta.x;
        RefreshProgressBar();
    }

    public void IncrementProgress(int value) // chamado para incrementar um valor x no progresso
    {
        int oldCurrent = current;
        current += value;
        if(current >= target) { CompleteTask(); }
        StartCoroutine(ProgressAnimation(oldCurrent, current));
    }

    private void CompleteTask() // chamado quando atinge o objetivo da task
    {
        current = target; // impede de ter um progresso maior que o target
        isCompleted = true; // marca essa task como completada
        whoMadeTheTask.taskCompleted = true; // avisa o script que criou a task, que ela esta agora completa
        foreach(TaskMaker maker in whoMadeTheTask.transform)
        {
            if(maker.taskCompleted == false) { return; } // caso uma das tasks requisitas ainda nao tenha sido cumprida
        }
        whoMadeTheTask.gameObject.SetActive(false); // desativa este dialogo pois ja completou todas as tasks
    }

    public void RefreshProgressBar() // atualiza o tamanho da barra
    {
        progressBar.sizeDelta = new Vector3((current * progressBarMaxSize) / target, progressBar.sizeDelta.y);
    }

    IEnumerator ProgressAnimation(float progress_current, int target_current)
    {
        yield return new WaitForSeconds(0.5f); // espera meio segundo antes de iniciar, animacao do icone indo ate a task
        while (progress_current < target_current)
        {
            progress_current += 0.025f;
            progressBar.sizeDelta = new Vector3((progress_current * progressBarMaxSize) / target, progressBar.sizeDelta.y);
            yield return new WaitForSeconds(0.025f);
        }
        RefreshProgressBar();
    }

    public void DestroyTask()
    {
        Destroy(this.gameObject);
    }
}
