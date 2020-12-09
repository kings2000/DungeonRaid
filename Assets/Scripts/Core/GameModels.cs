using Enums;
using System.Collections;
using UnityEngine;

public abstract class GameModels
{
    [System.Serializable]
    public class ItemProps
    {
        public string name;
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