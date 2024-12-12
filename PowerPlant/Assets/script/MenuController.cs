using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
   public void LoadRulesScreen()
   {
       SceneManager.LoadScene("ruleScene");
   }


   public void BackToMainMenu()
   {
       SceneManager.LoadScene("TitleScreen"); // Replace with your actual main menu scene name
   }
}
