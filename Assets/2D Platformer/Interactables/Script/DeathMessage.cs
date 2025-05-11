using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{

    public GameObject deathScreenPanel;

    public void Retry()
    {
        // Deactivate the death screen and reset the game
        Time.timeScale = 1f; // Resumes the game
        deathScreenPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Time.timeScale = 1f; // Ensures the game is resumed when going to the title screen
        SceneManager.LoadScene("TitleScreen");
    }
}
