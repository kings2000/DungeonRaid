using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    int Count { get; }
    void Put(int id);
    void Get(int id); //by type

}



public class InventoryBase : IInventory
{
    private Dictionary<int, IInventory> inventoryItems = new Dictionary<int, IInventory>();
    public int Count => throw new System.NotImplementedException();

    public void Put(int id)
    {
        
    }

    public void Get(int id)
    {
        
    }
}
