using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using System;

public class SettingsFunctions : MonoBehaviour
{
    private static SettingsFunctions Instance;

    public Button Video;
    public Button Audio;
    public Button Controls;
    public Button Accessibility;

    public Toggle Vsync;
    public Toggle Fullscreen;

    public GameObject VideoSettings;
    public GameObject AudioSettings;
    public GameObject ControlsSettings;
    public GameObject AccessibilitySettings;

    public GameObject settings;
    public GameObject mainMenu;

    private PlayerCam playerCam;

    Resolution[] resolutions;

    int selectedResolution;

    public TMP_Text resolutionLabel;

    private void Start()
    {
        resolutions = Screen.resolutions;

        if (QualitySettings.vSyncCount == 0)
        {
            Vsync.isOn = false;
        }
        else
        {
            Vsync.isOn = true;
        }

        Fullscreen.isOn = Screen.fullScreen;

        for(int i = resolutions.Length - 1; i > 0; i--)
        {
            if(Screen.width == resolutions[i].width && Screen.height == resolutions[i].height && Screen.currentResolution.refreshRateRatio.CompareTo(resolutions[i].refreshRateRatio) == 0)
            {
                selectedResolution = i;
                UpdateResLabel();
            }
        }
    }

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


    public void ResLeft()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Length - 1)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = resolutions.Length - 1;
        }

        UpdateResLabel();
    }
    void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].ToString();
    }

    public void ApplyButton()
    {
        AudioManager.Instance.PlaySfX("ButtonClick");
        if (Vsync.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].width, resolutions[selectedResolution].height, Fullscreen.isOn);
    }
}
