using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskSlot : MonoBehaviour
{
    public Image icon; // icone da task
    public Text descriptionText; // descricao da task
    public int target; // valor objetivo

    private int current = 0; // valor atual
    private RectTransform progressBar; // a barra de progresso na UI
    private int progressBarMaxSize; // tamanho maximo da barra em pixels

    void Start()
    {
        progressBar = this.transform.Find("Progress Bar").GetComponent<RectTransform>();
        progressBarMaxSize = (int)progressBar.sizeDelta.x;
        RefreshProgressBar();
    }

    public void IncrementProgress(int value) // chamado para incrementar um valor x no progresso
    {
        current += value;
        RefreshProgressBar();
    }

    public void RefreshProgressBar() // atualiza o tamanho da barra
    {
        progressBar.sizeDelta = new Vector3((current * progressBarMaxSize) / target, progressBar.sizeDelta.y);
    }
}
