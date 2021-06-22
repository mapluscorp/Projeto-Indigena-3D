using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
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
