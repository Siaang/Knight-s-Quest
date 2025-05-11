using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public RectTransform[] layers; // Array of layers (single sprites)
    public float[] speedFactors;  // Scrolling speed for each layer
    public Vector2 direction = new Vector2(-1, 0); // Scrolling direction (-1, 0 for leftward)

    private float[] layerWidths; // Full widths of each layer

    void Start()
    {
        // Calculate the full width of each layer
        layerWidths = new float[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            layerWidths[i] = layers[i].rect.width;
        }
    }

    void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            // Move the layer based on its speed
            Vector2 movement = direction * speedFactors[i % speedFactors.Length] * Time.deltaTime * 100;
            layers[i].anchoredPosition += movement;

            // Check if the layer has moved completely out of frame (halfway point)
            if (layers[i].anchoredPosition.x <= -layerWidths[i] / 2) // For horizontal scrolling
            {
                // Reset the position by moving the sprite forward by its full width
                layers[i].anchoredPosition += new Vector2(layerWidths[i], 0);
            }
        }
    }
}
