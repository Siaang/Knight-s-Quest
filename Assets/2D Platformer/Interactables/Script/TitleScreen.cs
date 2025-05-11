using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Called when the Play button is clicked
    public void Play()
    {
        // Load the scene with index 1
        SceneManager.LoadSceneAsync(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
