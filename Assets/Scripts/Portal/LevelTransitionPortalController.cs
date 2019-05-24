public class LevelTransitionPortalController : IInteractiveObject
{
    public string destinationSceneName;
    public bool resetPlayerPosition = true;
    public float playerRotaitonY = 0;

    private SceneManagerController sceneManager;

    void Start()
    {
        sceneManager = FindObjectOfType<SceneManagerController>();
    }

    public override void Interact()
    {
        SaveGlobalFloat(PlayerSavableController.PLAYER_ROTATION_Y, playerRotaitonY);
        sceneManager.LoadScene(destinationSceneName, !resetPlayerPosition);        
    }
}
