using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;
using System;

[CreateAssetMenu(menuName = Constants.EDITOR_MENU_PREFIX+"/Factories/" + nameof(UiItemFactory))]
public class UiItemFactory : ObjectFactories
{
    public ActionButtonItem actionButtonItem;
    

    private Dictionary<Type, Queue<GameObject>> uiItemsPool;

    public void OnEnable()
    {
        uiItemsPool = new Dictionary<Type, Queue<GameObject>>();
    }

    /// <summary>
    /// Get a ui gameobject and send it to the UI scene
    /// </summary>
    /// <param name="item">The item</param>
    /// <returns></returns>
    public T GetItem<T>(T item) where T : MonoBehaviour
    {
        T ins = null;
        //check pool for recycled items
        if (uiItemsPool.TryGetValue(item.GetType(), out Queue<GameObject> items))
        {
            //if item exsit extract it and remove from the pool
            if(items.Count > 0)
            {
                ins = items.Dequeue().GetComponent<T>();
                ins.gameObject.SetActive(true);
                return ins;
            }
            
        }
        //create a new instance of that asset
        ins = CreateObject(item, true ,Constants.UI_SCENE);
        ins.transform.localScale = Vector3.one;
        return ins;
    }

    //Pools the item for easy load 
    public override void Recycle<T>(T obj)
    {
        obj.gameObject.SetActive(false);
        if (uiItemsPool.ContainsKey(obj.GetType()))
        {
            uiItemsPool[obj.GetType()].Enqueue(obj.gameObject);
            return;
        }
        uiItemsPool.Add(obj.GetType(), new Queue<GameObject>());
        uiItemsPool[obj.GetType()].Enqueue(obj.gameObject);
        
    }
}