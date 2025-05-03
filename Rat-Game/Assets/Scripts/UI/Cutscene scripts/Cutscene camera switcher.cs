using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CutsceneCameraSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class CameraDelay
    {
        public CinemachineCamera camera;
        public float HoldFor = 5f;
    }

    public List<CameraDelay> cameraSequence = new List<CameraDelay>();

    private float timer = 0f;
    private int camIndex = 0;
    private bool switchingDone = false;

    void Start()
    {
        if (cameraSequence.Count > 0 && cameraSequence[0].camera != null)
        {
            SetActiveCamera(0);
        }
    }

    void Update()
    {
        if (switchingDone || cameraSequence.Count == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= cameraSequence[camIndex].HoldFor)
        {
            camIndex++;

            if (camIndex >= cameraSequence.Count)
            {
                switchingDone = true;
                return;
            }

            SetActiveCamera(camIndex);
            timer = 0f;
        }
    }

    void SetActiveCamera(int index)
    {
        for (int i = 0; i < cameraSequence.Count; i++)
        {
            if (cameraSequence[i].camera != null)
            {
                cameraSequence[i].camera.Priority = (i == index) ? 10 : 0;
            }
        }
    }
}
