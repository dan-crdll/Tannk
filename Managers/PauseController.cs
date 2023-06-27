using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    bool inPause = false;
    [SerializeField] CanvasGroup pauseScreen;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = !inPause;
            CanvasSwitcher.ToggleCanvasGroup(pauseScreen, inPause);
            if(inPause)
            {
                Time.timeScale = 0;
            }
        }
    }

    public void OnCloseBtn()
    {
        Time.timeScale = 1;
        inPause = false;
        CanvasSwitcher.ToggleCanvasGroup(pauseScreen, inPause);
    }

    public void OnDesktopBtn()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void OnMainMenuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
