using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExtractor : MonoBehaviour
{
    public bool InExtracting = false;
    public const float ExtractingSpeedInSeconds = 0.5f;
    float extractingTimer = 0;
    public bool ColliderTag = false;

    public string[] AllowItemsTags;

    private void OnDisable()
    {
        ColliderTag = false;
        this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
        InExtracting = false;
        extractingTimer = 0;
    }

    private void Awake()
    {
        this.GetComponent<ItemObject>().MouseLeftButton_GetKey += ExtractMethod;
    }

    public void ExtractMethod(Collider collider, EquipmentManager equipmentManager)
    {
        if (IsCompareTag(collider) && !InExtracting)
        {
            InExtracting = true;
            this.gameObject.GetComponent<ItemObject>().IsLockForUse = true;
        }
    }

    public bool IsCompareTag(Collider collider)
    {
        foreach (var item in AllowItemsTags)
        {
            if (collider.CompareTag(item))
            {
                return true;
            }
        }
        return false;
    }
    
    public bool IsCompareTag(GameObject collider)
    {
        foreach (var item in AllowItemsTags)
        {
            if (collider.CompareTag(item))
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<ItemObject>().HaveOvner && this.gameObject.activeInHierarchy)
        {

            if (Input.GetKey(KeyCode.Mouse0) && !InExtracting && ColliderTag)
            {
                InExtracting = true;
            }

            else if (Input.GetKeyUp(KeyCode.Mouse0) && InExtracting)
            {
                this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
                this.gameObject.GetComponent<ItemObject>().IsLockForUse = false;
                InExtracting = false;
                extractingTimer = 0;
            }

            if (Input.GetKey(KeyCode.Mouse0) && InExtracting)
            {
                if (extractingTimer < ExtractingSpeedInSeconds / 2)
                {
                    extractingTimer += Time.deltaTime;
                    this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(
                        0,
                        0,
                        this.gameObject.GetComponent<Transform>().localRotation.eulerAngles.z - (90 / ExtractingSpeedInSeconds * Time.deltaTime)
                        );
                }
                else if (extractingTimer <= ExtractingSpeedInSeconds)
                {
                    extractingTimer += Time.deltaTime;
                    this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(
                        0,
                        0,
                        this.gameObject.GetComponent<Transform>().localRotation.eulerAngles.z + (90 / ExtractingSpeedInSeconds * Time.deltaTime)
                        );
                }
                else
                {
                    this.gameObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 0);
                    InExtracting = false;
                    extractingTimer = 0;
                }
            }
        }
    }

}
