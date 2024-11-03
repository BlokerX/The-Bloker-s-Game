using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireOnOff : MonoBehaviour
{
    public bool campfireIsOn;

    public AudioSource CampfireSound;
    public Light CampfireLight;
    public ParticleSystem Fire;
    public ParticleSystem Flames;
    public ParticleSystem Glow;
    public ParticleSystem SmokeDark;
    public ParticleSystem SmokeLit;
    public ParticleSystem SparksFalling;
    public ParticleSystem SparksRising;

    public GUISkin HelpMessageSkin;

    public KeyCode LightOrPutOutCampfire_KeyCode = KeyCode.F;

    private bool IsPlayerNear = false;

    private const string PlayerColliderTag = "PlayerCamera";

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        // Check is ON/OFF on start
        switch (campfireIsOn)
        {
            // Burn
            case true:
                CampfireSound.Play();
                CampfireLight.enabled = true;
                Fire.enableEmission = true;
                Flames.enableEmission = true;
                Glow.enableEmission = true;
                SmokeDark.enableEmission = true;
                SmokeLit.enableEmission = true;
                SparksFalling.enableEmission = true;
                SparksRising.enableEmission = true;

                campfireIsOn = true;
                break;
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    [System.Obsolete]
    void OnTriggerStay(Collider collider)
    {
        // Dla objektu typu gracz:
        if (collider.CompareTag(PlayerColliderTag))
        {
            if (Input.GetKeyDown(LightOrPutOutCampfire_KeyCode))
            {
                LightOrPutOutCampfire();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag))
        {
            IsPlayerNear = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag))
        {
            IsPlayerNear = false;
        }
    }

    [System.Obsolete]
    void LightOrPutOutCampfire()
    {
        switch (campfireIsOn)
        {
            // Light a fire
            case false:
                CampfireSound.Play();
                CampfireLight.enabled = true;
                Fire.enableEmission = true;
                Flames.enableEmission = true;
                Glow.enableEmission = true;
                SmokeDark.enableEmission = true;
                SmokeLit.enableEmission = true;
                SparksFalling.enableEmission = true;
                SparksRising.enableEmission = true;

                campfireIsOn = true;
                break;

            // Put out the fire
            case true:
                CampfireSound.Stop();
                CampfireLight.enabled = false;
                Fire.enableEmission = false;
                Flames.enableEmission = false;
                Glow.enableEmission = false;
                SmokeDark.enableEmission = false;
                SmokeLit.enableEmission = false;
                SparksFalling.enableEmission = false;
                SparksRising.enableEmission = false;

                campfireIsOn = false;
                break;
        }
    }

    private void OnGUI()
    {
        if (IsPlayerNear)
        {
            if (HelpMessageSkin != null) GUI.skin = HelpMessageSkin;
            Rect rect = new Rect(25, Screen.height - 25 - 110, 420, 110);
            switch (campfireIsOn)
            {
                case false:
                    GUI.Box(rect, $"Press [{LightOrPutOutCampfire_KeyCode.ToString()}]\n to light campfire");
                    break;

                case true:
                    GUI.Box(rect, $"Press [{LightOrPutOutCampfire_KeyCode.ToString()}]\n to put out campfire");
                    break;
            }
        }
    }

}
