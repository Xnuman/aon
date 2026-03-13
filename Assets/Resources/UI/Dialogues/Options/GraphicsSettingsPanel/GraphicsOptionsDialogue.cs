using UnityEngine;
using UnityEngine.UI;

public class GraphicsOptionsDialogue : MonoBehaviour
{
    public Button _Back = null;
    public Button _Apply = null;
    public Toggle _Fullscreen = null;

    SettingsData.GraphicsSettings _graphicsSettings;

    private DialogueManager _dialogueManager;

    public void Update()
    {
        if( _Apply != null )
        {
            _Apply.interactable = HasChanges();
        }
    }

    private bool HasChanges()
    {
        return !(_graphicsSettings.isFullscreen == _Fullscreen.isOn);
    }

    public void Init(Canvas canvas)
    {
        _dialogueManager    = GameController.instance.m_dialogueManager;
        _graphicsSettings   = GameController.instance.GetGraphicsSettings;

        if (_Fullscreen != null) 
        {
            _Fullscreen.isOn = _graphicsSettings.isFullscreen;
            _Fullscreen.onValueChanged.AddListener((bool value) =>
            {
                GameController.instance.SetFullscreen(value);
            });
        }
        
        if(_Back != null)
        {
            _Back.onClick.AddListener(() =>
            {
                _dialogueManager.PopDialogue();
            });
        }

        if(_Apply != null)
        {
            _Apply.onClick.AddListener( () =>
            {
                _graphicsSettings.isFullscreen = _Fullscreen.isOn;

                GameController.instance.SaveSettingsToDisk();
            });
        }
    }
}
