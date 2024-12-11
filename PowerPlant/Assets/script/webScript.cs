using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class webScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D enemyCollider = GameObject.Find("SpiderBoss").GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, enemyCollider);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object has the tag "spider" or "beetle"
        if (collision.gameObject.tag == "Spider" || collision.gameObject.tag == "Beetle")
        {
            // Do nothing if it collides with these tags
            return;
        }

        // Destroy the web on collision with any other object
        Destroy(gameObject);
    }
}
