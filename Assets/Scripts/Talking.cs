using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talking : MonoBehaviour
{
    private Animator anim;

    private float minValue = 2;
    private float maxValue = 10;

    public float rand;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        StartCoroutine(Randomizer());
    }

    IEnumerator Randomizer()
    {
        rand = Random.Range(0.0f, 1.0f);
        anim.SetFloat("Talking", rand);
        yield return new WaitForSeconds(Random.Range(minValue, maxValue));
        StartCoroutine(Randomizer());
    }
}
