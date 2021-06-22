using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public GameObject[] transitions;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("SpawnPosition") || !PlayerPrefs.HasKey("Stage")) { print("a"); transitions[0].SetActive(true); return; }
        switch (PlayerPrefs.GetString("Stage"))
        {
            case "Begin":
                transitions[0].SetActive(true);
                break;
            case "Nightshade":
                transitions[1].SetActive(true);
                break;
            default:
                transitions[0].SetActive(true);
                break;
        }

    }
}
