using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equip : MonoBehaviour
{
    public Crafting crafting;
    public GameObject PickaxeBtn;

    public GameObject Pickaxe;

    bool isEquipped;
    public void EquipPickaxe()
    {
        if (crafting.pickaxeCrafted)
        {
            isEquipped = !isEquipped;
        }
        else
        {
            isEquipped = false;
        }
    }

    private void Update()
    {
        Pickaxe.SetActive(isEquipped);
    }

    public void DisablePickaxeBtn()
    {
        PickaxeBtn.SetActive(crafting.pickaxeCrafted);
    }
}
