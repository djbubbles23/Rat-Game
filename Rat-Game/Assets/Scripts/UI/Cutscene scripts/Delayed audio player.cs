using UnityEngine;

public class DelayedAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayInSeconds = 2f;
    [Range(0f, 1f)] public float volume = 1f;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            Invoke(nameof(PlayAudio), delayInSeconds);
        }
    }

    void PlayAudio()
    {
        audioSource.Play();
    }
}
