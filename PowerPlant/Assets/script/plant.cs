using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Plant : MonoBehaviour
{
    public Sprite patch;            // Drag your default sprite here
    public Sprite planting;         // Drag your alternate sprite here
    public Transform player;        // Reference to the player's Transform
    public float changeDistance = 3f; // Max distance for the sprite to change
    public Sprite timeLimitSprite;  // Sprite to switch to after 10 seconds
    public TMP_Text plantText;
    public int plantsNum = 0;

    private SpriteRenderer spriteRenderer;
    private bool spriteChangedByKey = false;

    void Start()
    {
        // Get the SpriteRenderer component from the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite
        spriteRenderer.sprite = patch;
    }

    void Update()
    {
        // Calculate the distance between the player and this GameObject
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // If the player is within the specified distance, allow the sprite to change by key press
        if (distanceToPlayer <= changeDistance && !spriteChangedByKey)
        {
            // Check for key press (P key)
            if (Input.GetKeyDown(KeyCode.P))
            {
                // Change to the alternate sprite
                spriteRenderer.sprite = planting;
                spriteChangedByKey = true; // Prevent further key-based changes

                // Start the countdown coroutine for 10 seconds
                StartCoroutine(ChangeSpriteAfterTime(10f));
            }
        }
    }

    // Coroutine to change the sprite after 10 seconds
    IEnumerator ChangeSpriteAfterTime(float seconds)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(seconds);

        // Change to the time limit sprite
        spriteRenderer.sprite = timeLimitSprite;
        Debug.Log("Sprite changed to timeLimitSprite after 10 seconds");
        plantsNum = plantsNum+1;
        plantText.text = "Powerplants: " + plantsNum.ToString("F0");
    }
}
