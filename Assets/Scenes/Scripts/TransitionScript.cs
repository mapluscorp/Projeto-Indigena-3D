using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    public Animator anim;
    public UnityEvent OnTransition;
    public UnityEvent OnTransitionEnd;
    public bool disableAfterEnd;

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    public void WhileTransition()
    {
        OnTransition.Invoke();
    }

    public void OnEndTransition()
    {
        OnTransitionEnd.Invoke();
        if(disableAfterEnd)
            this.gameObject.SetActive(false);
    }

}
