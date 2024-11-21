using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public AudioClip _audioClip;    
    
    private bool isDestroyed = false; 
    private ScoreManager scoreManager;
    public delegate void DestroyedAction();
    public event DestroyedAction OnDestroyed;
    public Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Get the target immediately on start
        scoreManager = FindObjectOfType<ScoreManager>();
        
        if (animator == null)
        {
            Debug.LogError("Animator not assigned in SpiderScript.");
        }

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
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            // Ignore collisions with obstacles
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
        if (collision.gameObject.CompareTag("speed"))
        {
            // Ignore collisions with obstacles
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
         if (collision.gameObject.CompareTag("laser"))
         {
            isDestroyed = true;
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Debug.Log("Spider hit!");
            scoreManager?.AddScore(1);
            OnDestroyed?.Invoke();
            Destroy(collision.gameObject);
            Destroy(gameObject); 
            
         }

    }
}
