using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public AudioClip _audioClip;
    private bool isDestroyed = false; 
    private int hitCounter = 2;
    
    public delegate void DestroyedAction();
    public event DestroyedAction OnDestroyed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Get the target immediately on start
    }

    void Update()
    {
        if (isDestroyed) return; 
        if (!target) GetTarget();
    }

    private void FixedUpdate()
    {
        if (isDestroyed) return; //ends
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
         if (collision.gameObject.CompareTag("laser")){
         if (hitCounter==0)
            {
                isDestroyed = true;
                AudioSource.PlayClipAtPoint(_audioClip, transform.position);
                Debug.Log("Spider hit!");
                //gameObject.GetComponent<SpriteRenderer>().enabled = false;
                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                OnDestroyed?.Invoke();
                Destroy(collision.gameObject);
                Destroy(gameObject); 
                //return; 
            }
         else    {
            hitCounter = hitCounter-1;
            }
         }

    }
}
