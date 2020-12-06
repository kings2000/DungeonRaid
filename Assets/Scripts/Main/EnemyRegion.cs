using UnityEngine;
using System.Collections;

public class EnemyRegion : MonoBehaviour
{

    public Transform spriteRenderer;
    SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        spriteRenderer.gameObject.SetActive( true);
    }

    private void OnTriggerExit(Collider other)
    {
        spriteRenderer.gameObject.SetActive(false);
    }

    public void SetAreaSize(float size)
    {
        if(sphereCollider == null)
            sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = size;
        spriteRenderer.localScale *= size * 2;
    }
}
