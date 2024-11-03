using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenCloseCraftingCanvas : MonoBehaviour
{
    public RigidbodyFirstPersonController Player;
    public GameObject CraftingPanel;
    public KeyCode OpenCloseCraftingPanelKeyCode = KeyCode.I;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CraftingPanel.SetActive(false);
            Player.enabled = true;
        }
        if (Input.GetKeyDown(OpenCloseCraftingPanelKeyCode))
        {
            switch (CraftingPanel.activeInHierarchy)
            {
                case true:
                    CraftingPanel.SetActive(false);
                    Player.enabled = true;
                    break;
                case false:
                    Cursor.visible = true;
                    Player.enabled = false;
                    CraftingPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }
        }
    }
}
