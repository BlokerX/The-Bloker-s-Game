using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraComponent : MonoBehaviour
{
    public EquipmentManager PlayerEquipmentManager;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerEquipmentManager == null)
        {
            PlayerEquipmentManager = GetComponentInParent<EquipmentManager>();
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (TriggerStayTimer >= TriggerStayTimerInterval)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].CountItems > 0 &&
                    PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot] != null &&
                    PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot].TryGetComponent<ItemObject>(out var itemObject) &&
                    itemObject.MouseLeftButton_GetKeyDown != null)
                {
                    itemObject.MouseLeftButton_GetKeyDown.Invoke(collider, PlayerEquipmentManager);
                }

            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].CountItems > 0)
                {
                    if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot] != null &&
                        PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot].TryGetComponent<ItemObject>(out var itemObject) &&
                        itemObject.MouseLeftButton_GetKey != null)
                    {
                        var SelectedItem = PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot];
                        itemObject.MouseLeftButton_GetKey.Invoke(collider, PlayerEquipmentManager);

                        if (SelectedItem.TryGetComponent<ItemExtractor>(out ItemExtractor ItemExtractor1) &&
                            ItemExtractor1.IsCompareTag(collider) &&
                            collider.gameObject.TryGetComponent<ItemDrop>(out ItemDrop ColliderItemDrop1))
                        {
                            ColliderItemDrop1.InExtract = true;
                        }
                    }
                }
                //else if ((collider.CompareTag("Player") || collider.CompareTag("Bot")))
                //{
                //    collider.gameObject.GetComponent<Health>().GetDamage(1);
                //}
            }

            if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot] != null &&
                PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].CountItems > 0)
            {
                if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot] != null)
                {

                    // Other
                    if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot].TryGetComponent<ItemExtractor>(out ItemExtractor itemExtractor))
                    {
                        if (itemExtractor.IsCompareTag(collider))
                        {
                            itemExtractor.ColliderTag = true;
                        }
                    }
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].CountItems > 0)
            {
                if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot] != null &&
                    PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot].TryGetComponent<ItemObject>(out var itemObject) &&
                    itemObject.MouseLeftButton_GetKey != null)
                {
                    var SelectedItem = PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot];
                    itemObject.MouseLeftButton_GetKey.Invoke(collider, PlayerEquipmentManager);

                    if (SelectedItem.TryGetComponent<ItemExtractor>(out ItemExtractor ItemExtractor1) &&
                        ItemExtractor1.IsCompareTag(collider) &&
                        collider.gameObject.TryGetComponent<ItemDrop>(out ItemDrop ColliderItemDrop1))
                    {
                        ColliderItemDrop1.InExtract = false;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].CountItems > 0)
        {
            if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot] != null)
            {
                if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot].TryGetComponent<ItemExtractor>(out ItemExtractor itemExtractor))
                {
                    if (itemExtractor.IsCompareTag(collider))
                    {
                        itemExtractor.ColliderTag = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].CountItems > 0)
        {
            if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot] != null)
            {
                if (PlayerEquipmentManager.Slots[PlayerEquipmentManager.SelectedSlot].Items[PlayerEquipmentManager.SelectedItemInSlot].TryGetComponent<ItemExtractor>(out ItemExtractor itemExtractor))
                {
                    if (itemExtractor.IsCompareTag(collider))
                    {
                        itemExtractor.ColliderTag = false;
                    }
                }
            }
        }
    }


    private readonly float TriggerStayTimerInterval = 0.1f;
    private float TriggerStayTimer = 0;
    private void Update()
    {


        //Timer
        if (TriggerStayTimer < TriggerStayTimerInterval)
            TriggerStayTimer += Time.deltaTime;
    }

}

