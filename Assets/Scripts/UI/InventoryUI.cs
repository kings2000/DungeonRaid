using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public IInventory currentInventory;
    public Transform gridObject;


    private Dictionary<int, string> slot;

    void Start()
    {
        int chiledCount = gridObject.childCount;
        print(chiledCount);
        for (int i = 0; i < chiledCount; i++)
        {
            Transform c = gridObject.GetChild(i);
            Image img = c.GetChild(1).GetComponent<Image>();
            img.sprite = null;
            img.enabled = false;
            //print(c.name);
        }
    }

    public void Open(IInventory inventory)
    {
        currentInventory = inventory;
        slot = new Dictionary<int, string>();
        Dictionary<string, InventoyItem> inventoryItems = inventory.GetItems();
        foreach (var item in inventoryItems.Keys)
        {

        }
    }

    public void UpdateUI()
    {

    }




}
