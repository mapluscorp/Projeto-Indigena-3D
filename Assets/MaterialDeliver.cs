using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDeliver : MonoBehaviour
{
    public Material[] materials;
    private int index = 0;
    void Start()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Renderer>().material = materials[index]; index++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
