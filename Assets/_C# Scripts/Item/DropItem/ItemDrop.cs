using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject DropObject;
    public Vector3 SpawnPointForDropObject = new Vector3(0, 0, 0);
    public int Strength = 1;
    public GameObject ReturnObject;
    public float ExtractingSpeedInSecondsForOneDropItem = 1f;
    public bool InExtract = false;
    float extractingTimer = 0;

    public int MaxStrenght { get; private set; }

    private void Awake()
    {
        MaxStrenght = Strength;
        ExtractingCleanerInterval = ExtractingSpeedInSecondsForOneDropItem / 10;
    }

    public void DropItem()
    {
        if (Strength > 0)
        {
            Strength--;
            var go = SpawnDropObject();
            go.transform.parent = null;
        }
        else
        {
            SpawnReturnObject();
            Destroy(this.gameObject);
        }
    }

    private void SpawnReturnObject()
    {
        if (ReturnObject != null)
        {
            Strength = MaxStrenght;
            GameObject g = Instantiate(ReturnObject, this.transform.position, ReturnObject.transform.rotation, this.transform.parent);

            g.SetActive(true);
        }
    }

    private GameObject SpawnDropObject()
    {
        return Instantiate(DropObject, this.transform.position + SpawnPointForDropObject, DropObject.transform.rotation, null);
    }

    private float ExtractingCleanerTimer = 0;
    private float ExtractingCleanerInterval;
    private void Update()
    {
        if (InExtract)
        {
            if (extractingTimer < ExtractingSpeedInSecondsForOneDropItem)
            {
                extractingTimer += Time.deltaTime;
            }
            else if (extractingTimer >= ExtractingSpeedInSecondsForOneDropItem)
            {
                DropItem();
                extractingTimer = 0;
            }
        }

        ExtractingCleanerTimer += Time.deltaTime;
        if (ExtractingCleanerTimer >= ExtractingCleanerInterval)
        {
            InExtract = false;
            ExtractingCleanerTimer = 0;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent<ItemExtractor>(out ItemExtractor ie1) && ie1.IsCompareTag(this.gameObject))
        {
            InExtract = false;
            extractingTimer = 0;
        }
    }

}
