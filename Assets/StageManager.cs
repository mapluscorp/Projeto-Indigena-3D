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
            ExecuteStage(stage0);
            return;
        }

        InitState();
    }

    private void InitState()
    {
        switch (PlayerPrefs.GetString("Stage"))
        {
            case "Begin":
                ExecuteStage(stage0);
                break;
            case "Nightshade":
                ExecuteStage(stage1);
                break;
            case "Pari":
                ExecuteStage(stage2);
                break;
            case "Corn":
                ExecuteStage(stage3);
                break;
            case "School":
                ExecuteStage(stage4);
                break;
            case "LateSchool":
                ExecuteStage(stage5);
                break;
            case "Forest":
                ExecuteStage(stage6);
                break;
            case "Fishing":
                ExecuteStage(stage7);
                break;
            default:
                ExecuteStage(stage0);
                break;
        }
    }

    private void ExecuteStage(GameObject[] stage)
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
    
    public void SetAndExecuteStage(string name)
    {
        PlayerPrefs.SetString("Stage", name);
        InitState();
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
