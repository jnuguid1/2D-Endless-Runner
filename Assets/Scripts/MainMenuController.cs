using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    bool isEndlessMode = false;

    public void playGame()
    {
        SceneManager.LoadScene("ChooseMode"); //this will have the name of your main game scene
    }

    public void restart()
    {
        SceneManager.LoadScene("StartMenu"); //this will have the name of your main menu scene
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void endless()
    {
        PlayerPrefs.SetInt("isEndless", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public void quick()
    {
        PlayerPrefs.SetInt("isEndless", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}