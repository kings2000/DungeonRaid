using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public abstract class LivingEntity : MonoBehaviour, IDamagable
{

    [HideInInspector] public int currentHealthCount;

 
    public bool GetClosetObject(string Tag, out GameObject _gameObject)
    {
        bool found = false;
        GameObject[] possibleTarget = GameObject.FindGameObjectsWithTag(Tag);
        float dist = float.MaxValue;
        GameObject closestTarget = null;
        for (int i = 0; i < possibleTarget.Length; i++)
        {
            float _d = Vector3.Distance(transform.position, possibleTarget[i].transform.position);
            if (_d < dist)
            {
                dist = _d;
                closestTarget = possibleTarget[i];
                found = true;
            }

        }
        _gameObject = closestTarget;
        return found;
        
    }

    public Vector3[] GeneratePathPoints(Vector3 target)
    {
        NavMeshPath path = new NavMeshPath();
        bool found = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
        return path.corners;
    }

    public float GetHealthPercent()
    {
        return 0;// "0 to 1"
    }

    public void TakeDamage(int percent)
    {
        currentHealthCount -= percent;
        currentHealthCount = Mathf.Clamp(currentHealthCount, 0, int.MaxValue);
    }

    public void AddHealth(int percent)
    {
        currentHealthCount += percent;
    }
}
