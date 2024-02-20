using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenuController : MonoBehaviour
{
    public List<BuildableItems> bItems;

    public GameObject campfirePrefab;
    public GameObject campfireLitPrefab;

    int currentItem = 0;

    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;

    public GameObject redCover;

    public InventoryManager invManager;
    public PlayerInputs playerInputs;

    bool isCraftable;
    bool building;
    int NoOfStick;

    GameObject campfire;

    GameObject crosshair;

    List<Item> itemsToRemove = new List<Item>();

    Vector3 prevPoint;

    private void Start()
    {
        crosshair = GameObject.Find("crosshair");
    }
    private void Update()
    {
        if(building)
        {
            UpdateCampfirePosition();
            if (Input.GetMouseButtonDown(0))
            {
                building = false;
                Instantiate(campfireLitPrefab, campfire.transform.position, Quaternion.identity);
                Destroy(campfire);
            }
        }
    }
    public void RightArrow()
    {
        AudioManager.Instance.PlaySfX("ButtonClick");
        currentItem++;
        if (currentItem > bItems.Count - 1)
        {
            currentItem = 0;
        }
        UpdateBuildMenu();
    }

    public void LeftArrow()
    {
        AudioManager.Instance.PlaySfX("ButtonClick");
        currentItem--;
        if (currentItem < 0)
        {
            currentItem = bItems.Count - 1;
        }
        UpdateBuildMenu();
    }

    public void UpdateBuildMenu()
    {
        image.sprite = bItems[currentItem].Image;
        title.text = bItems[currentItem].Title;
        desc.text = bItems[currentItem].Description;
        UpdateCover();
    }

    public void UpdateCover()
    {
        if (CheckCampfire() && currentItem == 1)
        {
            redCover.SetActive(false);
        }
        else
        {
            redCover.SetActive(true);
        }
    }

    bool CheckCampfire()
    {
        if (isCraftable)
        {
            return true;
        }
        foreach (var item in invManager.Items)
        {
            if (item.id == 2 && NoOfStick < 3)
            {
                NoOfStick++;
                itemsToRemove.Add(item);
            }
        }
        if (NoOfStick >= 3)
        {
            Debug.Log("Enough");
            isCraftable = true;
            NoOfStick = 0;
            return true;
        }
        Debug.Log("Not Enough");
        itemsToRemove.Clear();
        NoOfStick = 0;
        return false;
    }

    public void BuildCampfireBtn()
    {
        if (isCraftable && CreateCampfireHologram())
        {
            foreach (var item in itemsToRemove)
            {
                invManager.Remove(item);
            }
            isCraftable = false;
            InventoryManager.Instance.DestroyItems();
        }
        UpdateCover();
        playerInputs.buildingMenu.SetActive(false);
        playerInputs.book.SetActive(false);
        playerInputs.enableMouse();
        building = true;
    }

    bool CreateCampfireHologram()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("ground")))
        {
            Debug.Log(hit.collider.name);
            Debug.Log(hit.transform.position);
            campfire = Instantiate(campfirePrefab, hit.point, Quaternion.identity);
            prevPoint = hit.point;
            return true;
        }
        return false;
    }

    void UpdateCampfirePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("ground")) && hit.point != prevPoint)
        {
            campfire.transform.position = hit.point;
        }
    }
}
