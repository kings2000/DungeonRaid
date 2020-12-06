using System.Collections;
using UnityEngine;

public abstract class GameModels
{

    public class ItemProps
    {
        public string name;
    }

    public class FoodItemProps : ItemProps
    {
        
        public int healthIncreamentAmount;

    }

    public class ConstrunctionItemPropes : ItemProps
    {
        
    }

}