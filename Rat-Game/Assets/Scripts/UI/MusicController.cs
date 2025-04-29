using UnityEngine;
using static Unity.VisualScripting.Member;

public class MusicController : MonoBehaviour
{
    [Header("Audio Layers")]
    public AudioClip[] Tracks;
    [Range(0f, 1f)] public float maxVolume = 1f;

    [Header("Fade Settings")]
    public float fadeSpeed = 1f; // Units per second

    private AudioSource[] audioSources;
    private float[] targetVolumes;
    private float[] currentVolumes;

    void Start()
    {
        audioSources = new AudioSource[Tracks.Length];
        targetVolumes = new float[Tracks.Length];
        currentVolumes = new float[Tracks.Length];

        for (int i = 0; i < Tracks.Length; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = Tracks[i];
            source.loop = true;
            source.playOnAwake = false;
            source.volume = 0f;
            source.spatialBlend = 0f;

            audioSources[i] = source;
            currentVolumes[i] = 0f;
            targetVolumes[i] = 0f;

            source.Play();
        }
    }

    void Update()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            // Smoothly approach target volume
            currentVolumes[i] = Mathf.MoveTowards(currentVolumes[i], targetVolumes[i], fadeSpeed * Time.deltaTime);
            audioSources[i].volume = currentVolumes[i];
        }
    }

    public void ActivateTrack(int index)
    {
        if (index >= 0 && index < targetVolumes.Length)
            targetVolumes[index] = maxVolume;
    }

    public void DeactivateTrack(int index)
    {
        if (index >= 0 && index < targetVolumes.Length)
            targetVolumes[index] = 0f;
    }
}
