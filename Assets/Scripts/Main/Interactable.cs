using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{

    public float scanRadius;
    private LayerMask scanMask;

    public void Start()
    {
        scanMask = LayerMask.GetMask(Constants.PlayerMask);
    }

    

    public void Interact()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name); 
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        GizmosManager.DrawCircle(transform.position, scanRadius);
    }
}
