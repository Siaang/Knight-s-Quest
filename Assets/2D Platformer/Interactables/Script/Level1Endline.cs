using UnityEngine;
using UnityEngine.SceneManagement;

public class Endline : MonoBehaviour
{
    public float delay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger!");
            Invoke("LoadNextScene", delay);
        }
    }

    void LoadNextScene()
    {
        Debug.Log("Loading next scene...");
        SceneManager.LoadSceneAsync(2);  // You can use the scene name or index here
    }
}
