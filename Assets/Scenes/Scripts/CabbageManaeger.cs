using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageManaeger : MonoBehaviour
{
    public GameObject[] variations;

    void Start()
    {
        foreach (GameObject var in variations)
        {
            var.SetActive(false);
        }
        variations[Random.Range(0, variations.Length)].SetActive(true);
    }

}
