using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    public float dragSpeed = 2;
    private Vector3 dragOrigin;


    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
        offset = Vector3.up * 10;
        transform.position = player.transform.position + offset;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
       
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("1 selected!!");
            player = GameObject.Find("A1");
            transform.position = player.transform.position + offset;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("2 selected!!");
            player = GameObject.Find("A2");
            transform.position = player.transform.position + offset;
        }


        //Manual tracking
        float step = 10.0f * Time.deltaTime;   
        float Xmove = Input.GetAxis("Horizontal") * step;
        float Ymove = Input.GetAxis("Vertical") * step;
        transform.position += new Vector3(Xmove, 0.0f,Ymove);
        
    }

}
