using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static Transform CurrentSpawnPosition { get; set; }

    public CheckPoint[] checkPoints;

    public bool meshVisible;

    private void Start()
    {
        checkPoints = this.transform.GetComponentsInChildren<CheckPoint>();

        foreach(CheckPoint cp in checkPoints)
        {
            cp.SetMeshVisibility(meshVisible);
        }
    }

}
