using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterScript : MonoBehaviour
{
    public void Home() // Vai para o menu principal do jogo
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(PlayerPrefs.GetString("Home"));
    }

    public void Restart() // Vai para o menu principal do jogo
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void RestartStatic() // Vai para o menu principal do jogo
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void HomeStatic()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        //StartCoroutine(LoadAsyncScene("Menu"));
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void Exit() // Fecha o jogo
    {
        Application.Quit();
    }
}
