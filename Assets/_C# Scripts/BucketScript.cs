using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketScript : MonoBehaviour
{
    public bool Filled = false;

    public GameObject WaterPlace;

    public Sprite WaterBucketSprite;
    private Sprite EmptyBucketSprite;

    public string[] WaterTags = new string[1] { "Water" };

    private ItemObject itemObject;

    public float BucketTimerInterwal = 0.1f;
    private float BucketTimer = 0;

    private void Awake()
    {
        itemObject = this.gameObject.GetComponent<ItemObject>();
        EmptyBucketSprite = itemObject.ItemSprite;
    }

    void Start()
    {
        itemObject.MouseLeftButton_GetKeyDown += FillOrPutOutWater;
    }

    void Update()
    {
        if (BucketTimer < BucketTimerInterwal)
        {
            BucketTimer += Time.deltaTime;
        }
    }

    private void FillOrPutOutWater(Collider collider, EquipmentManager equipmentManager)
    {
        if (BucketTimer >= BucketTimerInterwal)
        {
            if (Filled)
            {
                PourOutWater();
            }
            else if (!Filled && CompareTag(collider.tag, WaterTags))
            {
                FillWater();
            }
            equipmentManager.Slots[equipmentManager.SelectedSlot].GUISlotImage.sprite = equipmentManager.Slots[equipmentManager.SelectedSlot].Items[0].GetComponent<ItemObject>().ItemSprite;
            BucketTimer = 0;
        }
    }

    private void FillWater()
    {
        WaterPlace.SetActive(true);
        itemObject.ChangeItemSprite(WaterBucketSprite);
        Filled = true;
    }

    private void PourOutWater()
    {
        WaterPlace.SetActive(false);
        itemObject.ChangeItemSprite(EmptyBucketSprite);
        Filled = false;
    }

    private bool CompareTag(string tag, string[] tags)
    {
        bool b = false;
        foreach (var item in tags)
        {
            if (tag == item)
            {
                b = true;
                break;
            }
        }
        return b;
    }
}
