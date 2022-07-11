using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SettingsData
{ 
    public SettingsData()
    {
        audioSettings = new AudioSettings();
    }

    [System.Serializable]
    public class AudioSettings
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
        public float ambientVolume;
    }

    public AudioSettings audioSettings;
}
public class GameSettings : MonoBehaviour
{

    public SettingsData settings;
    private static string path => Path.Combine(Application.persistentDataPath, "necro_settings.dat");
    public static void LoadSettings(out SettingsData data)
    {
        if(File.Exists(path) == false)
        {
            data = null;
            return;
        }

        string fileData = File.ReadAllText(path);
        
        data = JsonUtility.FromJson<SettingsData>(fileData);
    }

    public static void SaveSettings(SettingsData data)
    {
        string fileData = JsonUtility.ToJson(data);
        File.WriteAllText(path, fileData);
    }
}
