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

    int currentItem = 0;

    public Image image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;

    public GameObject redCover;

    public InventoryManager invManager;

    bool isCraftable;
    int NoOfStick;

    List<Item> itemsToRemove = new List<Item>();
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

    private void Update()
    {
        CreateCampfireHologram();
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
            if (item.id == 2)
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
        if (isCraftable)
        {
            
        }
    }

    void CreateCampfireHologram()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(campfirePrefab, hit.transform.position, Quaternion.identity);
        }
    }
}
