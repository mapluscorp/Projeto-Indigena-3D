using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PescariaScript : MonoBehaviour
{
    public GameObject[] DesactivateAtStart;
    public GameObject[] ActivateAtStart;
    public GameObject[] ActivateAtGameStart;
    public GameObject[] Peixinhos;
    public GameObject menu;
    public GameObject left_HUD;
    public GameObject right_HUD;
    public GameObject FimDeJogo;
    public GameObject Water2;

    public AudioSource musicSource;
    public AudioSource menuSource;
    public AudioClip menuSound;

    public static short peixinhosObjetivo;
    private short peixinho = 4;

    private void Start()
    {
        //Camera.main.aspect = 1.6f;
        Time.timeScale = 1;

        foreach (GameObject g in DesactivateAtStart) // Desativa os objetos do vetor ao iniciar o jogo
            g.SetActive(false);
        foreach (GameObject g in ActivateAtStart) // Ativa os objetos do vetor ao iniciar o jogo
            g.SetActive(true);
        left_HUD.transform.position = new Vector3(0, Screen.height); // Seta a posicao dos botoes do canto da tela
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf && !FimDeJogo.activeSelf)
            AbrirMenu();
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
            FecharMenu();*/
    }

    public void AtualizarPeixinho(bool correto) // Indicador de quantos peixes faltam
    {
        if (correto) // Peixe correto foi atingido, diminui a quantia de peixinhos que faltam
        {
            //Peixinhos[peixinho].SetActive(false);
            Peixinhos[peixinho].GetComponent<Animator>().SetTrigger("Acertou");
            peixinho--;
        }
        else // Peixe incorreto foi atingido. Caso os peixes nao estejam no seu limite, incrementa o numero de peixinhos que faltam
        {
            if (peixinho < peixinhosObjetivo - 1)
            {
                peixinho++;
                Peixinhos[peixinho].GetComponent<Animator>().SetTrigger("Errou");
            }
            //Peixinhos[peixinho].SetActive(true);
        }

        if (peixinho < 0) // Pegou todos os peixes que precisava
            FimDeJogo.SetActive(true);
    }

    public void AtivarItensIniciarJogo() // Metodo chamado apos sair da tela de tutorial e iniciar o jogo de fato
    {
        peixinho = (short)(peixinhosObjetivo-1);
        foreach (GameObject g in ActivateAtGameStart)
            g.SetActive(true);
        print("Peixinhos objetivo: " + peixinhosObjetivo);
        for (short i = 0; i < peixinhosObjetivo; i++) // Ativa apenas a quantia de peixinhos necessario para esta dificuldade
            Peixinhos[i].SetActive(true);
    }

    public void AbrirMenu() // Abre a tela do menu e pause o jogo
    {
        menuSource.clip = menuSound;
        menuSource.Play();
        musicSource.volume = 0.3f;
        SetTimeScale(0);
        menu.SetActive(true);
    }

    public void SetTimeScale(int value)
    {
        Time.timeScale = value;
    }

    public void FecharMenu() // Quem esta fechando o menu eh o blackout
    {
        /*menuSource.clip = menuSound;
        menuSource.Play();*/
        musicSource.volume = 1;
        Time.timeScale = 1;
        menu.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton() // Vai para o menu
    {
        PlayerPrefs.SetInt("HomeController", 1);
        SceneManager.LoadScene("Menu");
    }

}