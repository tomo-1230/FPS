using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject CloneButton;
    public GameObject content;
    public GameObject CameraObject;
    public  float RayDistance;
    public int select;
    public ItemData itemdata;
    private bool OpenInventory = false;
    public GameObject InventoryObject;
    public Gun gun;
    public  GunUIObj gunUIObj;
    private InventoryView inventoryView;
    public InventoryData inventoryData;
    private TakeItem takeItem;
    private ItemView itemView;
    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        inventoryView = this.gameObject.AddComponent<InventoryView>();
        inventoryData = this.gameObject.AddComponent<InventoryData>();
        takeItem = this.gameObject.AddComponent<TakeItem>();
        itemView = this.gameObject.AddComponent<ItemView>();
        inventoryView.Settings(CloneButton, content, InventoryObject);
        inventoryData.setting(itemdata);
        takeItem.Settings(CameraObject, RayDistance, this.gameObject.GetComponent<Inventory>());
        itemView.setting(gunUIObj, this.gameObject.GetComponent<Inventory>());

        ReRoad(true,false);
    }

    // Update is called once per frame
    void Update()
    {
        TakeItem();
        UI();
    }
    public void UI()
    {
        itemView.select();

    }
    public void ReRoad(bool a = false, bool IsClone = false)
    {
        if (!a)
        {
            return;
        }
        if (itemView == null || inventoryData == null)
        {
            Debug.Log("a");
            return;
        }
        select = itemView.GetSelect();
        itemView.ItemSearch(inventoryData);
        if (itemView.GetList()[0] != null)
        {
            gun.GanHave(itemView.GetList(), IsClone);
        }
        
        itemView.ReRoad(inventoryData, gun.setBulletData, gun.CloneGun);
    }
    public void TakeItem()
    {
        takeItem.Take(inventoryData);
        InventoryView();

    }
    public void AddItem(GameObject hit)
    {
        inventoryData.AddItem(hit, true);
    }
    public void RemoveItem(int index, int count)
    {
        inventoryData.RemoveItem(index, count);
    }
    public void InventoryView()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (OpenInventory)
            {
                OpenInventory = false;
                inventoryView.InventoryDestroy();
            }
            else
            {
                OpenInventory = true;
                inventoryView.InventoryCreate(inventoryData.GetObjectList());
            }
        }
    }
}
