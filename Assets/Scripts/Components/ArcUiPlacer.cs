using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[ExecuteAlways]
public class ArcUiPlacer : MonoBehaviour
{


    //public RectTransform[] items = default;
    public float maxAngle;
    public float minAngle;
    [Range(0,1)]public float radius;


    private RectTransform rectTransform;
    private List<GameObject> currentItems = new List<GameObject>();

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnValidate()
    {
        UpdateChildren();
    }

    void UpdateChildren()
    {
        RectTransform[] children = UnityEngineEx.GetComponentsInDirectChildren<RectTransform>(this, false);
        float childCount = children.Length;
        rectTransform = GetComponent<RectTransform>();
        if (childCount > 0)
        {
            Vector3 size = rectTransform.sizeDelta;
            float averageAngle = (maxAngle - minAngle) / (childCount + 1f);
            float currentAngle = maxAngle - averageAngle;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = children[i];
                if (child.gameObject.activeSelf)
                {
                    Vector3 dir = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
                    Vector3 localPos = Vector3.Scale(size, dir) * radius; // new Vector3(size.x * dir.x * radius, size.y * dir.y * radius);
                    child.localPosition = localPos;

                    currentAngle -= averageAngle;
                }
            }
        }
    }


    public void ClearItems()
    {
        for (int i = 0; i < currentItems.Count; i++)
        {
            currentItems[i].SetActive(false);
            FactoryManager.Instance.uiItemFactory.Reclaim(currentItems[i]);
        }
        currentItems = new List<GameObject>();
        //UpdateChildren();
    }

    public void UpdatePositions()
    {
        UpdateChildren();
    }

    public void AddItem(GameObject item)
    {
        item.transform.SetParent(transform);
        item.transform.localScale = Vector3.one;
        currentItems.Add(item);
        UpdateChildren();
    }

}
