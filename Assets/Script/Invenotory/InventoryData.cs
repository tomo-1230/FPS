using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InventoryData : MonoBehaviour
{
    private List<string> ItemName = new List<string>();
    private List<int> ItemCount = new List<int>();
    private List<GameObject> ItemObject = new List<GameObject>();
    private List<Item> ItemData = new List<Item>();
    private ItemData itemdata;
    public void setting(ItemData _itemdata)
    {
        itemdata = _itemdata;
    }
    public void AddItem(GameObject hit, bool delete)
    {
        if (hit == null)
        {
            return;
        }
        Item HitItem = hit.GetComponent<Item>();
        int CloneObjectNumber = HitItem.CloneObjectNumber;
        GameObject CloneObject = itemdata.ItemObject[CloneObjectNumber];
        bool IsItemExists = ItemName.Contains(HitItem.ItemName);
        if (IsItemExists)
        {
            int ListNumber = ItemName.IndexOf(HitItem.ItemName);
            if (HitItem.ThisType == Item.ItemType.Gun)
            {
                ItemName.Add(HitItem.ItemName);
                ItemCount.Add(1);
                ItemObject.Add(CloneObject);
                ItemData.Add(HitItem.GetComponent<Item>());
            }
            if (HitItem.ThisType == Item.ItemType.bullet)
            {
                ItemCount[ListNumber] += HitItem.count;
            }
        }
        else
        {
            if (HitItem.ThisType == Item.ItemType.Gun)
            {
                ItemCount.Add(1);
            }
            if (HitItem.ThisType == Item.ItemType.bullet)
            {
                ItemCount.Add(HitItem.count);
            }
            ItemName.Add(HitItem.ItemName);
            ItemObject.Add(CloneObject);
            ItemData.Add(HitItem.GetComponent<Item>());

        }
        if (delete)
        {
            Destroy(hit);
        }
    }
    public void RemoveItem(int index, int count)
    {
        ItemCount[index] -= count;
        if (ItemCount[index] <= 0)
        {
            ItemName.RemoveAt(index);
            ItemCount.RemoveAt(index);
            ItemObject.RemoveAt(index);
            ItemData.RemoveAt(index);
        }
    }
    public List<string> GetNameList()
    {
        return ItemName;
    }
    public List<int> GetCountList()
    {
        return ItemCount;
    }
    public List<GameObject> GetObjectList()
    {
        return ItemObject;
    }
    public List<Item> GetItemComponentList()
    {
        return ItemData;
    }
}
