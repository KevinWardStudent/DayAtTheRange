using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour {

    // first Add Scene Management
    // Create public method, so that it can be accessed by multiple buttons
    // For Modularity, the method will take a string argument for which scene we want to jump to
    // Add Scene Manager, this takes either a string or int, 
    // So now we need to attach this to some game object, create an empty
    // Attach this script as a component to Menu Object
    // Then in the button go to on click event
    // it will change any variable, etc of any scripts, so attach menu object

    // Tool Tip guide
    /// <summary>
    /// This script changes the level.
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
