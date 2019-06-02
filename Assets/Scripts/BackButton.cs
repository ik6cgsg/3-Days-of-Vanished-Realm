using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            FindObjectOfType<SceneManagerController>().LoadScene("Menu");
        }
        //if running on Android, check for Menu/Home and exit
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home) || Input.GetKey(KeyCode.Menu))
            {
                Application.Quit();
                return;
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                if (SceneManager.GetActiveScene().name == "Menu")
                {
                    Application.Quit();
                    return;
                }
                FindObjectOfType<SceneManagerController>().LoadScene("Menu");
            }
        }
    }
}

