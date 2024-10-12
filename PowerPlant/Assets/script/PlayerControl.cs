using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Camera mainCam;
    public float movSpeed;
    public GameObject laserPrefab;  
    //public Transform firePoint; 
    private GameObject currentLaser;
    public float laserSpeed = 10f;
    public float fireRate = 0.5f;  
    private float nextFireTime = 0f;
    public AudioClip _audioClip;

    float speedX, speedY;
    Rigidbody2D rb;
    //Vector2 moveDirection;
    //Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
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
        laserRb.velocity = direction * laserSpeed;

        Destroy(laser, 3f);

        /*
        Quaternion customRotation = Quaternion.Euler(0, 0, 0); //start point
        currentLaser = Instantiate(laserPrefab, transform.position, customRotation); //creates laser
        currentLaser.transform.parent = this.transform;
        Rigidbody2D laserRb = currentLaser.GetComponent<Rigidbody2D>();
        laserRb.velocity = Vector2.right * laserSpeed; 
        Destroy(currentLaser, 3f);
        */
    }
    
}
