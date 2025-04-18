using UnityEngine;

public class MusicLayerTrigger : MonoBehaviour
{
    public MusicController musicController;
    public int trackIndex = 0;
    public bool activateOnEnter = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activateOnEnter)
                musicController.ActivateTrack(trackIndex);
            else
                musicController.DeactivateTrack(trackIndex);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activateOnEnter)
                musicController.DeactivateTrack(trackIndex);
            else
                musicController.ActivateTrack(trackIndex);
        }
    }
}
