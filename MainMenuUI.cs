using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour     //Podłączony na obiektu MainMenuUI, zawiera funkcje menu
{

    public void ReloadScene()   //Resetuje scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitApp()   //Wyjście z aplikacji
    {
        Application.Quit();
    }
}
