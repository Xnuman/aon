using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuPanel : MonoBehaviour
{
    public Button _Graphics;
    public Button _Audio;
    public Button _Controls;
    public Button _Back;

    private DialogueManager _dialogueManager = null;
    private Canvas _canvas = null;

    public void Init(Canvas canvas)
    {
        _dialogueManager = GameController.instance.m_dialogueManager;
        _canvas = canvas;

        _Graphics.onClick.AddListener(() =>
        {
            GameObject dialogue = OnClickCreateDialogue("GraphicsOptions");
            GraphicsOptionsDialogue graphicsSettingsDialogue;

            if( dialogue.TryGetComponent<GraphicsOptionsDialogue>(out graphicsSettingsDialogue))
            {
                graphicsSettingsDialogue.Init(canvas);
            }
            else
            {
                Debug.Log("Type of GraphicsOptionsDialogue is not the same as dialogue in dialogueManager named GraphicsOptions");
            }
        });

        _Audio.onClick.AddListener(() =>
        {
            GameObject dialogue = OnClickCreateDialogue("AudioOptions");
            AudioOptionsDialogue audioOptionsDialogue;
            
            if( dialogue.TryGetComponent<AudioOptionsDialogue>(out audioOptionsDialogue) )
            {
                audioOptionsDialogue.Init(_canvas);
            }
            else
            {
                Debug.Log("Type of AudioOptionsDialogue is not the same as dialogue in dialogueManager named AudioOptions");
            }
        });

        _Controls.onClick.AddListener(() =>
        {
            OnClickCreateDialogue("ControlsOptions");
        });

        _Back.onClick.AddListener(() =>
        {
            _dialogueManager.PopDialogue();
        });
    }

    private GameObject OnClickCreateDialogue(string dialogueName)
    {
        if (_dialogueManager == null)
        {
            Debug.Log("Trying to create dialogue " + dialogueName + ", but dialogueManager is null");
            return null;
        }

        GameObject newDialogue = _dialogueManager.CreateDialogue(dialogueName, _canvas);

        if (newDialogue == null)
        {
            Debug.Log("Trying to create dialogue " + dialogueName + ", but there is no such dialogue in dialogueManager");
            return null;
        }

        newDialogue.transform.SetParent(_canvas.transform, false);
        newDialogue.SetActive(true);

        return newDialogue;
    }
}
