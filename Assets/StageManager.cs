using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stage0;
    public GameObject[] stage1;
    public GameObject[] stage2;
    public GameObject[] stage3;
    public GameObject[] stage4;
    public GameObject[] stage5;
    public GameObject[] stage6;
    public GameObject[] stage7;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SpawnPosition") || !PlayerPrefs.HasKey("Stage"))
        {
            ExecutaStage(stage0);
            return;
        }
        switch (PlayerPrefs.GetString("Stage"))
        {
            case "Begin":
                ExecutaStage(stage0);
                break;
            case "Nightshade":
                ExecutaStage(stage1);
                break;
            case "Pari":
                ExecutaStage(stage2);
                break;
            case "Corn":
                ExecutaStage(stage3);
                break;
            case "School":
                ExecutaStage(stage4);
                break;
            case "LateSchool":
                ExecutaStage(stage5);
                break;
            case "Forest":
                ExecutaStage(stage6);
                break;
            case "Fishing":
                ExecutaStage(stage7);
                break;
            default:
                ExecutaStage(stage0);
                break;
        }

    }

    private void ExecutaStage(GameObject[] stage)
    {
        foreach(GameObject s in stage)
        {
            s.SetActive(true);
        }
    }

    public void SetStage(string name)
    {
        PlayerPrefs.SetString("Stage", name);
    }

    public static void SetStageStatic(string name)
    {
        PlayerPrefs.SetString("Stage", name);
    }

    public string GetStage()
    {
        return PlayerPrefs.GetString("Stage");
    }

    public static string GetStageStatic()
    {
        return PlayerPrefs.GetString("Stage");
    }
}
