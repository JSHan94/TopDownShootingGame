﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enemy와 Player등 살아있는 Entity 관리
public class LivingEntity : MonoBehaviour,IDamagable
{
    public float startinghealth;
    protected float health;
    protected bool dead;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        health = startinghealth;
    }
 
    public void TakeHit(float damage, RaycastHit hit)
    {
        TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    protected void Die()
    {
        dead = true;
        if(OnDeath != null)
        {
            OnDeath();
        }

        GameObject.Destroy(gameObject);
    }
}
