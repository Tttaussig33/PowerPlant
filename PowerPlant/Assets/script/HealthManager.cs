using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Add this line for TextMeshPro


public class HealthManager : MonoBehaviour
{
  public Image healthBar; 
  public TMP_Text healthText;  // Use TMP_Text instead of Text if using TextMeshPro
  public float healthAmount = 100f;


  void Start()
  {
      UpdateHealthUI();
  }


  void Update()
  {
      if (healthAmount <= 0)
      {
          Application.LoadLevel(Application.loadedLevel);
      }


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
      healthText.text = "HP: " + healthAmount.ToString("F0") + "%";  // Update the TMP text
  }
}
