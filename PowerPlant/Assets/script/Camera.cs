using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        // Set the camera position to the player's position plus the offset
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
