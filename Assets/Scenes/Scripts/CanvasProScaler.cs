using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasProScaler : MonoBehaviour
{
    private CanvasScaler scaler;
    private float aspect = 0;

    void Awake()
    {
        scaler = this.GetComponent<CanvasScaler>();
        SetScale();
    }

    private void Update()
    {
        if(Application.isEditor) { SetScale(); }
    }

    private void SetScale()
    {
        aspect = Camera.main.aspect;
        scaler.matchWidthOrHeight = aspect >= 1.5f && aspect < 1.7f ? 0 : 1;
    }

}
