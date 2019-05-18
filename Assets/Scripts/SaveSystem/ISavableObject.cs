using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public abstract class ISavableObject : MonoBehaviour
{
    public string uniqueObjectName;

    private string id;

    virtual public void Save()
    {
    }

    virtual public void Load()
    {
    }

    public string GetId()
    {
        if (id == null)
            id = gameObject.scene.name + uniqueObjectName;

        return id;
    }

    protected void SaveInt(string valueName, int value)
    {
        PlayerPrefs.SetInt(GetId() + valueName, value);
    }

    protected void SaveFloat(string valueName, float value)
    {
        PlayerPrefs.SetFloat(GetId() + valueName, value);
    }

    protected void SaveBool(string valueName, bool value)
    {
        PlayerPrefs.SetInt(GetId() + valueName, value ? 1 : 0);
    }

    protected void SaveString(string valueName, string value)
    {
        PlayerPrefs.SetString(GetId() + valueName, value);
    }

    public static void SaveGlobalInt(string valueName, int value)
    {
        PlayerPrefs.SetInt(valueName, value);
        PlayerPrefs.Save();
    }

    public static void SaveGlobalFloat(string valueName, float value)
    {
        PlayerPrefs.SetFloat(valueName, value);
        PlayerPrefs.Save();
    }

    public static void SaveGlobalBool(string valueName, bool value)
    {
        PlayerPrefs.SetInt(valueName, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SaveGlobalString(string valueName, string value)
    {
        PlayerPrefs.SetString(valueName, value);
        PlayerPrefs.Save();
    }

    protected int LoadInt(string valueName)
    {
        return PlayerPrefs.GetInt(GetId() + valueName);
    }

    protected float LoadFloat(string valueName)
    {
        return PlayerPrefs.GetFloat(GetId() + valueName);
    }

    protected bool LoadBool(string valueName)
    {
        return PlayerPrefs.GetInt(GetId() + valueName) == 1;
    }

    protected string LoadString(string valueName)
    {
        return PlayerPrefs.GetString(GetId() + valueName);
    }

    public static int LoadGlobalInt(string valueName)
    {
        return PlayerPrefs.GetInt(valueName);
    }

    public static float LoadGlobalFloat(string valueName)
    {
        return PlayerPrefs.GetFloat(valueName);
    }

    public static bool LoadGlobalBool(string valueName)
    {
        return PlayerPrefs.GetInt(valueName) == 1;
    }

    public static string LoadGlobalString(string valueName)
    {
        return PlayerPrefs.GetString(valueName);
    }
}
