using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    /* TODO: change serialize field + public to something less stupid */
    [Header("Game Controllers")]
    [SerializeField] public MainMenuUIController m_uiMenuController = null;
    [SerializeField] public SettingsUIController m_uiSettingsController = null;
    [SerializeField] public AudioManager m_audioManager = null;
    [Header("Level settings")]
    [SerializeField] private string newGameLevel;
    [Header("Default settings")]
    [SerializeField] private SettingsData.AudioSettings m_defaultAudioSettings;

    private string levelToLoad;

    private SettingsData settings;


    public static GameController instance = null;

    public SettingsData.AudioSettings GetAudioSettings => settings.audioSettings;
    public SettingsData.AudioSettings GetDefaultAudioSettings => m_defaultAudioSettings;
    public void UpdateAudioSettings(in SettingsData.AudioSettings newSettings)
    {
        settings.audioSettings = newSettings;
        if (m_audioManager != null)
        {
            m_audioManager.UpdateCfgSettings();
            m_audioManager.ResetToCfgSettings();
            m_uiSettingsController.ResetAudioSettingsToCfg(newSettings);
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        LoadSettingsFromDisk();

        if (settings == null)
        {
            settings = new SettingsData();
            ApplyDefaultAudioSettings();
        }

        if (m_audioManager)
        {
            m_audioManager.Init();
        }
        if (m_uiMenuController)
        {
            m_uiMenuController.Init();
        }
        if(m_uiSettingsController)
        {
            m_uiSettingsController.Init();
        }
    }

    public void ApplyDefaultAudioSettings()
    {
        UpdateAudioSettings(m_defaultAudioSettings);
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
