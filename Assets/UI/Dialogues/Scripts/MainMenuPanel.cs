using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuPanel : MonoBehaviour
{
    public Button _Continue = null;
    public Button _NewGame = null;
    public Button _Options = null;
    public Button _Quit = null;

    private DialogueManager _dialogueManager = null;

    public void Init()
    {
        _dialogueManager = GameController.instance.m_dialogueManager;

        _NewGame.onClick.AddListener(() =>
        {
            StartGameDialogYes();
        });

        _Options.onClick.AddListener(() =>
        {
            var go = _dialogueManager.CreateDialogue("Options");
            Canvas canvas = this.GetComponentInParent<Canvas>();

            go.transform.SetParent(canvas.transform, false);
            go.SetActive(true);
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
