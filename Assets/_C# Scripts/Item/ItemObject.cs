using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    // Zmienne dostepne dla wszystkich obiektow tej klasy
    protected static int nextID = 1;

    // Właściwości przedmiotu
    public readonly int ID;
    public ItemType TypeOfItem = ItemType.Item;
    public string Name = "Item";
    public string Describe = "This is only item";
    public Sprite ItemSprite;
    [Range(1, 64)] public int MaxInStack = 64;

    public Vector3 RotationInHand = new Vector3(0, 0, 0);
    private Quaternion NormalRotation;

    public Vector3 ScaleInHand = new Vector3(1, 1, 1);
    private Vector3 NormalScale = new Vector3(1, 1, 1);

    // Inputs:
    public KeyCode PickUp_KeyCode = KeyCode.E;

    // Const:
    public const string PlayerColliderTag = "PlayerCamera";

    public bool HaveOvner { get; set; } = false;

    public bool IsPlayerNear { get; private set; } = false;

    public bool IsLockForUse = false;

    public GUISkin HelpMessageSkin;

    // Delegat funkcji kolizyjnych
    public delegate void ItemUserFunctionColission(Collider collider, EquipmentManager equipmentManager);

    // Funkcje:
    public ItemUserFunctionColission MouseLeftButton_GetKeyDown;
    public ItemUserFunctionColission MouseLeftButton_GetKey;

    private Rigidbody ThisRigidbody;

    /// <summary>
    /// Default constructor for ItemObject.
    /// </summary>
    public ItemObject()
    {
        // Nadawanie nowego id do przedmiotu
        ID = nextID++;
    }

    /// <summary>
    /// It works when script is loading.
    /// </summary>
    private void Awake()
    {
        if (!this.gameObject.TryGetComponent<Rigidbody>(out ThisRigidbody))
        {
            ThisRigidbody = this.gameObject.AddComponent<Rigidbody>();
            ThisRigidbody.mass = 10;
        }
        ThisRigidbody.useGravity = true;
        ThisRigidbody.detectCollisions = true;
        ThisRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag) && !HaveOvner)
        {
            IsPlayerNear = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag) && !HaveOvner)
        {
            if (Input.GetKeyDown(PickUp_KeyCode))
            {
                PickUpItem(collider.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag(PlayerColliderTag) && !HaveOvner)
        {
            IsPlayerNear = false;
        }
    }

    /// <summary>
    /// The method draws 2d elements on the GUI.
    /// </summary>
    private void OnGUI()
    {
        if (IsPlayerNear && !HaveOvner)
        {
            if (HelpMessageSkin != null) GUI.skin = HelpMessageSkin;
            Rect rect = new Rect(25, Screen.height - 25 - 110, 420, 110);
            GUI.Box(rect, $"Press [{PickUp_KeyCode}]\n to pick up {Name}");
        }
    }

    /// <summary>
    /// On drop down an item.
    /// </summary>
    /// <param name="sender">The new parent game object.</param>
    public void DropItem(GameObject sender)
    {
        ThisRigidbody.isKinematic = false;
        ThisRigidbody.detectCollisions = true;
        ThisRigidbody.useGravity = true;
        this.transform.parent = sender.transform;
        this.gameObject.GetComponent<Transform>().localScale = NormalScale;
        this.gameObject.GetComponent<Transform>().localRotation = NormalRotation;
        this.transform.localPosition = new Vector3(0, 0, 0.75f);
        this.transform.parent = null;
        HaveOvner = false;
        IsPlayerNear = false;
    }

    /// <summary>
    /// On pick up an item.
    /// </summary>
    /// <param name="sender">Player.</param>
    public void PickUpItem(GameObject sender)
    {
        EquipmentManager playerEquipmentManager = sender.GetComponent<PlayerCameraComponent>().PlayerEquipmentManager;
        var slot = playerEquipmentManager.GetVoidSlotByTypeOrVoid(Name);
        if (playerEquipmentManager.PutOutOrDropDownTimer > playerEquipmentManager.PutOutOrDropDownInterwal)
        {
            if (slot?.AllowSlot == true)
            {
                playerEquipmentManager.PutOutOrDropDownTimer = 0;
                ThisRigidbody.useGravity = false;
                ThisRigidbody.detectCollisions = false;
                ThisRigidbody.isKinematic = true;
                slot.ResetAndSetMaxCountSlot(MaxInStack);
                ++slot.CountItems;
                slot.UpdateGUISlotCountTextBox();
                slot.AllowSlot = false;
                slot.ItemsType = this.Name;
                slot.Items[0] = this.gameObject;
                HaveOvner = true;

                var ThisObjectTransform = this.gameObject.GetComponent<Transform>();
                this.transform.parent = playerEquipmentManager.gameObject.GetComponent<Transform>();
                this.transform.parent = playerEquipmentManager.Hand[0].GetComponent<Transform>();
                ThisObjectTransform.localScale = ScaleInHand;
                ThisObjectTransform.localPosition = new Vector3(0, 0, 0);
                ThisObjectTransform.localRotation = Quaternion.Euler(RotationInHand);

                if (this.ItemSprite != null)
                    slot.SetGUISlotSprite(ItemSprite);
            }
            else if (slot?.ItemsType != null)
            {
                playerEquipmentManager.PutOutOrDropDownTimer = 0;
                ThisRigidbody.useGravity = false;
                ThisRigidbody.detectCollisions = false;
                ThisRigidbody.isKinematic = true;
                for (int i = 0; i < slot.MaxCountItems; i++)
                {
                    if (slot.Items[i] == null)
                    {
                        ++slot.CountItems;
                        slot.UpdateGUISlotCountTextBox();
                        slot.Items[i] = this.gameObject;
                        this.transform.parent = playerEquipmentManager.gameObject.GetComponent<Transform>();

                        var ThisTransform = this.gameObject.GetComponent<Transform>();
                        this.transform.parent = playerEquipmentManager.Hand[0].GetComponent<Transform>();
                        ThisTransform.localScale = ScaleInHand;
                        ThisTransform.localPosition = new Vector3(0, 0, 0);
                        ThisTransform.localRotation = Quaternion.Euler(RotationInHand);

                        this.gameObject.SetActive(false);
                        break;
                    }
                }
            }
            playerEquipmentManager.SelectItem(playerEquipmentManager.SelectedSlot);
        }
    }

    /// <summary>
    /// It works just before first Update.
    /// </summary>
    void Start()
    {
        var ThisTransform = this.gameObject.GetComponent<Transform>();
        NormalRotation = ThisTransform.localRotation;
        NormalScale = ThisTransform.localScale;
    }

    /// <summary>
    /// To change item sprite in game time.
    /// </summary>
    /// <param name="sprite">New sprite.</param>
    public void ChangeItemSprite(Sprite sprite)
    {
        ItemSprite = sprite;
    }

    /// <summary>
    /// Types of items.
    /// </summary>
    public enum ItemType
    {
        Item,
        Tool,
        Weapon,
        Armor,
        Food,
        Bow,
        Gun,
        ToolWeapon
    }

}
