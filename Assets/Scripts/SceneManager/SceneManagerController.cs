using UnityEngine;

public class SceneManagerController : MonoBehaviour
{
    SaveSystemController saveSystem;

    void Start()
    {
        saveSystem = Object.FindObjectOfType<SaveSystemController>();
    }

    public void LoadScene(string sceneName)
    {
        // Save current scene state
        if (saveSystem != null)
        {
            saveSystem.SaveSceneObjects();
        }

        // Set player position to not load
        ISavableObject.SaveGlobalBool(PlayerSavableController.LOAD_COORDINATES, false);

        // Change scene
        /// ...
    }
}
