using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleTween : MonoBehaviour
{
    public bool scaleIn;
    public bool scaleOut;
    public float transitionSpeed = 0.3f;

    public UnityEvent beforeClose;
    public UnityEvent beforeOpen;

    public UnityEvent onEnable;
    public UnityEvent onClose;

    private void OnEnable()
    {
        beforeOpen.Invoke();
        if (scaleIn)
        {
            this.gameObject.transform.localScale = Vector3.zero;
            LeanTween.scale(this.gameObject, Vector3.one, transitionSpeed).setOnComplete(OnFinishOpening);
        } else
        {
            onEnable.Invoke();
        }
    }

    public void OnClose()
    {
        beforeClose.Invoke();
        if (scaleOut)
        {
            LeanTween.scale(this.gameObject, Vector3.zero, transitionSpeed).setOnComplete(OnFinishClosing);
        } else
        {
            OnFinishClosing();
        }
    }

    private void OnFinishOpening()
    {
        onEnable.Invoke();
    }

    private void OnFinishClosing()
    {
        onClose.Invoke();
    }
}
