using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserFire : MonoBehaviour
{
    public GameObject laserPrefab; // The laser prefab to instantiate
    public Transform FirePoint; // The point where the laser is fired from
    public float fireForce = 20f; // The force with which the laser is fired

    // Fire the laser in a specific direction
    public void Fire(Vector2 fireDirection)
    {
        GameObject laser = Instantiate(laserPrefab, FirePoint.position, FirePoint.rotation); // Create the laser object
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.AddForce(fireDirection * fireForce, ForceMode2D.Impulse); // Apply force in the calculated direction
    }
}
