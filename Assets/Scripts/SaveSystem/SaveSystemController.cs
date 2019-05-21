using UnityEngine;

public class SaveSystemController : ISavableObject
{
    ISavableObject[] savableObjects;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        // Find all savable objects on the scene
        //savableObjects = Resources.FindObjectsOfTypeAll<ISavableObject>();
        savableObjects = Object.FindObjectsOfType<ISavableObject>();

        // Load all savable objects if not first scene load
        if (LoadBool("parametersExist"))
        {
            LoadSceneObjects();
        }
        SaveBool("parametersExist", true);
        SaveGlobalBool(PlayerSavableController.LOAD_COORDINATES, true);
    }

    private void OnApplicationPause()
    {
        Debug.Log("Application pause");
        SaveSceneObjects();
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application quit");
        SaveSceneObjects();
    }

    public void SaveSceneObjects()
    {
        Debug.Log("Saving scene objects");
        // Save all savable objects on the scene
        foreach (ISavableObject savable in savableObjects)
        {
            savable.Save();
        }

        PlayerPrefs.Save();
    }

    public void LoadSceneObjects()
    {
        Debug.Log("Loading scene objects");

        // Load all savable objects on the scene
        foreach (ISavableObject savable in savableObjects)
        {
            savable.Load();
        }
    }
}
