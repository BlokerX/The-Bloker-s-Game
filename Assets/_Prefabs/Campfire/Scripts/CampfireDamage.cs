using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireDamage : MonoBehaviour
{
    public CampfireOnOff CampfireSwitch;
    public int FireDamage = 5;
    public float InterwalOfGetDamage = 1;

    [HideInInspector]
    public bool GetDamage;
    [HideInInspector]
    public float DamageTimer;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetDamage = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (CampfireSwitch.campfireIsOn)
            {
                if (DamageTimer >= InterwalOfGetDamage)
                {
                    collider.gameObject.GetComponent<Health>().GetDamage(FireDamage);
                    DamageTimer = 0;
                }
                else
                {
                    DamageTimer += Time.deltaTime * 0.9f;
                }
            }
            else
            {
                if (DamageTimer != 0)
                { DamageTimer = 0; }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GetDamage = false;
            DamageTimer = 0;
        }
    }
}
