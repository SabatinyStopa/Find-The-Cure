using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour{
    public void Quit(){
        Application.Quit();
    }
    public void Play(){
        SceneManager.LoadScene("Play");
        Time.timeScale = 1;
    }

    public void MainMenu(){
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
