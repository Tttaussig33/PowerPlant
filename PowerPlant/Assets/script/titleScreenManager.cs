using UnityEngine;
using UnityEngine.SceneManagement;


public class titleScreenManager : MonoBehaviour
{
   public void StartGame()
   {
       // Load the main game scene (replace "MainScene" with your main scene's name)
       SceneManager.LoadScene("SampleScene");
   }


   public void QuitGame()
   {
       // Quit the application
       Application.Quit();
       Debug.Log("Game is exiting"); // Won't show in build but useful in editor
   }
}
