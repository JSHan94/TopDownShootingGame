using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

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
            player = GameObject.Find("Custom Player (1)");    
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Debug.Log("2 selected!!");
            player = GameObject.Find("Custom Player (2)");
        }
        transform.position = player.transform.position + offset;
    }
}
