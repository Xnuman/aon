using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject NoSavedLevelPopUpDialog = null;

    public void Init()
    {
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
}
