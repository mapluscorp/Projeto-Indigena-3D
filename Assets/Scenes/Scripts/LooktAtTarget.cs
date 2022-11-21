using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooktAtTarget : MonoBehaviour
{
    public bool onlyOnVisible;
    public bool x;
    public bool y;
    public bool z;
    public Transform target;

    private void Start()
    {
        if(target == null) { target = Camera.main.transform; }
    }

    private void OnBecameVisible() // comeca a conferir a distancia para o player
    {
        if(onlyOnVisible)
            InvokeRepeating("Look", 0.1f, 0.1f);
    }

    private void OnBecameInvisible() // para de conferir a distancia
    {
        if (onlyOnVisible)
            CancelInvoke("Look");
    }

    private void Update()
    {
        if(!onlyOnVisible)
        {
            Look();
        }
    }

    private void Look()
    {
        if(target == null) { return; }
        Vector3 myRotation = this.transform.eulerAngles;
        this.transform.LookAt(target.position);
        Vector3 newRotation = this.transform.eulerAngles;
        if (!x) { newRotation.x = myRotation.x; }
        if (!y) { newRotation.y = myRotation.y; }
        if (!z) { newRotation.z = myRotation.z; }
        this.transform.eulerAngles = newRotation;
    }
}
