using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [Header("Game Controllers")]
    [SerializeField] private MainMenuUIController menuController  = null;
    [SerializeField] private AudioManager   audioManager    = null;

    public PlayerController playerController = null;
    [Header("Level settings")]
    [SerializeField] private string newGameLevel;

    private string levelToLoad;

    private SettingsData settings;


    public static GameController instance = null;

    public SettingsData.AudioSettings GetAudioSettings => settings.audioSettings;

    public void UpdateAudioSettings(in SettingsData.AudioSettings newSettings)
    {
        settings.audioSettings = newSettings;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    private void Start()
    {
        LoadSettingsFromDisk();

        if (settings == null)
        {
            settings = new SettingsData();
            ApplyDefaultAudioSettings();
        }

        if (audioManager)
        {
            audioManager.Init();
        }
        if (menuController)
        {
            menuController.Init();
        }
    }

    public void ApplyDefaultAudioSettings()
    {
        GetAudioSettings.masterVolume = 0.5f;
        GetAudioSettings.musicVolume = 1.0f;
        GetAudioSettings.effectsVolume = 1.0f;
        GetAudioSettings.ambientVolume = 1.0f;
    }

    public void LoadSettingsFromDisk()
    {
        GameSettings.LoadSettings(out settings);
    }

    public void SaveSettingsToDisk()
    {

        if (settings != null)
        {
            GameSettings.SaveSettings(settings);
        }
    }

    public void StartNewGame()
    {
        if(newGameLevel != null)
        {
            SetLevel(newGameLevel);
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        if(levelToLoad != null)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    private void SetLevel(string levelName)
    {
        levelToLoad = levelName;
    }
}
