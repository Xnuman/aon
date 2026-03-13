using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource effects;
    [SerializeField] private AudioSource ambient;

    [SerializeField] private AudioMixer mainAudioMixer;

    AudioSource currentlyPlayingMusic;

    private float convertToLogarithmic(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1.0f);

        return Mathf.Log10(volume) * 20;
    }

    private void SetMixerGroupVolume(string group, float volume)
    {
        if (mainAudioMixer == null)
            return;

        mainAudioMixer.SetFloat(group, convertToLogarithmic(volume));
    }

    public void SetMasterVolume(float volume)
    {
        SetMixerGroupVolume("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        SetMixerGroupVolume("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        SetMixerGroupVolume("EffectsVolume", volume);
    }

    public void SetAmbientVolume(float volume)
    {
        SetMixerGroupVolume("AmbientVolume", volume);
    }

    public void ApplyAudioSettings(SettingsData.AudioSettings settings)
    {
        if (settings == null)
            return;

        SetMasterVolume(settings.masterVolume);
        SetMusicVolume(settings.musicVolume);
        SetAmbientVolume(settings.ambientVolume);
        SetEffectsVolume(settings.effectsVolume);
    }
    public void Init()
    {
        ApplyAudioSettings(GameController.instance.GetAudioSettings);
    }
}