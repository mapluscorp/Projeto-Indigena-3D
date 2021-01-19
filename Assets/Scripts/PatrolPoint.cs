using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public Transform newtWayPoint;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);
        Gizmos.DrawLine(transform.position, newtWayPoint.position);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
