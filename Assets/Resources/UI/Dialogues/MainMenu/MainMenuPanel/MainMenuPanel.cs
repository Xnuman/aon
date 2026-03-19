using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public Button _Continue = null;
    public Button _NewGame = null;
    public Button _Options = null;
    public Button _Quit = null;

    private DialogueManager _dialogueManager = null;
    private Canvas _canvas = null;

    public void Init(Canvas canvas)
    {
        _dialogueManager = GameController.instance.m_dialogueManager;
        _canvas = canvas;

        _NewGame.onClick.AddListener(() =>
        {
            StartGameDialogYes();
        });

        _Options.onClick.AddListener(() =>
        {
            var go = _dialogueManager.CreateDialogue("Options", _canvas);

            OptionsMenuPanel optionsMenuPanelComponent = go.GetComponent<OptionsMenuPanel>();
            optionsMenuPanelComponent.Init(_canvas);
        });

        _Quit.onClick.AddListener(() =>
        {
            GameController.instance.QuitGame();
        });
    }
    public void StartGameDialogYes()
    {
        GameController.instance.StartNewGame();
    }
    public void QuitGameDialogYes()
    {
        Application.Quit();
    }
}
