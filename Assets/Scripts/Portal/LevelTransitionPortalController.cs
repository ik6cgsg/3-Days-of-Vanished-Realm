using System.Collections;
using UnityEngine;

public class LevelTransitionPortalController : IInteractiveObject
{
    public string destinationSceneName;
    public bool resetPlayerPosition = true;

    private SceneManagerController sceneManager;

    void Start()
    {
        sceneManager = FindObjectOfType<SceneManagerController>();
    }

    public override void Interact()
    {
        sceneManager.LoadScene(destinationSceneName, !resetPlayerPosition);        
    }
}
