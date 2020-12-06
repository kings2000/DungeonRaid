using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SinglitonObjects<CameraController>
{

    public float camSpeed;
    public Transform target;

    private bool hasTarget;

    private void Start()
    {
        if(target != null)
        {
            hasTarget = true;
        }
    }

    public void SetCurrentTarget(Transform _target)
    {
        hasTarget = false;
        target = _target;
        hasTarget = true;
    }

    void FixedUpdate()
    {
        if(hasTarget)
        {
            Vector3 topos = target.position;
            topos.y = 0;
            transform.position = Vector3.Lerp(transform.position, topos, camSpeed * Time.fixedDeltaTime);
        }
    }
}
