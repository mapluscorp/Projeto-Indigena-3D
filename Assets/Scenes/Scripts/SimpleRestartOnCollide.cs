using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleRestartOnCollide : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && (other.gameObject.transform.parent == null || other.gameObject.transform.parent.name != "Boat"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}