using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    [Header("Volume Settings")]

    [SerializeField] private Slider _MasterVolumeSlider = null;
    [SerializeField] private Slider _MusicVolumeSlider = null;
    [SerializeField] private Slider _EffectsVolumeSlider = null;
    [SerializeField] private Slider _AmbientVolumeSlider = null;

    [SerializeField] private GameObject ConfirmationPrompt = null;

    [SerializeField] private Button _AudioSettingsDefaultSettingsButton = null;

    SettingsData.AudioSettings cfgAudioSettings = null;

    public void Init()
    {
        //GameObject AudioOptionsDialogueGameObject = GameController.instance.m_dialogueManager.dialogues["AudioOptions"];
        //AudioOptionsDialogue AudioOptionsDialogue = null;

        //AudioOptionsDialogue = AudioOptionsDialogueGameObject.GetComponentInChildren<AudioOptionsDialogue>(true);

        //_MasterVolumeSlider = AudioOptionsDialogue._MasterVolumeSlider;
        //_MusicVolumeSlider = AudioOptionsDialogue._MusicVolumeSlider;
        //_EffectsVolumeSlider = AudioOptionsDialogue._EffectsVolumeSlider;
        //_AmbientVolumeSlider = AudioOptionsDialogue._AmbientVolumeSlider;

        //_AudioSettingsDefaultSettingsButton = AudioOptionsDialogue._ResetButton;

        //InitSliderText(_MasterVolumeSlider);
        //InitSliderText(_MusicVolumeSlider);
        //InitSliderText(_EffectsVolumeSlider);
        //InitSliderText(_AmbientVolumeSlider);

        //cfgAudioSettings = GameController.instance.GetAudioSettings;

        //if (cfgAudioSettings == GameController.instance.GetDefaultAudioSettings)
        //    _AudioSettingsDefaultSettingsButton.interactable = false;

        //ResetAudioSettingsToCfg(cfgAudioSettings);
    }

    public void InitSliderText(Slider slider)
    {
        var textbox = slider.gameObject.GetComponentInChildren<TextListener>();
        if (textbox)
        {
            textbox.Init();
        }
    }

    public void AudioSettingsOnApply()
    {
        StartCoroutine(ConfirmationBox());

        cfgAudioSettings.masterVolume = _MasterVolumeSlider.value;
        cfgAudioSettings.musicVolume = _MusicVolumeSlider.value;
        cfgAudioSettings.effectsVolume = _EffectsVolumeSlider.value;
        cfgAudioSettings.ambientVolume = _AmbientVolumeSlider.value;

        GameController.instance.SaveSettingsToDisk();
    }

    public void ResetAudioSettingsToCfg(SettingsData.AudioSettings audioSettings)
    {
        _MasterVolumeSlider.value   = audioSettings.masterVolume;
        _MusicVolumeSlider.value    = audioSettings.musicVolume;
        _EffectsVolumeSlider.value  = audioSettings.effectsVolume;
        _AmbientVolumeSlider.value  = audioSettings.ambientVolume;
    }

    public IEnumerator ConfirmationBox()
    {
        ConfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        ConfirmationPrompt.SetActive(false);
    }

}
