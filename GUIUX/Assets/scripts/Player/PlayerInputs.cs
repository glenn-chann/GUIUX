using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private GameObject settingsMenu;
    private GameObject radialMenu;
    private PlayerCam playerCam;

    private GameObject GUI;

    private GameObject InvCraftMenu;

    private GameObject buildingMenu;
    private GameObject book;

    bool inMenu;

    public Crafting crafting;
    public Equip equip;
    public InventoryManager inventoryManager;
    public BuildingMenuController buildingMenuController;

    private void Awake()
    {
        settingsMenu = GameObject.Find("Settings");
        radialMenu = GameObject.Find("Radial Menu");
        buildingMenu = GameObject.Find("BuildingMenuUI");
        book = GameObject.Find("BuildingMenu");
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
        radialMenu.SetActive(false);
        buildingMenu.SetActive(false);
        book.SetActive(false);
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
            else if (!inMenu)
            {
                disableMouse();
                settingsMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AudioManager.Instance.PlaySfX("ButtonClick");
            if (InvCraftMenu.activeSelf || inMenu)
            {
                InvCraftMenu.SetActive(false);
                enableMouse();
                InventoryManager.Instance.DestroyItems();
            }
            else if (!inMenu)
            {
                InvCraftMenu.SetActive(true);
                disableMouse();
                InventoryManager.Instance.ListItems();
                crafting.UpdateColor();
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            AudioManager.Instance.PlaySfX("ButtonClick");
            if (radialMenu.activeSelf || inMenu)
            {
                radialMenu.SetActive(false);
                enableMouse();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AudioManager.Instance.PlaySfX("ButtonClick");
            if (!inMenu)
            {
                radialMenu.SetActive(true);
                disableMouse();
                equip.DisablePickaxeBtn();
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AudioManager.Instance.PlaySfX("ButtonClick");
            if (buildingMenu.activeSelf || inMenu)
            {
                buildingMenu.SetActive(false);
                book.SetActive(false);
                enableMouse();
            }
            else if (!inMenu)
            {
                buildingMenu.SetActive(true);
                buildingMenuController.UpdateBuildMenu();
                book.SetActive(true);
                disableMouse();
                crafting.UpdateColor();
            }
        }
    }

    void CloseAllMenus()
    {
        settingsMenu.SetActive(false);
        InvCraftMenu.SetActive(false);
        radialMenu.SetActive(false);
        book.SetActive(false);
        buildingMenu.SetActive(false);
        enableMouse();
    }

    void enableMouse()
    {
        inMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCam.enabled = true;
        GUI.SetActive(true);
    }

    void disableMouse()
    {
        inMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerCam.enabled = false;
        GUI.SetActive(false);  
    }
}

