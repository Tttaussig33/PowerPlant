using UnityEngine;


public class LaserPowerup : MonoBehaviour
{
   public float speedBoost = 0.2f; // Amount to decrease fire rate (increase speed)
   public float duration = 5f; // How long the powerup lasts
   public AudioClip _audioClip;
   public Animator animator;


   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
        PlayerControl playerControl = other.GetComponent<PlayerControl>();
        if (playerControl != null)
        {
            animator.SetTrigger("brake");
            playerControl.IncreaseLaserSpeed(speedBoost, duration);
            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            Destroy(gameObject, 1f); // Destroy the powerup after 2 seconds
        }
       }
   }
}
