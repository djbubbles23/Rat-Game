using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Setup")]
    public Collider Area;                     // The zone area
    public GameObject Player;                 // Reference to the player
    public AudioClip[] Tracks;                // Audio tracks for this zone

    [Header("Fading Settings")]
    public float maxVolume = 1f;              // Max volume when close
    public float fadeDistance = 10f;          // Distance where fade begins
    public float fadeSpeed = 2f;              // How fast the volume changes

    private AudioSource[] audioSources;
    private float[] currentVolumes;

    void Start()
    {
        audioSources = new AudioSource[Tracks.Length];
        currentVolumes = new float[Tracks.Length];

        for (int i = 0; i < Tracks.Length; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = Tracks[i];
            source.loop = true;
            source.playOnAwake = false;
            source.volume = 0f;
            source.spatialBlend = 0f; // 2D sound
            audioSources[i] = source;

            source.Play(); // Start playing silently so UnPause works
            source.Pause();
        }
    }

    void Update()
    {
        Vector3 closestPoint = Area.ClosestPoint(Player.transform.position);
        float distance = Vector3.Distance(Player.transform.position, closestPoint);
        float targetVolume = Mathf.Clamp01(1 - (distance / fadeDistance)) * maxVolume;

        for (int i = 0; i < audioSources.Length; i++)
        {
            AudioSource source = audioSources[i];
            float newVolume = Mathf.MoveTowards(currentVolumes[i], targetVolume, fadeSpeed * Time.deltaTime);

            if (newVolume > 0f)
            {
                if (!source.isPlaying)
                    source.UnPause(); // Resume from where it left off
            }
            else
            {
                if (source.isPlaying)
                    source.Pause(); // Pause without resetting playback time
            }

            source.volume = newVolume;
            currentVolumes[i] = newVolume;
        }
    }
}
