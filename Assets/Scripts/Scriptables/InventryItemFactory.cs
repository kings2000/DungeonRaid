using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(menuName = Constants.EDITOR_MENU_PREFIX + "/Factories/" + nameof(InventryItemFactory))]
public class InventryItemFactory : ScriptableObject
{
    public List<InventoyItem> inventoyItems;

    public void GetItem()
    {

    }
}

[System.Serializable]
public class InventoyItem
{
    public int count;
    public int maxPackSize; //The amount of this item that can be in one pack
    public Sprite image;
    public GameModels.ItemProps ItemProps;
    public ItemCategory itemCategory;
}