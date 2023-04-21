using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slots_Button_Script : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler
{
    public MainMenuScript mainMenuScript;
    public int N_Button;
    private Button pb;
    public Sprite[] OffSprite; // 0 = normal | 1 = over | 2 = clicked
    public Sprite[] KameSprite; // 0 = normal | 1 = over | 2 = clicked
    public Sprite[] KairuSprite; // 0 = normal | 1 = over | 2 = clicked
    public Text myName; // nome do jogador deste slot

    private Profile profile;
    void Start()
    {
        pb = GetComponent<Button>();
        Refresh();
    }

    public void Refresh()
    {
        List<Profile> profiles = ProfileManager.Instance.GetProfiles();
        profile = profiles.Count > N_Button ? profiles[N_Button] : null;
        string tmp_name = PlayerPrefs.GetInt("Idioma") == 0 ? "Vazio" : "Kupré";
        if (profile != null)
        {
            switch (profile.playerType)
            {
                case PlayerType.KAME_FEMALE:
                    pb.image.sprite = KameSprite[0];
                    break;
                case PlayerType.KAME_MALE:
                    pb.image.sprite = KameSprite[0];
                    break;
                case PlayerType.KANHRU_FEMALE:
                    pb.image.sprite = KairuSprite[0];
                    break;
                case PlayerType.KANHRU_MALE:
                    pb.image.sprite = KairuSprite[0];
                    break;
                default:
                    pb.image.sprite = OffSprite[0];
                    break;
            }
            tmp_name = profile.name;
        }
        myName.text = tmp_name;

    }

    public void OnSelect() // metodo chamado ao clicar no slot
    {
        if (profile == null)
            mainMenuScript.OpenNameInsertScreen();
        else
        {
            ProfileManager.Instance.LoadProfile(N_Button);
            mainMenuScript.GoToMainMenu();
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
