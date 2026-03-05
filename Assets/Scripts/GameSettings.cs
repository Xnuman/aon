using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class SettingsData
{ 
    public SettingsData()
    {
        audioSettings = new AudioSettings();
    }

    [System.Serializable]
    public class AudioSettings : IEquatable<AudioSettings>
    {
        public float masterVolume;
        public float musicVolume;
        public float effectsVolume;
        public float ambientVolume;

        public bool Equals(AudioSettings other)
        {
            if(other == null)
                return false;

            return masterVolume == other.masterVolume && musicVolume == other.musicVolume && effectsVolume == other.effectsVolume && ambientVolume == other.ambientVolume;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AudioSettings);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(masterVolume, musicVolume, effectsVolume, ambientVolume);
        }

        public static bool operator ==(AudioSettings a, AudioSettings b)
        {
            if( ReferenceEquals( a, b) )
                return true;
            if( a is null )
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(AudioSettings a, AudioSettings b) => !(a == b);
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
