using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bulletTest : MonoBehaviour
{
    public Transform bulletprefab;
    public float bulletStartPositionX;
    public float bulletStartPositionY;
    Transform[] bullet = new Transform[5000];
    private void Start()
    {
        for (int i = 0; i < 5000; i++)
        {
            bullet[i] = Instantiate(bulletprefab, new Vector3(bulletStartPositionX, bulletStartPositionY), Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletStartPositionX += 0.03f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < 5000; i++)
        {
            float X = bullet[i].position.x - 1;
            float Y = bullet[i].position.y - 1;
            //sDestroy(bullet[i]);
            //bullet[i] = Instantiate(bulletprefab, new Vector3(X, 0,Y), Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletStartPositionX += 0.03f;
            //Vector3.MoveTowards(bullet[i].position, new Vector3(bullet[i].position.x - 1, bullet[i].position.y - 1), Time.deltaTime * 2);
        }
    }
}
