using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public Transform initialPos;
    public static Vector3 CurrentSpawnPosition { get; set; }

    public CheckPoint[] checkPoints;

    public bool meshVisible;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("SpawnPosition") || !PlayerPrefs.HasKey("Stage"))
        {
            CurrentSpawnPosition = initialPos.position;
        }
        else
        {
            CurrentSpawnPosition = PlayerPrefsX.GetVector3("SpawnPosition");
        }

        checkPoints = this.transform.GetComponentsInChildren<CheckPoint>();

        foreach(CheckPoint cp in checkPoints)
        {
            cp.SetMeshVisibility(meshVisible);
        }
    }

}
