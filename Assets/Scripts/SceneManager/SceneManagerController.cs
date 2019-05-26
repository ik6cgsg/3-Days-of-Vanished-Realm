using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerController : FloorController
{
    private SaveSystemController saveSystem;

    private AsyncOperation asyncLoad;

    void Start()
    {
        saveSystem = Object.FindObjectOfType<SaveSystemController>();
        SetUpFields();
        isJumping = true;
        IsJumpingStatic = true;
        jumpState = JumpState.FADE_OUT;
    }

    public void LoadScene(string sceneName, bool loadSavedPosition = false)
    {
        // Save current scene state
        if (saveSystem != null)
        {
            saveSystem.SaveSceneObjects();
        }

        // Set player position load boolean
        ISavableObject.SaveGlobalBool(PlayerSavableController.LOAD_COORDINATES, loadSavedPosition);

        // Set load inventory boolean to false (inventory is static, saves from scene to scene)
        ISavableObject.SaveGlobalBool(InventoryController.LOAD_INVENTORY, false);

        // Change scene
        IsJumpingStatic = true;
        isJumping = true;
        timer = 0;
        jumpState = JumpState.FADE_IN;
        blackScreen.transform.localScale = new Vector3(playerRadius, playerRadius, playerRadius);
        asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        Debug.Log("Started scene load");
    }

    private void Update()
    {
        if (!isJumping)
        {
            return;
        }

        Material mtl = blackScreen.GetComponent<Renderer>().material;

        timer += Time.deltaTime;

        switch (jumpState)
        {
            case JumpState.FADE_IN:
                // Fade to black
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to wait");
                    jumpState = JumpState.WAIT;
                    timer = 0;
                    mtl.SetColor("_Color", SetFadeColorAlpha(1));
                    return;
                }
                fadeColor.a = 1;
                mtl.SetColor("_Color", SetFadeColorAlpha(timer / fadeTime));
                break;
            case JumpState.WAIT:
                // Wait with black screen
                asyncLoad.allowSceneActivation = true;
                return;
            case JumpState.FADE_OUT:
                // Fade out of black
                if (timer >= fadeTime)
                {
                    Debug.Log("Switch to no jump");
                    mtl.SetColor("_Color", SetFadeColorAlpha(0));
                    IsJumpingStatic = false;
                    isJumping = false;
                    return;
                }
                mtl.SetColor("_Color", SetFadeColorAlpha(1.0F - timer / fadeTime));
                break;
        }
    }
}
