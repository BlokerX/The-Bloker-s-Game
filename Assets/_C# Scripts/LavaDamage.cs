using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public int Damage = 3;
    public float DamageTimerInterval = 0.5f;
    float DamageTimer;

    Health PlayerHealth;
    bool do_ = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            do_ = true;
            collision.collider.TryGetComponent<Health>(out PlayerHealth);
            DamageTimer = 0;

        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            do_ = false;
            DamageTimer = 0;
            PlayerHealth = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DamageTimer < DamageTimerInterval)
        {
            DamageTimer += Time.deltaTime;
        }
        else if (do_ && DamageTimer >= DamageTimerInterval)
        {
            PlayerHealth.GetDamage(Damage);
            DamageTimer = 0;
        }
    }
}
