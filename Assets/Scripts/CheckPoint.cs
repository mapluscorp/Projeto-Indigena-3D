using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider[] triggers;
    private Transform spawnPosition;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        triggers = this.transform.GetComponentsInChildren<BoxCollider>();
        spawnPosition = this.transform.Find("Spawn Position").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            if(other.GetComponentInParent<PlayerManager>().IsAlive)
                CheckPointSystem.CurrentSpawnPosition = spawnPosition;
    }
}
