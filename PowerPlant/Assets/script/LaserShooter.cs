using UnityEngine;


public class LaserShooter : MonoBehaviour
{
   [Tooltip("The laser prefab to be instantiated.")]
   public GameObject laserPrefab;


   [Tooltip("The speed at which the laser moves.")]
   public float laserSpeed = 10f;


   private GameObject currentLaser;


   void Update()
   {
       // Detect left mouse button press
       if (Input.GetMouseButtonDown(0))
       {
           ShootLaser();
       }
   }


   void ShootLaser()
{
   // Check if there is already an active laser
   if (currentLaser != null)
   {
       Destroy(currentLaser);
   }


   // Instantiate the laser at the player's position with a custom rotation (horizontal shooting)
   Quaternion customRotation = Quaternion.Euler(0, 0, -90);  // Adjust to your desired angle
   currentLaser = Instantiate(laserPrefab, transform.position, customRotation);


   // Parent the laser to the player so it moves with the player
   currentLaser.transform.parent = this.transform;


   // Get the Rigidbody2D component attached to the laser
   Rigidbody2D laserRb = currentLaser.GetComponent<Rigidbody2D>();


   // Apply velocity in the right direction (horizontal)
   laserRb.velocity = Vector2.right * laserSpeed;  // Use Vector2.left for leftward shooting


   // Destroy the laser after 3 seconds to prevent clones from piling up
   Destroy(currentLaser, 3f);
}


}
