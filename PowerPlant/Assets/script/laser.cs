using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //public float laserSpeed = 10f;
    void Start()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = GameObject.Find("player").GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, enemyCollider);
    }
    void Update()
    {
        //transform.Translate(Vector2.right * laserSpeed * Time.deltaTime);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(gameObject);  
    }
    */
}