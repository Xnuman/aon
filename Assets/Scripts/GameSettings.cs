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

        public static bool operator ==(AudioSettings a, AudioSettings b)
        {
            return a.masterVolume == b.masterVolume && a.musicVolume == b.musicVolume && a.effectsVolume == b.effectsVolume && a.ambientVolume == b.ambientVolume;
        }

        public static bool operator !=(AudioSettings a, AudioSettings b)
        {
            return !(a == b);
        }
    }

    public AudioSettings audioSettings;
}
public class GameSettings : MonoBehaviour
{

    public SettingsData settings;
    private static string SettingsPath => System.IO.Path.Combine(Application.persistentDataPath, "necro_settings.dat");
    public static void LoadSettings(out SettingsData data)
    {
        if(File.Exists(SettingsPath) == false)
        {
            data = null;
            return;
        }

        string fileData = File.ReadAllText(SettingsPath);
        
        data = JsonUtility.FromJson<SettingsData>(fileData);
    }

    public static void SaveSettings(SettingsData data)
    {
        string fileData = JsonUtility.ToJson(data);
        File.WriteAllText(SettingsPath, fileData);
    }
}
