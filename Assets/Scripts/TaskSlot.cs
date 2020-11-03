using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSlot : MonoBehaviour
{
    public Image icon; // icone da task
    public Text descriptionText; // descricao da task
    public int target; // valor objetivo

    public int current = 0; // valor atual
    private RectTransform progressBar; // a barra de progresso na UI
    private int progressBarMaxSize; // tamanho maximo da barra em pixels

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
        if(current > target) { current = target; } // impede de ter um progresso maior que o target
        StartCoroutine(ProgressAnimation(oldCurrent, current));
    }

    public void RefreshProgressBar() // atualiza o tamanho da barra
    {
        progressBar.sizeDelta = new Vector3((current * progressBarMaxSize) / target, progressBar.sizeDelta.y);
    }

    IEnumerator ProgressAnimation(float progress_current, int target_current)
    {
        while(progress_current < target_current)
        {
            progress_current += 0.025f;
            progressBar.sizeDelta = new Vector3((progress_current * progressBarMaxSize) / target, progressBar.sizeDelta.y);
            yield return new WaitForSeconds(0.025f);
        }
        RefreshProgressBar();
    }
}
