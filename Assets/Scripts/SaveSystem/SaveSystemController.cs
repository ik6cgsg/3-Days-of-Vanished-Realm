using UnityEngine;

public class SaveSystemController : MonoBehaviour
{
    ISavableObject[] savableObjects;

    private void Start()
    {
        // Find all savable objects on the scene
        savableObjects = Resources.FindObjectsOfTypeAll<ISavableObject>();

        // Load all savable objects
        LoadSceneObjects();
    }

    private void OnApplicationQuit()
    {
        SaveSceneObjects();    
    }

    public void SaveSceneObjects()
    {
        // Save all savable objects on the scene
        foreach (ISavableObject savable in savableObjects)
        {
            savable.Save();
        }

        PlayerPrefs.Save();
    }

    public void LoadSceneObjects()
    {
        // Load all savable objects on the scene
        foreach (ISavableObject savable in savableObjects)
        {
            savable.Load();
        }
    }
}
