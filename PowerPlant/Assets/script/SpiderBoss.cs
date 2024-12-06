using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class SpiderBoss : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public AudioClip _audioClip;  
    public AudioClip _audioWeb;   
    public TMP_Text bossHealthText;
    public TMP_Text bossText;
    private bool isDestroyed = false; 
    private ScoreManager scoreManager;
    public delegate void DestroyedAction();
    public event DestroyedAction OnDestroyed;
    public Animator animator;
    private int hitCounter = 20;
    private int maxHitCounter; 

    public GameObject GameWinPanel;
    public GameObject webPrefab; // Add the web prefab
    public float webSpeed = 5f; // Speed of the web

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Get the target immediately on start
        scoreManager = FindObjectOfType<ScoreManager>();
        
        maxHitCounter = hitCounter;

        if (animator == null)
        {
            Debug.LogError("Animator not assigned in SpiderScript.");
        }

        if (GameWinPanel != null)
        {
           GameWinPanel.SetActive(false); // Ensure GameOverPanel is hidden at the start
        }
        if (bossHealthText != null)
        {
            bossHealthText.gameObject.SetActive(false); // Hide initially
        }

        UpdateBossHealthUI();

        // Start the web shooting coroutine
        StartCoroutine(ShootWeb());
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
        if (collision.gameObject.CompareTag("laser"))
        {
            if (hitCounter == 0)
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
            else
            {
                AudioSource.PlayClipAtPoint(_audioClip, transform.position);
                hitCounter = hitCounter - 1;
                Destroy(collision.gameObject);
                UpdateBossHealthUI();
            }
        }
    }

    private void TriggerGameWin()
    {
        bossHealthText.gameObject.SetActive(false);
        bossText.gameObject.SetActive(false);
        // Show the game over panel
        if (GameWinPanel != null)
        {
            GameWinPanel.SetActive(true);
        }

      
        // Freeze game time
        Time.timeScale = 0f;
    }

    private IEnumerator ShootWeb()
    {
        while (!isDestroyed)
        {
            yield return new WaitForSeconds(4f); // Shoot web every 4 seconds

            if (webPrefab != null && target != null)
            {
                // Instantiate the web
                GameObject web = Instantiate(webPrefab, transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_audioWeb, transform.position);

                // Calculate direction toward the target
                Vector2 direction = (target.position - transform.position).normalized;

                // Apply velocity to the web
                Rigidbody2D webRb = web.GetComponent<Rigidbody2D>();
                if (webRb != null)
                {
                    webRb.velocity = direction * webSpeed;
                    Destroy(web, 2f);
                }

            }
        }
    }
    private void UpdateBossHealthUI()
    {
        if (bossHealthText != null)
        {
            if (!bossHealthText.gameObject.activeSelf)
            {
                bossHealthText.gameObject.SetActive(true); // Show the text when updating
            }

            float percentage = ((float)hitCounter / maxHitCounter) * 100f;
            bossHealthText.text = $"Boss Health: {percentage:F0}%";
        }
    }
}
