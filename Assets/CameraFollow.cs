using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {

    }

    public void UpdateOffset(float value)
    {
        offset = new Vector3(0, value, value);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.position + offset;
        this.transform.LookAt(target.position);
    }
}
