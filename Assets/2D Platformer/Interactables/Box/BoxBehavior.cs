using UnityEngine;

public class PushableBox : MonoBehaviour
{
    [SerializeField] private GameObject skeleton; // Reference to the skeleton GameObject
    [SerializeField] private GameObject initialPositionObject; // Reference to the initial position GameObject

    private Vector3 initialPosition;

    private void Start()
    {
        // Get the initial position from the assigned GameObject
        if (initialPositionObject != null)
        {
            initialPosition = initialPositionObject.transform.position;
        }
        else
        {
            Debug.LogWarning("Initial position object is not assigned!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the specified skeleton
        if (collision.gameObject == skeleton)
        {
            // Destroy the box
            gameObject.SetActive(false);

            // Respawn the box after a short delay
            Invoke(nameof(RespawnBox), 1.0f); // Adjust delay as needed
        }
    }

    private void RespawnBox()
    {
        // Reset position
        transform.position = initialPosition;

        // Reactivate the box
        gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        // Draw a sphere at the initial position for visualization in the Scene view
        if (initialPositionObject != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(initialPositionObject.transform.position, 0.2f);
        }
    }
}
