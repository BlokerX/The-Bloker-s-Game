using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    public Light słońce;
    public Light księżyc;
    [Range(0, 60)] public float minutyCyklu;
    [Range(0, 1)] public float aktualnyCzas;
    private float mnożnikCzasu = 1f;
    private float intensywnośćSłońca;
    private float intensywnośćCieni;

    private Quaternion rotacjaSłońca;
    private Quaternion rotacjaKsiężyca;

    public Material[] Skyboxes;

    private void Awake()
    {
        AktualizujNiebo();
    }

    public void Start()
    {
        intensywnośćSłońca = słońce.intensity;
        intensywnośćCieni = słońce.shadowStrength;
    }

    public void AktualizujSłońce()
    {
        rotacjaSłońca = Quaternion.Euler((aktualnyCzas * 360f) - 90, 0, 0);
        słońce.transform.localRotation = rotacjaSłońca;

        float aktualnaIntensywnośćSłońca = 1;
        float aktualnaIntensywnośćCieni = 1;

        if (aktualnyCzas >= 0.23f || aktualnyCzas <= 0.75f)
        {
            aktualnaIntensywnośćCieni = 0.8f;
        }

        if (aktualnyCzas <= 0.23f || aktualnyCzas >= 0.75f)
        {
            aktualnaIntensywnośćSłońca = 0;
            aktualnaIntensywnośćCieni = 1;
        }

        else if (aktualnyCzas < 0.25f)
        {
            aktualnaIntensywnośćSłońca = Mathf.Clamp01((aktualnyCzas - 0.23f) * (1 / 0.02f));
            aktualnaIntensywnośćCieni = Mathf.Clamp((aktualnyCzas - 0.23f) * (1 / 0.02f), 1, 0.6f);
        }

        else if (aktualnyCzas >= 0.73f)
        {
            aktualnaIntensywnośćSłońca = Mathf.Clamp01(1 - (aktualnyCzas - 0.73f) * (1 / 0.02f));
            aktualnaIntensywnośćCieni = Mathf.Clamp((aktualnyCzas - 0.73f) * (1 / 0.02f), 0.6f, 1);
        }

        słońce.intensity = intensywnośćSłońca * aktualnaIntensywnośćSłońca;
        słońce.shadowStrength = intensywnośćCieni * aktualnaIntensywnośćCieni;
    }
    
    public void AktualizujKsiężyc()
    {
        rotacjaKsiężyca = Quaternion.Euler((aktualnyCzas * 360f) + 90, 0, 0);
        księżyc.transform.localRotation = rotacjaKsiężyca;
    }

    public void Update()
    {
        AktualizujSłońce();
        AktualizujKsiężyc();

        aktualnyCzas += Time.deltaTime / (minutyCyklu * 60) * mnożnikCzasu;

        if (aktualnyCzas >= 1)
            aktualnyCzas = 0;
    }

    public void FixedUpdate()
    {
        AktualizujNiebo();
    }

    public void AktualizujNiebo()
    {
        if (aktualnyCzas > 0.20f && aktualnyCzas <= 0.35f)
        {
            RenderSettings.skybox = Skyboxes[0];
        }
        else if (aktualnyCzas > 0.35f && aktualnyCzas <= 0.65f)
        {
            RenderSettings.skybox = Skyboxes[1];
        }
        else if (aktualnyCzas > 0.65f && aktualnyCzas <= 0.80f)
        {
            RenderSettings.skybox = Skyboxes[2];
        }
        else if (aktualnyCzas > 0.80f || aktualnyCzas <= 0.20f)
        {
            RenderSettings.skybox = Skyboxes[3];
        }
    }

}