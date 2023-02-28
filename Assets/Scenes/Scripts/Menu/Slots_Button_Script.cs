using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Profiles;

public class Slots_Button_Script : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    public MainMenuScript mainMenuScript;
    public int N_Button;
    private Button pb;
    public Sprite[] OffSprite; // 0 = normal | 1 = over | 2 = clicked
    public Sprite[] KameSprite; // 0 = normal | 1 = over | 2 = clicked
    public Sprite[] KairuSprite; // 0 = normal | 1 = over | 2 = clicked
    public Text myName; // nome do jogador deste slot

    void Start()
    {
        pb = GetComponent<Button>();
        Refresh();
    }

    public void Refresh()
    {
        switch (PlayerPrefs.GetString("Type" + N_Button.ToString()))
        {
            case "Kame":
                pb.image.sprite = KameSprite[0];
                break;
            case "Kairu":
                pb.image.sprite = KairuSprite[0];
                break;
            default:
                pb.image.sprite = OffSprite[0];
                break;
        }
        string tmp_name = PlayerPrefs.GetString("Name" + N_Button.ToString());
        if (PlayerPrefs.GetInt("Idioma") == 0) // portugues
            myName.text = tmp_name.Length > 0 ? tmp_name : "Vazio"; // poe o nome do jogador no slot
        if (PlayerPrefs.GetInt("Idioma") == 1) // kaingang
            myName.text = tmp_name.Length > 0 ? tmp_name : "Kupré"; // poe o nome do jogador no slot
    }

    public void OnSelect() // metodo chamado ao clicar no slot
    {
        MainMenuScript.selectedSlot = N_Button; // diz para o script do menu qual foi o slot selecionado
        if (!PlayerPrefs.HasKey("Type" + N_Button.ToString()) || PlayerPrefs.GetString("Type" + N_Button.ToString()) == "") // perfil vazio
        {
            mainMenuScript.OpenNameInsertScreen();
        }
        else if (!PlayerPrefs.HasKey("Sex" + N_Button.ToString()) || PlayerPrefs.GetString("Sex" + N_Button.ToString()) == "") // ainda nao escolheu menina ou menino
        {
            mainMenuScript.GoToSexSelection();
        }
        else // perfil completo
        {
            mainMenuScript.GoToMainMenu();
            bool isKame = PlayerPrefs.GetString("Type" + N_Button.ToString()) == "Kame";
            bool isMale = PlayerPrefs.GetString("Sex" + N_Button.ToString()) == "Menino";
            Debug.Log("SAVE? " + ((int)(isKame ? (isMale ? PROFILE_TYPES.KAME_MALE : PROFILE_TYPES.KAME_FEMALE) : (isMale ? PROFILE_TYPES.KAMHRU_MALE : PROFILE_TYPES.KANHRU_FEMALE))).ToString());
            PlayerPrefs.SetInt("profile_active", (int)(isKame ? (isMale ? PROFILE_TYPES.KAME_MALE : PROFILE_TYPES.KAME_FEMALE) : (isMale ? PROFILE_TYPES.KAMHRU_MALE : PROFILE_TYPES.KANHRU_FEMALE)));
        }
    }

    public void OnPointerEnter(PointerEventData eventData) // Mouse sobre o botao. Sprite over
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            switch (PlayerPrefs.GetString("Type" + N_Button.ToString()))
            {
                case "Kame":
                    pb.image.sprite = KameSprite[1];
                    break;
                case "Kairu":
                    pb.image.sprite = KairuSprite[1];
                    break;
                default:
                    pb.image.sprite = OffSprite[1];
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) // Mouse saiu do botao. Sprite volta ao normal
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            switch (PlayerPrefs.GetString("Type" + N_Button.ToString()))
            {
                case "Kame":
                    pb.image.sprite = KameSprite[0];
                    break;
                case "Kairu":
                    pb.image.sprite = KairuSprite[0];
                    break;
                default:
                    pb.image.sprite = OffSprite[0];
                    break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData) // Mouse esta sendo pressionado. Sprite de clicado
    {
        switch (PlayerPrefs.GetString("Type" + N_Button.ToString()))
        {
            case "Kame":
                pb.image.sprite = KameSprite[2];
                break;
            case "Kairu":
                pb.image.sprite = KairuSprite[2];
                break;
            default:
                pb.image.sprite = OffSprite[2];
                break;
        }
        pb.transform.localScale = new Vector2(0.97f, 0.97f);
    }
    public void OnPointerUp(PointerEventData eventData) // Soltou o mouse. Sprite volta ao normal
    {
        int v;
        if (Application.platform != RuntimePlatform.Android) v = 1;
        else v = 0;

        switch (PlayerPrefs.GetString("Type" + N_Button.ToString()))
        {
            case "Kame":
                pb.image.sprite = KameSprite[v];
                break;
            case "Kairu":
                pb.image.sprite = KairuSprite[v];
                break;
            default:
                pb.image.sprite = OffSprite[v];
                break;
        }
        pb.transform.localScale = new Vector2(1f, 1f);
    }

}
