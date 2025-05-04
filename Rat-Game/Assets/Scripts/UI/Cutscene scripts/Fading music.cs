using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FadingMusic : MonoBehaviour
{
    [Tooltip("Time in seconds to start playback from.")]
    public float startAtTime = 0f;

    [Tooltip("Time in seconds to wait before starting the fade.")]
    public float delayBeforeFade = 5f;

    [Tooltip("Duration of the fade to 0 volume.")]
    public float fadeDuration = 2f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.time = Mathf.Clamp(startAtTime, 0f, audioSource.clip.length);
        audioSource.Play();

        StartCoroutine(DelayedFade());
    }

    private IEnumerator DelayedFade()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float startVolume = audioSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
