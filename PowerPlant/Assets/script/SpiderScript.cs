using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public AudioClip _audioClip;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Get the target immediately on start
    }

    void Update()
    {
        if (!target) GetTarget();
    }

    private void FixedUpdate()
    {
        if (target)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
    }

    private void GetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            target = player.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("laser"))
         {
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Debug.Log("Spider hit!");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(collision.gameObject);
         }

    }
}
