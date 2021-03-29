using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    public float agentSpeed = 3.5f;
    public int idleProbability = 4;
    public Transform[] waypoints;
    
    private NavMeshAgent agent;
    private Animator anim;

    private int currentWP = 0;

    private bool CanMove { get; set; }

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponentInChildren<Animator>();
        CanMove = true;
        StartCoroutine(Rest());
    }

    // Update is called once per frame
    void Update()
    {
        print("CanMove: " + CanMove);
        if(CanMove)
        {
            agent.speed = agentSpeed;
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
        }
        return waypoints[currentWP].position;
    }

    IEnumerator Rest()
    {
        CanMove = true;
        yield return new WaitForSeconds(Random.Range(6, 15));
        CanMove = false;
        yield return new WaitForSeconds(Random.Range(6, 15));
        print("Restarting Rest");
        StartCoroutine(Rest());
    }
}
