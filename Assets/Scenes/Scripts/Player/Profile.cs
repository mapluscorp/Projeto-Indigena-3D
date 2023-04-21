using System;
using UnityEngine;

[Serializable]
public class Profile
{
    public string name;
    public PlayerType playerType;
    public Level level = Level.LEVEL0;
    public Vector3 position;

    public Profile(string name, PlayerType playerType)
    {
        this.name = name;
        this.playerType = playerType;
    }

}