using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UIController : MonoBehaviour
{
    #region Variables
    [BoxGroup("Game Control",true,true)]
    public GameObject PauseMenu;

    [BoxGroup("Game Control", true, true)]
    public GameObject OptionsMenu;

    [BoxGroup("Game Control", true, true),LabelText("IsPaused"),ReadOnly]
    public bool paused;
    #endregion

    private void Start()
    {
        paused = false;
    }

    private void Update()
    {
        PauseInputCheck();
    }

    private void PauseInputCheck()
    {
        if((Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.P)) && !paused)
        {
            Pause();
        }
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && paused)
        {
            Resume();
        }
    }

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
        paused = true;
        Debug.Log("Pause");
        Time.timeScale = .01f;
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        paused = false;
        Debug.Log("Resume");
        Time.timeScale = 1;
    }
}
