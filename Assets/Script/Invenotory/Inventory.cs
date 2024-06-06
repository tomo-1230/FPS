using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;
    public GameObject CloneButton;
    public GameObject content;
    public GameObject CameraObject;
    public float RayDistance;
    public int select;
    public ItemData itemdata;
    private bool OpenInventory = false;
    public GameObject InventoryObject;
    [Header("UI")]
    public GameObject panel;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public Text BulletSet;
    public Text BulletHave;
    public Image ImageGun;
    public Image ImageBullet;
    public List<GameObject> search;
    public Gun gun;
    public GunUIObj gunUIObj;
    private InventoryView inventoryView;
    private InventoryData inventoryData;
    private TakeItem takeItem;
    private ItemView itemView;
    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        ReRoad();


        inventoryView = this.gameObject.AddComponent<InventoryView>();
        inventoryData = this.gameObject.AddComponent<InventoryData>();
        takeItem = this.gameObject.AddComponent<TakeItem>();
        itemView = this.gameObject.AddComponent<ItemView>();
        inventoryView.Settings(CloneButton, content, InventoryObject);
        inventoryData.setting(itemdata);
        takeItem.Settings(CameraObject, RayDistance, this.gameObject.GetComponent<Inventory>());
        itemView.setting(gunUIObj,this.gameObject.GetComponent<Inventory>());
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
    public void ReRoad(bool a = false,bool IsClone = false)
    {
        if (!a)
        {
            return;
        }
       
        if(itemView == null || inventoryData == null)
        {
            return;
        }
        select = itemView.GetSelect();
        itemView.ReRoad(inventoryData);
        gun.GanHave(itemView.GetList(),IsClone);
    }
    public void TakeItem()
    {
        takeItem.Take();
        InventoryView();

    }
    public void AddItem(GameObject hit)
    {
        inventoryData.AddItem(hit, true);
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
    

    public void CreateInventory()
    {



    }
    public void DestroyButton()
    {

    }
}
