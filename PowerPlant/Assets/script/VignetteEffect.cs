using UnityEngine;
using UnityEngine.UI;

public class VignetteEffect : MonoBehaviour
{
    public Image vignetteImage; // Assign your vignette Image in the Inspector
    public float fadeSpeed = 2f; // Speed of fading
    private float targetAlpha = 0f; // Target alpha value for fading
    
    void Update()
    {
        if (vignetteImage)
        {
            // Gradually change the alpha of the vignette
            Color currentColor = vignetteImage.color;
            float newAlpha = Mathf.Lerp(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            vignetteImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
    }

    // Call this method to fade in the vignette
    public void FadeIn()
    {
        targetAlpha = 1f; // Full vignette effect
    }

    // Call this method to fade out the vignette
    public void FadeOut()
    {
        targetAlpha = 0f; // No vignette effect
    }
}
