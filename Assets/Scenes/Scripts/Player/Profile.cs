using System;

[Serializable]
public class Profile
{
    public string name;
    public PlayerType playerType;

    public Profile(string name, PlayerType playerType)
    {
        this.name = name;
        this.playerType = playerType;
    }
}