using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMenuEvents : MonoBehaviour
{
    public GameObject RespawnMenuPanel;
    private GameObject Player;

    public void ShowRespawnMenu(GameObject argPlayer)
    {
        StopTime();
        RespawnMenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player = argPlayer;
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

    public void Respawn()
    {
        RespawnMenuPanel.SetActive(false);
        StartTime();
        Player.GetComponent<Health>().Respawn();
        Player = null;
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    }
}
