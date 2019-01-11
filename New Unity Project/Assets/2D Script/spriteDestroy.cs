using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteDestroy : MonoBehaviour
{
    float lifetime;

    void Start()
    {
        lifetime = 0;
    }

    void Update()
    {
        lifetime++;
        Debug.Log(lifetime);
        if (lifetime > 1000)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
