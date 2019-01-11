using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritePosition : MonoBehaviour
{
    public float width = 1;
    public float height = 1;
    int t=0;//fps (update 될때마다 1씩값 증가)
    public Vector3 position = new Vector3(10, 5, 0);
    float[] array = new float[2000];
    private void Start()
    {
       for(int i=0; i < 1000; i++)
        {
            array[i] = i / 100; //이동거리 세팅
        }
    }
    void Update()
    {
        // set the scaling
        // Vector3 scale = new Vector3(width, height, 1f);
        // transform.localScale = scale;
        // set the position
        if (t < 1000)
        {
            position = new Vector3(0, array[t], 0);
            transform.position = position;
            t++;
            Debug.Log(t);
            Debug.Log(array[t]);
            Debug.Log(position);
        }
    }
}
