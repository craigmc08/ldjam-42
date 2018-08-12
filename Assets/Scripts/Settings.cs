using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings {
    static float mouseSensitivity;

    static float fxVolume;
    static float musicVolume;

    public static void Load()
    {
        mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity", 2f);
        fxVolume = PlayerPrefs.GetFloat("fxVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
    }

    public static float GetFloat(string name)
    {
        switch (name)
        {
            case "mouseSensitivity":
                return MouseSensitivity;
            case "fxVolume":
                return FXVolume;
            case "musicVolume":
                return MusicVolume;
        }
        return default(float);
    }
    public static void SetFloat(string name, float value)
    {
        switch (name)
        {
            case "mouseSensitivity":
                MouseSensitivity = value;
                break;
            case "fxVolume":
                FXVolume = value;
                break;
            case "musicVolume":
                MusicVolume = value;
                break;
        }
    }

    public static float MouseSensitivity
    {
        get
        {
            return mouseSensitivity;
        }
        set
        {
            mouseSensitivity = value;
            PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivity);
        }
    }

    public static float FXVolume
    {
        get
        {
            return fxVolume;
        }
        set
        {
            fxVolume = value;
            PlayerPrefs.SetFloat("fxVolume", fxVolume);
        }
    }

    public static float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            fxVolume = value;
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
        }
    }
}
