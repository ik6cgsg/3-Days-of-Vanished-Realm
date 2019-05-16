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

    private string GetId()
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
}
