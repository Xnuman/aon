using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject NoSavedLevelPopUpDialog = null;

    [SerializeField] private GameObject ConfirmationPrompt = null;

    [Header("Volume Settings")]

    [SerializeField] private Slider _MasterVolumeSlider    = null;
    [SerializeField] private Slider _MusicVolumeSlider     = null;
    [SerializeField] private Slider _EffectsVolumeSlider   = null;
    [SerializeField] private Slider _AmbientVolumeSlider   = null;

    SettingsData.AudioSettings cfgAudioSettings = null;

    public void Init()
    {
        InitTextbox(_MasterVolumeSlider);
        InitTextbox(_MusicVolumeSlider);
        InitTextbox(_EffectsVolumeSlider);
        InitTextbox(_AmbientVolumeSlider);

        cfgAudioSettings = GameController.instance.GetAudioSettings;

        ResetAudioSettingsToCfg();
    }

    public void InitTextbox(Slider slider)
    {
        var textbox = slider.gameObject.GetComponentInChildren<TextListener>();
        if (textbox)
        {
            textbox.Init();
        }
    }

    public void StartGameDialogYes()
    {
        GameController.instance.StartNewGame();
    }

    public void ContinueGameDialogYes()
    {
        //if (PlayerPrefs.HasKey("SavedLevel"))
        //{
        //    loadingLevelName = PlayerPrefs.GetString("SavedLevel");
        //    LoadLevel();
        //}
        //else
        //{
        //    if (NoSavedLevelPopUpDialog != null)
        //    {
        //        NoSavedLevelPopUpDialog.SetActive(true);
        //    }
        //}
    }

    public void QuitGameDialogYes()
    {
        Application.Quit();
    }

    public void AudioSettingsOnApply() { 

        StartCoroutine(ConfirmationBox());

        cfgAudioSettings.masterVolume = _MasterVolumeSlider.value;
        cfgAudioSettings.musicVolume = _MusicVolumeSlider.value;
        cfgAudioSettings.effectsVolume = _EffectsVolumeSlider.value;
        cfgAudioSettings.ambientVolume = _AmbientVolumeSlider.value;

        GameController.instance.UpdateAudioSettings(cfgAudioSettings);
        GameController.instance.SaveSettingsToDisk();
    }

    public void ResetAudioSettingsToCfg()
    {
        _MasterVolumeSlider.value = cfgAudioSettings.masterVolume;
        _MusicVolumeSlider.value = cfgAudioSettings.musicVolume;
        _EffectsVolumeSlider.value = cfgAudioSettings.effectsVolume;
        _AmbientVolumeSlider.value = cfgAudioSettings.ambientVolume;
    }

    public IEnumerator ConfirmationBox()
    {
        ConfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        ConfirmationPrompt.SetActive(false);
    }
}
