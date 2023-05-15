using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Text musicVolumeInPercent;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Text sfxVolumeInPercent;

    private Sound _sound;
    
    [Inject]
    private void Init(Sound sound)
    {
        _sound = sound;
    }

    private void Awake()
    {
        var musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        musicVolumeSlider.value = musicVolume;
        musicVolumeInPercent.text = Mathf.RoundToInt(musicVolume * 100) + "%";

        var sfxVolume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        sfxVolumeSlider.value = sfxVolume;
        sfxVolumeInPercent.text = Mathf.RoundToInt(sfxVolume * 100) + "%";
    }

    private void OnEnable()
    {
        musicVolumeSlider.onValueChanged.AddListener(_sound.SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(_sound.SetSfxVolume);
        
        musicVolumeSlider.onValueChanged.AddListener(DisplayMusicVolumeInPercent);
        sfxVolumeSlider.onValueChanged.AddListener(DisplaySfxVolumeInPercent);
    }

    private void OnDisable()
    {
        musicVolumeSlider.onValueChanged.RemoveListener(_sound.SetMusicVolume);
        sfxVolumeSlider.onValueChanged.RemoveListener(_sound.SetSfxVolume);
        
        musicVolumeSlider.onValueChanged.RemoveListener(DisplayMusicVolumeInPercent);
        sfxVolumeSlider.onValueChanged.RemoveListener(DisplaySfxVolumeInPercent);
    }

    private void DisplayMusicVolumeInPercent(float volume)
    {
        musicVolumeInPercent.text = Mathf.RoundToInt(volume * 100) + "%";
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    
    private void DisplaySfxVolumeInPercent(float volume)
    {
        sfxVolumeInPercent.text = Mathf.RoundToInt(volume * 100) + "%";
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }
}
