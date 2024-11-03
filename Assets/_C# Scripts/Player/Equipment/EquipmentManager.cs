using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //public List<GameObject[]> Items = new List<GameObject[]>();
    //public bool[] AllowItem = new bool[9] { true, true, true, true, true, true, true, true, true };

    // Pomocnik w transporcie
    public GameObject[] guiSlots = new GameObject[9]; // todo rozbudowa? do 36 slotów

    // Equipment:
    public EquipmentSlot[] Slots { get; set; }
    public int CountOfSlots { get; set; } = 9;

    public int SelectedSlot = 0;
    public int SelectedItemInSlot = 0;

    public Sprite EmptySlot;
    public GameObject[] Hand = new GameObject[1];

    // Start is called before the first frame update
    void Awake()
    {
        EquipmentSlot.EmptySlotImage = EmptySlot;
        Slots = new EquipmentSlot[CountOfSlots];
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = new EquipmentSlot(64, guiSlots[i]);
            Slots[i].UpdateGUISlotCountTextBox();
        }
        guiSlots = null;
    }

    private void Start()
    {
        SelectItem(SelectedSlot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectItem(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectItem(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectItem(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectItem(8);
        }

        else if (Input.GetKeyDown(KeyCode.Q) &&
            PutOutOrDropDownTimer > PutOutOrDropDownInterwal)
        {
            DropDownSelectedItem();
            PutOutOrDropDownTimer = 0;
        }

        if (PutOutOrDropDownTimer < PutOutOrDropDownInterwal)
            PutOutOrDropDownTimer += Time.deltaTime;
    }

    public float PutOutOrDropDownTimer = 0;
    public float PutOutOrDropDownInterwal = 0.01f;

    public void SelectItem(int itemSlot)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i != itemSlot)
            {
                Slots[i].SetNormalGUISlot();
                if (Slots[i].MaxCountItems > 0)
                {
                    for (int j = 0; j < Slots[i].MaxCountItems; j++)
                    {
                        if (Slots[i].Items[j] != null)
                        {
                            Slots[i].Items[j].SetActive(false);
                            break;
                        }
                    }
                }
            }
        }

        Slots[itemSlot].SetSelectGUISlot();
        SelectedSlot = itemSlot;
        if (Slots[itemSlot].MaxCountItems > 0)
        {
            for (int i = 0; i < Slots[itemSlot].MaxCountItems; i++)
            {
                if (Slots[itemSlot].Items[i] != null)
                {
                    Slots[itemSlot].Items[i].SetActive(true);
                    SelectedItemInSlot = i;
                    break;
                }
            }
        }
    }

    public void DropDownSelectedItem()
    {
        if (Slots[SelectedSlot].CountItems > 0)
        {
            Slots[SelectedSlot].Items[SelectedItemInSlot].SetActive(true);
            Slots[SelectedSlot].Items[SelectedItemInSlot].GetComponent<ItemObject>().DropItem(this.gameObject);
            if (Slots[SelectedSlot].CountItems > 1)
            {
                Slots[SelectedSlot].Items[SelectedItemInSlot] = null;
            }
            else
            {
                Slots[SelectedSlot].SetGUISlotSprite(EmptySlot);
                Slots[SelectedSlot].Items[SelectedItemInSlot] = null;
                Slots[SelectedSlot].AllowSlot = true;
                Slots[SelectedSlot].ItemsType = null;
            }
            --Slots[SelectedSlot].CountItems;
            Slots[SelectedSlot].UpdateGUISlotCountTextBox();
            SelectItem(SelectedSlot);
        }
    }

    public int GetCountOfItem(string ItemType)
    {
        int sum = 0;
        foreach (var slot in Slots)
        {
            if (slot.ItemsType == ItemType)
            {
                sum += slot.CountItems;
            }
        }
        Debug.Log($"{ItemType}: {sum}");
        return sum;
    }

    /// <summary>
    /// Find slot of the item (not full).
    /// </summary>
    /// <param name="itemType">The type of item.</param>
    /// <returns></returns>
    public EquipmentSlot GetVoidSlotByType(string itemType)
    {
        if (Slots[SelectedSlot].ItemsType == itemType && Slots[SelectedSlot].MaxCountItems > Slots[SelectedSlot].CountItems)
            return Slots[SelectedSlot];

        foreach (var slot in Slots)
        {
            if (slot.ItemsType == itemType && slot.MaxCountItems > slot.CountItems)
                return slot;
        }
        return null;
    }

    /// <summary>
    /// Find slot of the item (not full) or void.
    /// </summary>
    /// <param name="itemType">The type of item.</param>
    public EquipmentSlot GetVoidSlotByTypeOrVoid(string itemType)
    {
        EquipmentSlot a;
        if ((a = GetVoidSlotByType(itemType)) != null)
            return a;

        else
            foreach (var slot in Slots)
            {
                if (slot.AllowSlot)
                    return slot;
            }

        return null;
    }
}
