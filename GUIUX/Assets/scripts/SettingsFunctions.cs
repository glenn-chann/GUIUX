using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SettingsFunctions : MonoBehaviour
{
    private static SettingsFunctions Instance;

    public Button Video;
    public Button Audio;
    public Button Controls;
    public Button Accessibility;

    public GameObject VideoSettings;
    public GameObject AudioSettings;
    public GameObject ControlsSettings;
    public GameObject AccessibilitySettings;

    public GameObject settings;
    public GameObject mainMenu;

    private PlayerCam playerCam;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartSettings()
    {
        DisableSettings();
        VideoSettings.SetActive(true);
        Video.Select();
    }

    private void DisableSettings()
    {
        VideoSettings.SetActive(false);
        AudioSettings.SetActive(false);       
        ControlsSettings.SetActive(false);
        AccessibilitySettings.SetActive(false);
    }

    public void VideoButton()
    {
        DisableSettings();
        VideoSettings.SetActive(true);
    }

    public void AudioButton()
    {
        DisableSettings();
        AudioSettings.SetActive(true);
    }

    public void ControlsButton()
    {
        DisableSettings();
        ControlsSettings.SetActive(true);
    }

    public void AccessibilityButton()
    {
        DisableSettings();
        AccessibilitySettings.SetActive(true);
    }

    public void CloseButton()
    {
        AudioManager.Instance.PlaySfX("ButtonClick");
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
            settings.SetActive(false);
            
        }
        else
        {
            if (playerCam == null) 
            {
                playerCam = GameObject.Find("Camera").GetComponent<PlayerCam>();
                playerCam.enabled = true;
                settings.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                playerCam.enabled = true;
                settings.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
