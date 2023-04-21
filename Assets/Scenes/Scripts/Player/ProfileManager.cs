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

    private int profileLoadIndex = -1;
    public static ProfileManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        this.LoadProfiles();
    }

    private void LoadProfiles()
    {
        if (PlayerPrefs.HasKey("profiles"))
        {
            string profilesData = PlayerPrefs.GetString("profiles", "[]");
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

    public void CreatePlayer(string playerTypeString)
    {
        string playerName = profileNameInputField.text;
        profileNameInputField.text = "";
        PlayerType playerType = (PlayerType)Enum.Parse(typeof(PlayerType), playerTypeString);
        if (this.CreateProfile(playerName, playerType))
        {
            this.LoadProfile(this.profiles.Count - 1);
            MainMenuScript.Instance.GoToMainMenu();
        }
    }

    public void LoadOrCreate(string playerName, PlayerType playerType)
    {
        if (this.profiles.Count > 0)
        {
            this.LoadProfile(0);
        }
        else if (this.CreateProfile(playerName, playerType))
        {
            this.LoadProfile(0);
        }

    }
    public void LoadProfile(int index)
    {
        this.profileLoadIndex = index;
    }

    public Profile GetActiveProfile()
    {
        if (profileLoadIndex < 0)
            return null;
        return this.profiles[profileLoadIndex];
    }

    public void SetLevelCurrentProfile(Level level, Vector3 position)
    {
        this.profiles[profileLoadIndex].level = level;
        this.profiles[profileLoadIndex].position = position;
        this.SaveProfiles();
    }

    public void DeleteCurrentProfile()
    {
        this.profiles.RemoveAt(profileLoadIndex);
        this.SaveProfiles();
        MainMenuScript.Instance.RefreshSlots();
    }
}

