using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // For TextMeshPro
using UnityEngine.SceneManagement; // For SceneManager


public class HealthManager : MonoBehaviour
{
   public Image healthBar;
   public TMP_Text healthText; 
   public float healthAmount = 100f;
   public GameObject gameOverPanel; // Reference to the GameOverPanel UI
   public AudioClip _audioClip;
   public Animator animator; 
   private PlayerControl playerControl;
   private AudioSource deathSound;
   public TMP_Text bossHealthText;
    public TMP_Text bossText;


   void Start()
   {
        GameObject player = GameObject.FindWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();

        deathSound = gameObject.AddComponent<AudioSource>();
        deathSound.clip = _audioClip;
       UpdateHealthUI();
       if (gameOverPanel != null)
       {
           gameOverPanel.SetActive(false); // Ensure GameOverPanel is hidden at the start
       }
   }


   void Update()
   {
       if (healthAmount <= 0 && !gameOverPanel.activeSelf) // Check for game over only if panel is not already active
       {
            deathSound.Play();
            playerDeadMethod();
            /*
            AudioSource.PlayClipAtPoint(_audioClip, transform.position, 1.0f);
            Debug.Log("Player health is zero");
            animator.SetTrigger("Die");
            if (playerControl != null)
            {
                playerControl.DisableMovement();
                Debug.Log("playercontrols!=null");
            }

            StartCoroutine(DelayedGameOver());
            */

       }
   }
   private void playerDeadMethod()
   {
        Debug.Log("Player health is zero");
            animator.SetTrigger("Die");
            if (playerControl != null)
            {
                playerControl.DisableMovement();
                Debug.Log("playercontrols!=null");
            }

            StartCoroutine(DelayedGameOver());
   }
   private IEnumerator DelayedGameOver()
{
    yield return new WaitForSeconds(2f); // Wait for 1 second
    TriggerGameOver(); // Then trigger the game-over logic
}


   public void TakeDamage(float damage)
   {
       healthAmount -= damage;
       healthAmount = Mathf.Clamp(healthAmount, 0, 100);
       UpdateHealthUI();
   }


   public void Heal(float healingAmount)
   {
       healthAmount += healingAmount;
       healthAmount = Mathf.Clamp(healthAmount, 0, 100);
       UpdateHealthUI();
   }


   void UpdateHealthUI()
   {
       healthBar.fillAmount = healthAmount / 100f;
       healthText.text = "HP: " + healthAmount.ToString("F0") + "%";
   }


   void TriggerGameOver()
   {
        //AudioSource.PlayClipAtPoint(_audioClip, transform.position);
        //Debug.LogError("game over method");
       // Show the game over panel
       bossHealthText.gameObject.SetActive(false);
       bossText.gameObject.SetActive(false);
       if (gameOverPanel != null)
       {
           gameOverPanel.SetActive(true);
       }
      
       // Freeze game time
       Time.timeScale = 0f;
   }


   public void RestartGame()
   {
       // Reset health
       healthAmount = 100f; // Reset health to initial value
       UpdateHealthUI(); // Update the UI to reflect the initial health
       Plant.plantsNum=0;
       ScoreManager.score=0;


       // Hide GameOverPanel
       if (gameOverPanel != null)
       {
           gameOverPanel.SetActive(false);
       }


       // Reset time scale to ensure the game runs again
       Time.timeScale = 1f;


       // Reload the scene to restart the game
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
