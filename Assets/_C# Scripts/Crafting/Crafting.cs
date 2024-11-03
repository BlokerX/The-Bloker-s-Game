using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public EquipmentManager PlayerEquipmentManager;
    public GameObject Item;
    public int CraftedItemsCount = 1;

    public string[] Items;
    public int[] ItemsMinimumCount;

    public void Craft()
    {
        bool b = true;

        for (int i = 0; i < Items.Length; i++)
        {
            if (PlayerEquipmentManager.GetCountOfItem(Items[i]) < ItemsMinimumCount[i])
            {
                b = false;
            }
        }

        if (b)
        {
            DeleteCraftingComponents();
            for (int i = 0; i < CraftedItemsCount; i++)
            {
                Instantiate(Item, PlayerEquipmentManager.transform.position, Item.transform.rotation, null);
            }
        }
    }

    private void DeleteCraftingComponents()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            int destroyedItemCount = 0;
            foreach (var slot in PlayerEquipmentManager.Slots)
            {
                if (slot.ItemsType == Items[i])
                {
                    for (int j = 0; j < slot.MaxCountItems; j++)
                    {
                        if (slot.Items[j] != null)
                        {
                            slot.DestroyItemAtIndex(j);
                            destroyedItemCount++;
                        }
                        if (destroyedItemCount == ItemsMinimumCount[i])
                        {
                            break;
                        }
                    }

                }
                if (destroyedItemCount == ItemsMinimumCount[i])
                {
                    break;
                }
            }
        }
    }

}
