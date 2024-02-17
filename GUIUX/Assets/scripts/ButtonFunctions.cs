using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;

    public SettingsFunctions settingsFunction;

    private void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    private void Update()
    {
        if (settings.activeSelf && Input.GetKeyDown(KeyCode.Escape)) 
        {
            mainMenu.SetActive(true);
            settings.SetActive(false);
        }
    }
    public void StartButton()
    {
        Debug.Log("Start Button Pressed");
        SceneManager.LoadScene("Game");
    }

    public void SettingButton()
    {
        Debug.Log("Settings Button Pressed");
        mainMenu.SetActive(false);
        settings.SetActive(true);
        settingsFunction.StartSettings();
    }

    public void QuitButton()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();
    }
}
