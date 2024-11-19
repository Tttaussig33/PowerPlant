using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBoss : MonoBehaviour
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
    private int hitCounter = 15;

    public GameObject GameWinPanel;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Get the target immediately on start
        scoreManager = FindObjectOfType<ScoreManager>();
        
        if (animator == null)
        {
            Debug.LogError("Animator not assigned in SpiderScript.");
        }

        if (GameWinPanel != null)
        {
           GameWinPanel.SetActive(false); // Ensure GameOverPanel is hidden at the start
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
            //Debug.Log(rb.velocity.magnitude);
            //Debug.Log("Direction: " + direction + " | Velocity: " + rb.velocity);

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
         if (collision.gameObject.CompareTag("laser")){
            if (hitCounter==0)
            {
                isDestroyed = true;
                AudioSource.PlayClipAtPoint(_audioClip, transform.position);
                Debug.Log("Spider hit!");
                scoreManager?.AddScore(50);
                OnDestroyed?.Invoke();
                Destroy(collision.gameObject);
                Destroy(gameObject); 
                TriggerGameWin();
            }
            else    {
                AudioSource.PlayClipAtPoint(_audioClip, transform.position);
                hitCounter = hitCounter-1;
                Destroy(collision.gameObject);
            }
         }

    }
     void TriggerGameWin()
   {
       // Show the game over panel
       if (GameWinPanel != null)
       {
           GameWinPanel.SetActive(true);
       }
      
       // Freeze game time
       Time.timeScale = 0f;
   }
}
