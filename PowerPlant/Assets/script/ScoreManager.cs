using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
   public TMP_Text scoreText;  // Reference to the TextMeshPro UI element
   public static int score = 0;      // Player's score


   void Start()
   {
       UpdateScoreUI();
   }


   public void AddScore(int points)
   {
       score += points;
       UpdateScoreUI();
   }


   private void UpdateScoreUI()
   {
       scoreText.text = "Score: " + score;
   }
}
