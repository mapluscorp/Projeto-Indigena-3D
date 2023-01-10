using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{

    [SerializeField]
    public List<Profile> profiles;
    public InputField profileNameInputField;

    public Profile selectedProfile;
    public static ProfileManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        this.LoadProfiles();
        selectedProfile = this.profiles[0];
    }

    private void LoadProfiles()
    {
        if (PlayerPrefs.HasKey("profiles"))
        {
            string profilesData = PlayerPrefs.GetString("profiles", "[]");
            Debug.Log(profilesData);
            this.profiles = JsonHelper.FromJson<Profile>(profilesData);
        }
    }

    private void SaveProfiles()
    {
        string profilesData = JsonHelper.ToJson(this.profiles);
        PlayerPrefs.SetString("profiles", profilesData);
    }

    public List<Profile> GetProfiles()
    {
        return this.profiles;
    }

    public bool CreateProfile(string name, PlayerType playerType)
    {
        Profile profile = new Profile(name, playerType);
        this.profiles.Add(profile);
        this.SaveProfiles();
        return true;
    }

    public void SelectPlayer(string playerTypeString)
    {
        string playerName = profileNameInputField.text;
        profileNameInputField.text = "";
        PlayerType playerType = (PlayerType)Enum.Parse(typeof(PlayerType), playerTypeString);
        this.CreateProfile(playerName, playerType);
    }
}

