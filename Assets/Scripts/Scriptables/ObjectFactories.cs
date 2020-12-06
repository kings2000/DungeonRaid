using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public abstract class ObjectFactories : ScriptableObject
{
    Scene contentScene;

    public virtual void Init(){}

    protected T CreateObject<T>(T item, bool moveToScene = true, string sceneName="") where T : MonoBehaviour
    {
        
        Assert.IsTrue(item != null);
        T instance = Instantiate(item);
        if(moveToScene)
            MoveObjetToScene(instance.gameObject, sceneName);
        return instance;
    }

    protected GameObject CreateObject(GameObject item, bool moveToScene = true, string sceneName = "")
    {

        Assert.IsTrue(item != null);
        GameObject instance = Instantiate(item);
        if (moveToScene)
            MoveObjetToScene(instance.gameObject, sceneName);
        return instance;
    }

    protected void MoveObjetToScene(GameObject o, string sceneName = "")
    {
        Assert.IsTrue(!string.IsNullOrEmpty(sceneName));

        contentScene = SceneManager.GetSceneByName(sceneName);

        if (!contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                contentScene = SceneManager.GetSceneByName(sceneName);
                if (!contentScene.isLoaded)
                {
                    contentScene = SceneManager.CreateScene(sceneName);
                }
            }
            else
            {

                contentScene = SceneManager.CreateScene(sceneName);
            }

        }

        SceneManager.MoveGameObjectToScene(o, contentScene);
    }

    public virtual void Recycle<T>(T obj) where T : MonoBehaviour { }
    public virtual void Recycle(GameObject obj) { }

    public void Reclaim<T>(T obj, float time = 0.0f) where T : MonoBehaviour
    {
        Destroy(obj.gameObject, time);
    }

    public void Reclaim(GameObject obj, float time = 0.0f)
    {
        Destroy(obj.gameObject, time);
    }

    public virtual void Dispose(){}

    protected void ResetPools<T>(List<T> pools) where T : MonoBehaviour
    {
        //Debug.Log("A pool request");
        if (pools.Count > 0)
        {
            int len = pools.Count;
            for (int i = 0; i < len; i++)
            {
                if (pools[i] != null)
                {
                    Destroy(pools[i].gameObject);
                }
            }
        }
        pools = new List<T>();
    }

    
}
