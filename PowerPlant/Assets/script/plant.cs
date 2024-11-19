using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // For TextMeshPro

public class Plant : MonoBehaviour
{
    public Sprite patch;            // Drag your default sprite here
    public Sprite planting;         // Drag your alternate sprite here
    public Transform player;        // Reference to the player's Transform
    public float changeDistance = 5f; // Max distance for the sprite to change
    public Sprite timeLimitSprite;  // Sprite to switch to after 10 seconds
    public TMP_Text plantText;
    public TMP_Text countdownText;  // Text to display the countdown timer
    public static int plantsNum = 0;
    public AudioClip _audioClip;
    public AudioClip _audioClip2;
    public GameObject GameWinPanel;
    public GameObject SpiderBoss; 

    private SpriteRenderer spriteRenderer;
    private bool spriteChangedByKey = false;

    void Start()
    {
        // Get the SpriteRenderer component from the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite
        spriteRenderer.sprite = patch;

        // Ensure GameWinPanel and SpiderBoss are inactive at the start
        if (GameWinPanel != null)
        {
            GameWinPanel.SetActive(false);
        }
        if (SpiderBoss != null)
        {
            SpiderBoss.SetActive(false);
        }

        // Hide the countdown text at the start
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }
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
                AudioSource.PlayClipAtPoint(_audioClip, transform.position);

                // Start the countdown coroutine for 10 seconds
                StartCoroutine(ChangeSpriteAfterTime(5f));
            }
        }
    }

    // Coroutine to change the sprite after 10 seconds
    IEnumerator ChangeSpriteAfterTime(float seconds)
    {
        float elapsed = 0f;

        // Show the countdown text
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(true);
        }

        while (elapsed < seconds)
        {
            // Check if the player is within the specified distance
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= changeDistance)
            {
                // Increment elapsed time if the player is nearby
                elapsed += Time.deltaTime;

                // Update the countdown text
                if (countdownText != null)
                {
                    countdownText.text = $"Planting: {Mathf.Ceil(seconds - elapsed)}s";
                }
            }
            else
            {
                Debug.Log("Player is too far, countdown paused.");
            }

            // Wait for the next frame
            yield return null;
        }

        // Hide the countdown text
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        // Change to the time limit sprite when the countdown is complete
        spriteRenderer.sprite = timeLimitSprite;
        Debug.Log("Sprite changed to timeLimitSprite after 10 seconds");
        plantsNum += 1;
        plantText.text = "Powerplants: " + plantsNum.ToString("F0");
        AudioSource.PlayClipAtPoint(_audioClip2, transform.position);

        if (plantsNum == 5)
        {
            Debug.Log("spawn boss");
            if (SpiderBoss != null)
            {
                SpiderBoss.SetActive(true);
            }
        }
    }
}
