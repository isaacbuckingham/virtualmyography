using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Function to switch to a specified scene
    public void SwitchScene(string sceneName)
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.UnloadSceneAsync(currentScene);
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
   /* 
    // Optional: Function to reload the current scene
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    } */
}
