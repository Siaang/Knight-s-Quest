using UnityEngine;
using UnityEngine.UI;

public class AudioMuteManager : MonoBehaviour
{
    public Image buttonImage; // Reference to the button's image component
    public Sprite muteSprite; // Sprite for mute state
    public Sprite unmuteSprite; // Sprite for unmute state
    private bool isMuted = false; // Tracks mute state

    // This method is called by the button's OnClick() event in the Inspector
    public void ToggleMute()
    {
        // Toggle the mute state
        isMuted = !isMuted;
        AudioListener.pause = isMuted; // Mutes/unmutes all audio in the game
        UpdateButtonSprite();
    }

    private void UpdateButtonSprite()
    {
        // Change the button sprite based on the mute state
        buttonImage.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    // Optional: Initialize to the correct state on start
    void Start()
    {
        AudioListener.pause = isMuted;
        UpdateButtonSprite();
    }
}
