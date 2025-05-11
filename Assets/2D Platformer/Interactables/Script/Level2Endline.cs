using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Endline : MonoBehaviour
{
    public GameObject endscreenPanel; // The panel to show at the end
    public float delay = 0.5f; // Delay before showing the end screen
    private PlayerMovement playerMovement; // Reference to the PlayerMovement script

    private void Start()
    {
        // Find the player object and get the PlayerMovement component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowEndScreenAfterDelay());
        }
    }

    private IEnumerator ShowEndScreenAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        endscreenPanel.SetActive(true); // Show the end screen panel
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player movement
        }
        Time.timeScale = 0f; // Pause the game
    }

    public void Exit()
    {
        Time.timeScale = 1f; // Resume the game
        if (playerMovement != null)
        {
            playerMovement.enabled = true; // Re-enable player movement if necessary
        }
        SceneManager.LoadScene("TitleScreen"); // Load the title screen
    }
}
