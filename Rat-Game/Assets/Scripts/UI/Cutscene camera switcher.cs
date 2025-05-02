using UnityEngine;
using System.Collections.Generic;
using Unity.Cinemachine;

public class CutsceneCameraSwitcher : MonoBehaviour
{
    public List<CinemachineCamera> virtualCameras = new List<CinemachineCamera>();
    public float switchInterval = 5f;

    private float timer = 0f;
    private int camIndex = 0;
    private bool switchingDone = false;

    void Start()
    {
        if (virtualCameras.Count > 0)
        {
            SetActiveCamera(0);
        }
    }

    void Update()
    {
        if (switchingDone || virtualCameras.Count == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            camIndex++;

            if (camIndex >= virtualCameras.Count)
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
        for (int i = 0; i < virtualCameras.Count; i++)
        {
            virtualCameras[i].Priority = (i == index) ? 10 : 0;
        }
    }
}
