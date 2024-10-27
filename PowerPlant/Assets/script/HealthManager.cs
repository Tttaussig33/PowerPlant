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


   void Start()
   {
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
           TriggerGameOver();
       }


       // Test damage and healing inputs
       if (Input.GetKeyDown(KeyCode.Return))
       {
           TakeDamage(20);
       }


       if (Input.GetKeyDown(KeyCode.Space))
       {
           Heal(5);
       }
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
       // Show the game over panel
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
