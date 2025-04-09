using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
