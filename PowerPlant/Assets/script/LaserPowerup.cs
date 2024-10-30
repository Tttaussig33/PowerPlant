using UnityEngine;


public class LaserPowerup : MonoBehaviour
{
   public float speedBoost = 0.2f; // Amount to decrease fire rate (increase speed)
   public float duration = 5f; // How long the powerup lasts
   public AudioClip _audioClip;


   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
           PlayerControl playerControl = other.GetComponent<PlayerControl>();
           if (playerControl != null)
           {
               playerControl.IncreaseLaserSpeed(speedBoost, duration);
               Destroy(gameObject); // Destroy the powerup after it's collected
               AudioSource.PlayClipAtPoint(_audioClip, transform.position);
           }
       }
   }
}
