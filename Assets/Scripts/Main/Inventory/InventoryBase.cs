using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    int Count { get; }
    void Put(InventoyItem item);
    Dictionary<string, InventoyItem> GetItems(); //by type
    
}



public class InventoryBase : MonoBehaviour, IInventory
{
    private Dictionary<string, InventoyItem> inventoryItems = new Dictionary<string, InventoyItem>();
    public int maxCellCount;
    public int Count => throw new System.NotImplementedException();

    public virtual void Put(InventoyItem item)
    {
        
    }

    public virtual Dictionary<string, InventoyItem> GetItems()
    {
        return inventoryItems;
    }
}
