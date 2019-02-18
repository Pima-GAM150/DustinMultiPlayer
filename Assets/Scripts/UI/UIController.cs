using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Variables
    [BoxGroup("Game Control",true,true)]
    public GameObject PauseMenu;

    [BoxGroup("Game Control", true, true)]
    public GameObject OptionsMenu;
    #endregion

    public void StartConnection()
    {
        SceneManager.LoadScene("ServerConnection");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    public void MainMenu()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Debug.Log("Pause");
        Time.timeScale = .01f;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Debug.Log("Resume");
        Time.timeScale = 1;
    }
}
