using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooktAtTarget : MonoBehaviour
{
    public Transform target;

    private void OnBecameVisible() // comeca a conferir a distancia para o player
    {
        InvokeRepeating("Look", 0.1f, 0.1f);
    }

    private void OnBecameInvisible() // para de conferir a distancia
    {
        CancelInvoke("Look");
    }

    private void Look()
    {
        this.transform.LookAt(target.position);
    }
}
