using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelecter : MonoBehaviour
{

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CustomMapSelectScene")
        {

            Cursor.lockState = CursorLockMode.None;
            this.gameObject.SetActive(true);
            
        }
    }
  
}
