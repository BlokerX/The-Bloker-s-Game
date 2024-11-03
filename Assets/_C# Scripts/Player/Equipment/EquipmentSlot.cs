using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot
{
    /// <summary>
    /// Create new slot
    /// </summary>
    /// <param name="countItemsInSlot">Count maximum items in slot</param>
    public EquipmentSlot(int countItemsInSlot, GameObject guiSlot)
    {
        Items = new GameObject[countItemsInSlot];
        MaxCountItems = countItemsInSlot;
        GUISlot = guiSlot;
        GUISlotBackgroundImage = guiSlot.GetComponent<Image>();
        GUISlotImage = guiSlot.transform.GetChild(0).GetComponent<Image>();
        GUISlotCountTextBox = guiSlot.transform.GetChild(1).GetComponent<Text>();
    }

    /// <summary>
    /// Przedmioty w slocie
    /// </summary>
    public GameObject[] Items { get; set; }

    /// <summary>
    /// Liczba przedmiotow w slocie
    /// </summary>
    public int CountItems { get; set; } = 0;

    /// <summary>
    /// Maksymalna liczba przedmiotow w slocie
    /// </summary>
    public int MaxCountItems { get; set; }

    // ------------------------------------------------- //

    /// <summary>
    /// Jesli slot pusty "true" a jesli zajety "false"
    /// </summary>
    public bool AllowSlot { get; set; } = true;

    /// <summary>
    /// Zwraca typ przechowywanych przedmiotow, jesli pusty slot to zwraca "null"
    /// </summary>
    public string ItemsType { get; set; } = null;

    /// <summary>
    /// Slot graficzny
    /// </summary>
    public GameObject GUISlot { get; set; }

    /// <summary>
    /// Component Image slotu graficznego
    /// </summary>
    public Image GUISlotBackgroundImage { get; set; }

    /// <summary>
    /// Component Image slotu graficznego
    /// </summary>
    public Image GUISlotImage { get; set; }

    /// <summary>
    /// Component Image slotu graficznego
    /// </summary>
    public Text GUISlotCountTextBox { get; set; }

    public void ResetAndSetMaxCountSlot(int count)
    {
        Items = new GameObject[count];
        CountItems = 0;
        MaxCountItems = count;
        AllowSlot = true;
        ItemsType = null;
        GUISlotCountTextBox.text = null;
    }

    static readonly Color selectedColor = new Color(1, 1, 1, 1);
    static readonly Color normalColor = new Color(0, 0, 0, 100 / 255);
    public void SetNormalGUISlot()
    {
        GUISlotBackgroundImage.color = normalColor;
    }
    public void SetSelectGUISlot()
    {
        GUISlotBackgroundImage.color = selectedColor;
    }

    public void SetGUISlotSprite(Sprite sprite)
    {
        GUISlotImage.sprite = sprite;
    }

    public void UpdateGUISlotCountTextBox()
    {
        if (CountItems > 1)
        {
            GUISlotCountTextBox.text = CountItems.ToString();
        }
        else
        {
            GUISlotCountTextBox.text = "";
        }

        Debug.Log("CountItems: " + CountItems);
    }

    public static Sprite EmptySlotImage = null;

    public void DestroyItemAtIndex(int index)
    {
        Object.Destroy(Items[index]);
        Items[index] = null;
        CountItems--;
        UpdateGUISlotCountTextBox();
        if(CountItems == 0)
        {
            AllowSlot = true;
            GUISlotImage.sprite = EmptySlotImage;
        }
    }

}
