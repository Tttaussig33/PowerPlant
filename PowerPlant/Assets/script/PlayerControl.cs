using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Camera mainCam;
    public float movSpeed;
    public GameObject laserPrefab;  
    private GameObject currentLaser;
    public float laserSpeed = 10f;
    public float fireRate = 0.5f;  
    private float nextFireTime = 0f;
    public AudioClip _audioClip;
    public AudioClip _audioClip2;
    public AudioClip _audioClip3;
    private HealthManager healthManager;
    public Animator animator;
    private bool canMove = true;

    float speedX, speedY;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        healthManager = FindObjectOfType<HealthManager>();

    }

    void Update()
    {
        
       if (!canMove) return; // Prevent movement if canMove is false

        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        
        rb.velocity = Vector2.ClampMagnitude(new Vector2(speedX, speedY), movSpeed);
        
        bool isMoving = speedX != 0 || speedY != 0;
        animator.SetBool("isMoving", isMoving);

        // Laser firing
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime) 
        {
            FireLaser();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void DisableMovement()
    {
        /*
        canMove = false;
        rb.velocity = Vector2.zero; // Stop movement immediately
        */
        movSpeed = 0f;
        rb.velocity = Vector2.zero;
        animator.SetBool("isMoving", false); // Ensure idle animation
        
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    private void FixedUpdate(){
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    void FireLaser()
    {
        float volume = 0.6f;
        AudioSource.PlayClipAtPoint(_audioClip, transform.position, volume);
        //finds mouse position
        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

        Vector2 direction = (mousePosition - transform.position).normalized;

        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        laserRb.AddForce(direction * laserSpeed, ForceMode2D.Impulse); //addforce
        //laserRb.velocity = direction * laserSpeed;

        Destroy(laser, 3f);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spider"))
         {
            Debug.Log("Player hit!");
            if (healthManager != null)
            {
                healthManager.TakeDamage(10); // Use the instance of HealthManager to take damage
                AudioSource.PlayClipAtPoint(_audioClip2, transform.position);

            }
            else
            {
                Debug.LogError("HealthManager not found!");

            }

         }
        if (collision.gameObject.CompareTag("Beetle"))
         {
            Debug.Log("Player hit by beetle!");
            if (healthManager != null)
            {
                healthManager.TakeDamage(15); // Use the instance of HealthManager to take damage
                AudioSource.PlayClipAtPoint(_audioClip2, transform.position);
            }
            else
            {
                Debug.LogError("HealthManager not found!");

            }

         }
         if (collision.gameObject.CompareTag("spiderBoss"))
         {
            Debug.Log("Player hit by boss!");
            if (healthManager != null)
            {
                healthManager.TakeDamage(45); // Use the instance of HealthManager to take damage
                AudioSource.PlayClipAtPoint(_audioClip2, transform.position);
            }
            else
            {
                Debug.LogError("HealthManager not found!");

            }

         }
         if (collision.gameObject.CompareTag("speed"))
         {
            Debug.Log("Player hit speed boost!");
            IncreaseSpeed(1.88f, 3f);
         }
         if (collision.gameObject.CompareTag("web"))
         {
            animator.SetBool("isFrozen", true); 
            healthManager.TakeDamage(3);
            StartCoroutine(FreezePlayer(2f));
            AudioSource.PlayClipAtPoint(_audioClip3, transform.position);
         }

    }

    private IEnumerator FreezePlayer(float freezeDuration)
    {
        Debug.Log("Player frozen!");
        // Store original movement speed
        float originalSpeed = movSpeed;

        // Stop movement
        movSpeed = 0f;
        rb.velocity = Vector2.zero; // Ensure the player doesn't slide

        // Disable animations if needed
        animator.SetBool("isMoving", false);

        // Wait for freeze duration
        yield return new WaitForSeconds(freezeDuration);

        // Restore original movement speed
        movSpeed = originalSpeed;
        Debug.Log("Player unfrozen!");
        animator.SetBool("isFrozen", false); 
    }

    public void IncreaseLaserSpeed(float speedBoost, float duration) //laser power up  
   {
       float newFireRate = fireRate - speedBoost; 

       newFireRate = Mathf.Max(newFireRate, 0.1f);

       StartCoroutine(TemporaryLaserSpeedBoost(newFireRate, duration));
   }


   private IEnumerator TemporaryLaserSpeedBoost(float newFireRate, float duration)
   {
        animator.SetBool("WeaponBoosted", true); 
       float originalFireRate = fireRate; 
       fireRate = newFireRate; 


       yield return new WaitForSeconds(duration); 


       fireRate = originalFireRate; 
       animator.SetBool("WeaponBoosted", false);
   }

    private void IncreaseSpeed(float speedMultiplier, float duration) //speed power up
    {
        StartCoroutine(TemporarySpeedBoost(speedMultiplier, duration));
    }

    private IEnumerator TemporarySpeedBoost(float speedMultiplier, float duration)
    {
        float originalSpeed = movSpeed; // Store the original speed
        movSpeed *= speedMultiplier;   // Increase the speed

        yield return new WaitForSeconds(duration); // Wait for the duration

        movSpeed = originalSpeed; // Reset to the original speed
    }
}
