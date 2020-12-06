using UnityEngine;
using System.Collections;

public interface IDamagable
{

    void TakeDamage(int percent);
    void AddHealth(int percent);

}
