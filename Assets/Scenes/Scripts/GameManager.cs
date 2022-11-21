using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] enableAtStart;
    public GameObject[] disableAtStart;
    public Text resolutionText;

    private void Awake()
    {
        //int width = (int) (Screen.width * 0.7f);
        //int height = (int) (Screen.height * 0.7f);
        Screen.SetResolution(1280, 720, true);
        //resolutionText.text = width.ToString() + "x" + height.ToString();
        PlayerPrefs.SetString("Home", "Game");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
        
        if(Input.GetKeyDown(KeyCode.Keypad4))
            Screen.SetResolution(1280, 720, true);
        if(Input.GetKeyDown(KeyCode.Keypad5))
            Screen.SetResolution(1920, 1080, true);
    }
}
