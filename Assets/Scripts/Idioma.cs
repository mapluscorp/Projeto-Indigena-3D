using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Idioma : MonoBehaviour
{
    public Text[] textos;
    public string[] portugues;
    public string[] kaingang;

    void Start()
    {
        if(GameObject.Find("IdiomaSlider") != null)
        {
            GameObject.Find("IdiomaSlider").GetComponent<Slider>().value = PlayerPrefs.GetInt("Idioma");
        }

        Refresh();
    }

    public void Refresh() // atualiza os textos para o idioma escolhido
    {
        if(PlayerPrefs.GetInt("Idioma") == 0) // 0 == portugues
        {
            for (int i = 0; i < textos.Length; i++)
            {
                textos[i].text = portugues[i];
            }
        } else if (PlayerPrefs.GetInt("Idioma") == 1) // 1 == kaingang
        {
            for (int i = 0; i < textos.Length; i++)
            {
                textos[i].text = kaingang[i];
            }
        }
    }

    public void OnSliderChange(float value) // metodo para por dinamicamente no slider de idioma
    {
        PlayerPrefs.SetInt("Idioma", (int)value);
        Refresh();
    }
}
