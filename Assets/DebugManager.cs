using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    [Header("Texts")]
    public Text stageText;
    public Text spawnText;
    void Update()
    {
        stageText.text = "Stage: " + PlayerPrefs.GetString("Stage");
        spawnText.text = CheckPointSystem.CurrentSpawnPosition.ToString();
    }

    public void OnResetBtn()
    {
        PlayerPrefs.DeleteKey("Stage");
    }
}
