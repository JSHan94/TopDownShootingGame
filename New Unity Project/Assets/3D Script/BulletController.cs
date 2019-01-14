using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RonnieJ.ObjectPool;

public class BulletController : MonoBehaviour
{
    public string poolItemName = "Bullet";
    public float moveSpeed = 10f;
    public float lifeTime = 3f;
    public float _elapsedTime = 0f;

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if(GetTimer() > lifeTime)
        {
            SetTimer();
            ObjectPool.Instance.PushToPool(poolItemName, gameObject);
        }
    }
    float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }
    void SetTimer()
    {
        _elapsedTime = 0f;
    }
}
