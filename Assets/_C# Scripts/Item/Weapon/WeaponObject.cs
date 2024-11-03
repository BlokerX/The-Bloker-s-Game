using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public int AttackDamage = 10;

    public float AttackSpeedInSeconds = 1;
    private float attackTimer = 0;

    private readonly float AttackAnimationSpeedInSeconds = 0.5f;
    private float attackAnimationTimer = 0;
    
    private bool InAttack = false;

    private void OnDisable()
    {
        //InAttack = false;
        this.gameObject.GetComponent<ItemObject>().IsLockForUse = false;
        this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
        attackAnimationTimer = 0;
    }

    private void Awake()
    {
        this.GetComponent<ItemObject>().MouseLeftButton_GetKeyDown += NormalAttack;
    }

    public void NormalAttack(Collider collider, EquipmentManager equipmentManager)
    {
        if ((collider.CompareTag("Player") || collider.CompareTag("Bot")) && !InAttack)
        {
            InAttack = true;
            this.gameObject.GetComponent<ItemObject>().IsLockForUse = true;
            collider.gameObject.GetComponent<Health>().GetDamage(AttackDamage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<ItemObject>().HaveOvner && this.gameObject.activeInHierarchy)
        {

            if (Input.GetKeyDown(KeyCode.Mouse0) && !this.gameObject.GetComponent<ItemObject>().IsLockForUse)
            {
                InAttack = true;
                this.gameObject.GetComponent<ItemObject>().IsLockForUse = true;
            }

            else if (InAttack)
            {
                if (attackTimer < AttackSpeedInSeconds)
                {
                    attackTimer += Time.deltaTime;
                    if (attackAnimationTimer < AttackAnimationSpeedInSeconds / 2)
                    {
                        attackAnimationTimer += Time.deltaTime;
                        this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(
                            0,
                            0,
                            this.gameObject.GetComponent<Transform>().localRotation.eulerAngles.z - (90 / AttackAnimationSpeedInSeconds * Time.deltaTime)
                            );
                    }
                    else if (attackAnimationTimer <= AttackAnimationSpeedInSeconds)
                    {
                        attackAnimationTimer += Time.deltaTime;
                        this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(
                            0,
                            0,
                            this.gameObject.GetComponent<Transform>().localRotation.eulerAngles.z + (90 / AttackAnimationSpeedInSeconds * Time.deltaTime)
                            );
                    }
                    else if(attackAnimationTimer > AttackAnimationSpeedInSeconds)
                    {
                        this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else
                {
                    this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
                    InAttack = false;
                    this.gameObject.GetComponent<ItemObject>().IsLockForUse = false;
                    attackTimer = 0;
                    attackAnimationTimer = 0;
                }
            }
        }
    }

}
