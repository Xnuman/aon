using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource effects;
    [SerializeField] private AudioSource ambient;

    private float masterVolume;
    private float musicVolume;
    private float effectVolume;
    private float ambientVolume;

    private SettingsData.AudioSettings cfgSettings = null;

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;

        SetMusicVolume(musicVolume);
        SetEffectsVolume(effectVolume);
        SetAmbientVolume(ambientVolume);
    }


    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        SetVolume(music, musicVolume);
    }

    public void SetEffectsVolume(float volume)
    {
        effectVolume = volume;
        SetVolume(effects, volume);
    }

    public void SetAmbientVolume(float volume)
    {
        ambientVolume = volume;
        SetVolume(ambient, volume);
    }

    public void SetVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = masterVolume * volume;
    }

    public void UpdateCfgSettings()
    {
        cfgSettings = GameController.instance.GetAudioSettings;
    }

    public void ResetToCfgSettings()
    {
        masterVolume = cfgSettings.masterVolume;
        SetMusicVolume(cfgSettings.musicVolume);
        SetEffectsVolume(cfgSettings.effectsVolume);
        SetAmbientVolume(cfgSettings.ambientVolume);
    }

    public void Init()
    {
        UpdateCfgSettings();
        ResetToCfgSettings();
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
