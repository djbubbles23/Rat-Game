using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class CamSwitcher : MonoBehaviour
{
    public CinemachineCamera[] cameras; // Array to hold cameras
    public Button nextButton; // Button to go forward
    public Button prevButton; // Button to go backward

    private int currentCamIndex = 0; // Tracks the active camera

    void Start()
    {
        // Ensure only the first camera is active
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == 0) ? 10 : 0;
        }

        // Assign button click listeners
        nextButton.onClick.AddListener(SwitchToNextCamera);
        prevButton.onClick.AddListener(SwitchToPreviousCamera);
    }

    void SwitchToNextCamera()
    {
        cameras[currentCamIndex].Priority = 0; // Disable current camera
        currentCamIndex = (currentCamIndex + 1) % cameras.Length; // Move forward
        cameras[currentCamIndex].Priority = 10; // Enable new active camera
    }

    void SwitchToPreviousCamera()
    {
        cameras[currentCamIndex].Priority = 0; // Disable current camera
        currentCamIndex = (currentCamIndex - 1 + cameras.Length) % cameras.Length; // Move backward
        cameras[currentCamIndex].Priority = 10; // Enable new active camera
    }
}
