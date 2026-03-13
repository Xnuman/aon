using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsDialogue : MonoBehaviour
{
    public Slider _MasterVolumeSlider = null;
    public Slider _MusicVolumeSlider = null;
    public Slider _EffectsVolumeSlider = null;
    public Slider _AmbientVolumeSlider = null;

    public Button _Reset = null;
    public Button _Apply = null;
    public Button _Back = null;

    SettingsData.AudioSettings _audioSettings = null;

    private DialogueManager _dialogueManager = null;
    private Canvas _canvas = null;

    public void Update()
    {
        if( _Apply != null )
        {
            _Apply.interactable = HasChanges();
        }
    }
    public void Init(Canvas canvas)
    {
        _dialogueManager = GameController.instance.m_dialogueManager;
        _canvas = canvas;
        _audioSettings = GameController.instance.GetAudioSettings;

        InitSliderText(_MasterVolumeSlider);
        InitSliderText(_MusicVolumeSlider);
        InitSliderText(_EffectsVolumeSlider);
        InitSliderText(_AmbientVolumeSlider);

        _Back.onClick.AddListener(() =>
        {
            SyncUIComponents();
            GameController.instance.m_audioManager.ApplyAudioSettings(_audioSettings);
            _dialogueManager.PopDialogue();
        });
        _Apply.onClick.AddListener(() =>
        {
            _audioSettings.masterVolume     = _MasterVolumeSlider.value;
            _audioSettings.musicVolume      = _MusicVolumeSlider.value;
            _audioSettings.effectsVolume    = _EffectsVolumeSlider.value;
            _audioSettings.ambientVolume    = _AmbientVolumeSlider.value;

            GameController.instance.m_audioManager.ApplyAudioSettings(_audioSettings);

            GameController.instance.SaveSettingsToDisk();
        });
        _MasterVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            GameController.instance.m_audioManager.SetMasterVolume(value);
        });
        _MusicVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            GameController.instance.m_audioManager.SetMusicVolume(value);
        });
        _EffectsVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            GameController.instance.m_audioManager.SetEffectsVolume(value);
        });
        _AmbientVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            GameController.instance.m_audioManager.SetAmbientVolume(value);
        });

        if (_audioSettings == GameController.instance.GetDefaultAudioSettings)
        {
            _Reset.interactable = false;
        }

        SyncUIComponents();
    }

    public void InitSliderText(Slider slider)
    {
        var textbox = slider.gameObject.GetComponentInChildren<TextListener>();
        if (textbox)
        {
            textbox.Init();
        }
    }

    public void SyncUIComponents()
    {
        _MasterVolumeSlider.value = _audioSettings.masterVolume;
        _MusicVolumeSlider.value = _audioSettings.musicVolume;
        _EffectsVolumeSlider.value = _audioSettings.effectsVolume;
        _AmbientVolumeSlider.value = _audioSettings.ambientVolume;
    }

    public bool HasChanges()
    {
        return !(
        _MasterVolumeSlider.value == _audioSettings.masterVolume &&
        _MusicVolumeSlider.value == _audioSettings.musicVolume &&
        _EffectsVolumeSlider.value == _audioSettings.effectsVolume &&
        _AmbientVolumeSlider.value == _audioSettings.ambientVolume);
    }
}
