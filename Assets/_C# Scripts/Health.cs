using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Health
    public int HealthPoints;
    public int HealthSlots = 100;
    private SpawnpointAndRespawn spawnpointAndRespawn;
    public GUISkin HealthGUISkin;
    public GameObject RespawnPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameObjects.GameObjectsList.Add(this.gameObject);
        HealthPoints = HealthSlots;
        spawnpointAndRespawn = GetComponent<SpawnpointAndRespawn>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void GetDamage(int damage)
    {
        if (HealthPoints - damage > 0)
        {
            HealthPoints -= damage;
        }
        else
        {
            HealthPoints = 0;
            Kill();
        }
    }

    public void GetHeal(int healthPointToHeal)
    {
        if (HealthPoints + healthPointToHeal <= HealthSlots)
        {
            HealthPoints += healthPointToHeal;
        }
        else { HealthPoints = HealthSlots; }
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
        RespawnPanel.SetActive(true);
        RespawnPanel.GetComponent<RespawnMenuEvents>().ShowRespawnMenu(this.gameObject);
    }

    public void Respawn()
    {
        HealthPoints = HealthSlots;
        spawnpointAndRespawn.GoToRespawn();
        this.gameObject.SetActive(true);
    }

    private void OnGUI()
    {
        GUI.skin = HealthGUISkin;
        GUI.skin.box.fontSize = (int)(0.012f * Screen.width);
        GUI.Box(new Rect(0.01f * Screen.width, 0.012f * Screen.height, 0.175f * Screen.width, 0.05f * Screen.height), "Health points: " + HealthPoints + " / " + HealthSlots);
        //GUI.Box(new Rect(10, 40, 260, 25), "Defence points: " + DefencePoints + " / " + DefenceSlots);
    }

}

public class GameObjects
{
    // ListOfObject
    public static List<GameObject> GameObjectsList = new List<GameObject>();
}
