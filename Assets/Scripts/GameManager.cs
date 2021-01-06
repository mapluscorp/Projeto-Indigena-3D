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
        int width = (int) (Screen.width * 0.8f);
        int height = (int) (Screen.height * 0.8f);
        Screen.SetResolution(width, height, true);
        resolutionText.text = width.ToString() + "x" + height.ToString();
    }

}
