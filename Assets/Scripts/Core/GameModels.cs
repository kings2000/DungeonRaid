using Enums;
using System.Collections;
using UnityEngine;

public abstract class GameModels
{
    [System.Serializable]
    public class ItemProps
    {
        public string name;
        public int roomSpace; //the amount of space the item can take in the inventory cell space
        public ItemCategory itemCategory;
        public Sprite image;
    }

    public class FoodItemProps : ItemProps
    {
        public int healthIncreamentAmount;

    }

    public class ConstrunctionItemPropes : ItemProps
    {
        
    }

}