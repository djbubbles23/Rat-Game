using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class CamSwitcher : MonoBehaviour
{
    public CinemachineCamera[] cameras; // Array of cameras
    public Button nextButton; // Button to go forward
    public Button prevButton; // Button to go backward

    private int currentCamIndex = 0; // Tracks the active camera

    void Start()
    {
        // Set up cameras, only enabling the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].Priority = (i == 0) ? 10 : 0;
        }

        // Assign button click listeners
        nextButton.onClick.AddListener(SwitchToNextCamera);
        prevButton.onClick.AddListener(SwitchToPreviousCamera);

        // Update button states on start
        UpdateButtonStates();
    }

    void SwitchToNextCamera()
    {
        if (currentCamIndex < cameras.Length - 1) // Ensure we don't go out of bounds
        {
            cameras[currentCamIndex].Priority = 0; // Disable current camera
            currentCamIndex++; // Move forward
            cameras[currentCamIndex].Priority = 10; // Enable new active camera
            UpdateButtonStates();
        }
    }

    void SwitchToPreviousCamera()
    {
        if (currentCamIndex > 0) // Ensure we don't go out of bounds
        {
            cameras[currentCamIndex].Priority = 0; // Disable current camera
            currentCamIndex--; // Move backward
            cameras[currentCamIndex].Priority = 10; // Enable new active camera
            UpdateButtonStates();
        }
    }

    void UpdateButtonStates()
    {
        // Disable "Back" button on first camera
        prevButton.interactable = currentCamIndex > 0;

        // Disable "Next" button on last camera
        nextButton.interactable = currentCamIndex < cameras.Length - 1;
    }
}
