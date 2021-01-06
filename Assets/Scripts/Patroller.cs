using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    public Transform[] waypoints;
    
    private NavMeshAgent agent;
    private Animator anim;

    private int currentWP = 0;

    private bool CanMove { get; set; }

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
        {
            agent.speed = 3.5f;
            agent.SetDestination(GetWayPoint());
            anim.SetFloat("Speed", agent.velocity.magnitude / agent.speed);
        }
        else
        {
            agent.speed = 0;
            anim.SetFloat("Speed", 0);
        }
    }

    private Vector3 GetWayPoint()
    {
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].position) < 1) 
        {
            currentWP++;
            if(currentWP >= waypoints.Length) { currentWP = 0; }
            if(Random.Range(0, 4) == 1) { StartCoroutine(Rest()); }
        }
        return waypoints[currentWP].position;
    }

    IEnumerator Rest()
    {
        CanMove = false;
        yield return new WaitForSeconds(4);
        CanMove = true;
    }
}
