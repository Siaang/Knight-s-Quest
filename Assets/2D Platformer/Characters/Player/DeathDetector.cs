using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();

            Debug.Log("You died");
        }
    }

}
