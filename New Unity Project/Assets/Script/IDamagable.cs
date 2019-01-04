using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    // Start is called before the first frame update
    void TakeHit(float damage, RaycastHit hit);
    void TakeDamage(float damage);
}
