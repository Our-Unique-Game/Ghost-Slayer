using UnityEngine;
using UnityEngine.Video; // For VideoPlayer
using UnityEngine.UI;    // For RawImage

public class AutoPlayVideo : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject videoPanel;       // Black background panel
    [SerializeField] private RawImage videoDisplay;       // UI RawImage to display video
    [SerializeField] private VideoPlayer videoPlayer;     // Video Player component
    [SerializeField] private RenderTexture videoTexture;  // Render texture for video

    private bool videoPlaying = true; // Track if the video is playing

    private void Start()
    {
        // Activate the panel and setup display
        videoPanel.SetActive(true);                           // Enable the black background
        videoPlayer.targetTexture = videoTexture;             // Assign the render texture
        videoDisplay.texture = videoTexture;                  // Display it on the RawImage
        videoDisplay.rectTransform.sizeDelta = new Vector2(1920, 1080); // Force size to 1920x1080

        // Play the video
        videoPlayer.Play();
    }

    private void Update()
    {
        // Exit the video if Spacebar is pressed
        if (videoPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            CloseVideo();
        }

        // Close the video automatically when it ends
        if (videoPlaying && !videoPlayer.isPlaying)
        {
            CloseVideo();
        }
    }

    private void CloseVideo()
    {
        // Stop the video and hide the panel
        videoPlayer.Stop();             // Stop playback
        videoPanel.SetActive(false);    // Hide panel
        videoPlaying = false;           // Mark video as stopped
    }
}
