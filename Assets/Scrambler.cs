using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scrambler : MonoBehaviour
{
    public Transform[] children;
    void Start()
    {
        GetChildren();
        Scramble();
    }

    private void Scramble()
    {
        int tam = children.Length;
        for (int i = 0; i < tam; i++)
        {
            int rand = Random.Range(0, tam);
            Vector3 pos = children[i].position;
            children[i].position = children[rand].position;
            children[rand].position = pos;
        }
    }

    private void GetChildren()
    {
        children = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            children[i] = child; i++;
        }
    }
}
