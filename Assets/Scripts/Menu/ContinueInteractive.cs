public class ContinueInteractive : IInteractiveObject
{
    public override void Interact()
    {
        string currentScene = LoadGlobalString("currentScene");

        if (!currentScene.Equals(""))
        {
            FindObjectOfType<SceneManagerController>().LoadScene(currentScene, true, true);
        }
        else
        {
            MenuController.NewGameClicked();
        }
    }
}
