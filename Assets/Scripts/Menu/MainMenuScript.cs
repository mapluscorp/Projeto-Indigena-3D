using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject[] enableAtStart;
    public GameObject[] disableAtStart;

    [Header("Screens")]
    public GameObject nameInsertionScreen; // tela de insercao de nome
    public GameObject choiceScreen; // tela de escolha entre kame e kairu
    public GameObject meninoMeninaScreen; // tela de escolha entre menino e menina
    public GameObject mainMenuScreen; // tela do menu principal
    public GameObject alertIcon; // alerta ao nao inserir o nome
    public GameObject kameCharacters; // opcoes de personagens kame
    public GameObject kairuCharacters; // opcoes de personagens kairu

    [Header("Background")]
    public Sprite kameBackground; // fundo do menu principal
    public Sprite kairuBackground; // fundo do menu principal
    public Image menino_menina_background;

    [Header("Slots")]
    public Slots_Button_Script[] slots;

    [Header("Transition")]
    public GameObject insertNameToChooseTransition;
    public GameObject typeChooseBoyGirlTransition;
    public GameObject BoyGirlToMenuTransition;

    public GameObject charBlinking;

    private string playerName;
    public static int selectedSlot;

    public static bool hasAlreadyLogedIn = false;

    private void Start()
    {
        foreach (GameObject g in enableAtStart)
            g.SetActive(true);

        foreach (GameObject g in disableAtStart)
            g.SetActive(false);

        if (hasAlreadyLogedIn) // caso esteja voltando de outra tela para o menu principal
            mainMenuScreen.SetActive(true);
    }

    public void OpenNameInsertScreen() // chamado pelo slot vazio
    {
        nameInsertionScreen.SetActive(true);
    }

    public void OnSelectGroupType(int type) // on select kame ou kairu
    {
        PlayerPrefs.SetString("Type" + selectedSlot.ToString(), type == 0 ? "Kame" : "Kairu");
        menino_menina_background.sprite = PlayerPrefs.GetString("Type" + selectedSlot.ToString()) == "Kame" ? kameBackground : kairuBackground;
        GoToSexSelection();
    }

    public void OnSelectBoyOrGirl(int value) // ao selecionar menino ou menina | 0 == Menino , 1 == Menina
    {
        PlayerPrefs.SetString("Sex" + selectedSlot.ToString(), value == 0 ? "Menino" : "Menina"); // 0 == Menino , 1 == Menina
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        mainMenuScreen.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = PlayerPrefs.GetString("Type" + selectedSlot.ToString()) == "Kame" ? kameBackground : kairuBackground;
        BoyGirlToMenuTransition.SetActive(true);
        hasAlreadyLogedIn = true;
    }

    public void GoToSexSelection()
    {
        typeChooseBoyGirlTransition.SetActive(true);
        if (PlayerPrefs.GetString("Type" + selectedSlot.ToString()) == "Kame")
        {
            kameCharacters.SetActive(true);
            kairuCharacters.SetActive(false);
        }
        else
        {
            kairuCharacters.SetActive(true);
            kameCharacters.SetActive(false);
        }
    }

    public void OnNameInputField(string name)
    {
        playerName = name;
        if(playerName.Length > 0) 
        {
            alertIcon.SetActive(false);
            charBlinking.SetActive(false);
        }
        else
            charBlinking.SetActive(true);
    }

    public void CheckName() // Chamado ao clicar em continuar na tela de inserir o nome
    {
        if(playerName.Length > 0)
        {
            PlayerPrefs.SetString("Name" + selectedSlot, playerName); // armazena o nome do jogador
            insertNameToChooseTransition.SetActive(true);
        } 
        else
        {
            alertIcon.SetActive(true);
        }
    }

    public void SetCurrentSlot(int value) // chamado pelo trash button
    {
        selectedSlot = value;
        PlayerPrefs.SetInt("SelectedSlot", value);
    }

    public void CleanSlot() // chamado ao confirmar a exclusao
    {
        PlayerPrefs.SetString("Name" + selectedSlot.ToString(), "");
        PlayerPrefs.SetString("Type" + selectedSlot.ToString(), "");
        PlayerPrefs.SetString("Sex" + selectedSlot.ToString(), "");
        RefreshSlots();
    }

    public void RefreshSlots()
    {
        foreach(Slots_Button_Script slot in slots)
        {
            slot.Refresh();
        }
    }

}
