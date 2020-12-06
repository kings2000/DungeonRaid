using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour
{

    protected void Update()
    {
        Vector3 relativePos = Camera.main.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.FromToRotation(Vector3.back, relativePos);
        transform.rotation = rotation;
    }
}
