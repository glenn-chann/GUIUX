using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private GameObject settingsMenu;
    private PlayerCam playerCam;

    private GameObject GUI;

    private GameObject InvCraftMenu;

    bool inMenu;

    public Crafting crafting;

    private void Awake()
    {
        settingsMenu = GameObject.Find("Settings");
        GUI = GameObject.Find("GUI");
        InvCraftMenu = GameObject.Find("Inventory&Crafting");
        playerCam = GameObject.Find("Camera").GetComponent<PlayerCam>();
    }

    // Start is called before the first frame update
    void Start()
    {
        inMenu = false;
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(false);
        }
        InvCraftMenu.SetActive(false);
        GUI.SetActive(true);
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
            AudioManager.Instance.PlaySfX("ButtonClick");
            if (settingsMenu.activeSelf || inMenu)
            {
                CloseAllMenus();
            }
            else
            {
                inMenu = true;
                playerCam.enabled = false;
                settingsMenu.SetActive(true);
                GUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AudioManager.Instance.PlaySfX("ButtonClick");
            if (InvCraftMenu.activeSelf || inMenu)
            {
                CloseAllMenus();
            }
            else
            {
                inMenu = true;
                playerCam.enabled = false;
                InvCraftMenu.SetActive(true);
                GUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                InventoryManager.Instance.ListItems();
                crafting.UpdateColor();
            }
        }
    }

    void CloseAllMenus()
    {
        inMenu = false;
        settingsMenu.SetActive(false);
        InvCraftMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCam.enabled = true;
        GUI.SetActive(true);
    }

}

