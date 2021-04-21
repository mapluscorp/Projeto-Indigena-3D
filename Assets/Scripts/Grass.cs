using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        int value = Random.Range(1, 4);
        anim.SetInteger("number", value);
    }
}
