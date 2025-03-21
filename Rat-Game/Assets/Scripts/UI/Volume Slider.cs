using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Slider VolumeSlider;
    void Start()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
            LoadVolume();
        else
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
        }
    }

    public void SetVolume()
    {

        AudioListener.volume = VolumeSlider.value;
        SaveVolume();
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", VolumeSlider.value);
    }

    public void LoadVolume()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
}
