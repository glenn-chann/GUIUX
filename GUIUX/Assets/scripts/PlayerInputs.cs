using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private GameObject settingsMenu;
    private PlayerCam playerCam;

    private void Awake()
    {
        settingsMenu = GameObject.Find("SettingsCanvas");
        playerCam = GameObject.Find("Camera").GetComponent<PlayerCam>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
    }

    public void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                playerCam.enabled = true;
                settingsMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                playerCam.enabled = false;
                settingsMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}

