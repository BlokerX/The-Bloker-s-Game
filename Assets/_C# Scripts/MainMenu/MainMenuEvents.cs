using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEvents : MonoBehaviour
{
    public GameObject MenuPanel;
    public KeyCode MainMenuKeyCode = KeyCode.Escape;

    public void Update()
    {
        if (Input.GetKeyDown(MainMenuKeyCode))
        {
            StopTime();
            MenuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
