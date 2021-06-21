using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour // script da cesta do mini jogo da cestaria, faz ela girar em torna dela mesma
{
    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }
}
