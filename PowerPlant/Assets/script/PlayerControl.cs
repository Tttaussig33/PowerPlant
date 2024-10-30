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
    private HealthManager healthManager;

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
        
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.velocity = new Vector2(speedX, speedY);

        //laser
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime) 
        {
            FireLaser();
            nextFireTime = Time.time + fireRate; // Set the next time the player can fire
        }
    }
    void FireLaser()
    {
        AudioSource.PlayClipAtPoint(_audioClip, transform.position);
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
                healthManager.TakeDamage(15); // Use the instance of HealthManager to take damage
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
                healthManager.TakeDamage(25); // Use the instance of HealthManager to take damage
                AudioSource.PlayClipAtPoint(_audioClip2, transform.position);
            }
            else
            {
                Debug.LogError("HealthManager not found!");

            }

         }

    }
    public void IncreaseLaserSpeed(float speedBoost, float duration) //powerup method 
   {
       float newFireRate = fireRate - speedBoost; 

       newFireRate = Mathf.Max(newFireRate, 0.1f);


       StartCoroutine(TemporaryLaserSpeedBoost(newFireRate, duration));
   }


   private IEnumerator TemporaryLaserSpeedBoost(float newFireRate, float duration)
   {
       float originalFireRate = fireRate; 
       fireRate = newFireRate; 


       yield return new WaitForSeconds(duration); 


       fireRate = originalFireRate; 
   }

    
}
