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

    private InventoryView inventoryView;
    private InventoryData inventoryData;
    private TakeItem takeItem;
    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        RoadItem();
      
        
        inventoryView = this.gameObject.AddComponent<InventoryView>();
        inventoryData = this.gameObject.AddComponent<InventoryData>();
        takeItem = this.gameObject.AddComponent<TakeItem>();
        inventoryView.Settings(CloneButton, content, InventoryObject) ;
        inventoryData.setting(itemdata);
        takeItem.Settings(CameraObject, RayDistance,this.gameObject.GetComponent<Inventory>());
    }

    // Update is called once per frame
    void Update()
    {
        TakeItem();
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
    public void RoadItem()
    {
        search.Add(null); 
        search.Add(null); 
        search.Add(null);
        int i = 0;
        foreach(GameObject a in player.ItemObject)
        {
            if(a.GetComponent<Item>().ThisType == Item.ItemType.Gun)
            {
                search[i] = a;
                i++;
            }
        }
        //Debug.Log(search[0]);
        if (search[0] != null)
        {
            panel1.SetActive(true);
            panel1.transform.Find("GunName").gameObject.GetComponent<Text>().text = search[0].GetComponent<Item>().ItemName.ToString();
            if(search[1] != null)
            {
                panel2.SetActive(true);
                panel2.transform.Find("GunName").gameObject.GetComponent<Text>().text = search[1].GetComponent<Item>().ItemName.ToString();
                if (search[2] != null)
                {
                    panel3.SetActive(true);
                    panel3.transform.Find("GunName").gameObject.GetComponent<Text>().text = search[2].GetComponent<Item>().ItemName.ToString();
                }
                else
                    panel3.SetActive(false);
            }
            else
            {
                panel2.SetActive(false);
                panel3.SetActive(false);
            }
        }
        else
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
        }


        if(select == 0)
        {
            panel1.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel2.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel3.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel.SetActive(false);
        }
        if(select == 1)
        {
            panel1.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            panel2.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel3.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            ImageGun.sprite = search[0].GetComponent<Item>().GumImage;
            ImageBullet.sprite = search[0].GetComponent<Item>().BulletImage;
        }
        if (select == 2)
        {
            panel1.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel2.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            panel3.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            ImageGun.sprite = search[1].GetComponent<Item>().GumImage;
            ImageBullet.sprite = search[1].GetComponent<Item>().BulletImage;
        }
        if (select == 3)
        {
            panel1.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel2.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel3.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            ImageGun.sprite = search[2].GetComponent<Item>().GumImage;
            ImageBullet.sprite = search[1].GetComponent<Item>().BulletImage;
        }

        //’e‚ÌŽc—Ê•\Ž¦

        if(select == 0 || gun.Clone_HaveGun == null)
        {
            panel.SetActive(false);
            return;
        }
        else
        {
            panel.SetActive(true);
        }

        //SetBullet
        int SetBullet = gun.Clone_HaveGun.GetComponent<Item>().SetBullet;

        if(SetBullet < 10)
        {
            BulletSet.text = "0" + SetBullet.ToString();
        }
        else
        {
            BulletSet.text = SetBullet.ToString();
        }

        int HaveBullet = 0;
        int value = -1;
        int index = -1;
        foreach (GameObject a in player.ItemObject)
        {
            value++;
            Item item = a.GetComponent<Item>();
            if (item == null)
            {
                return;
            }
            
            if(item.ThisType == Item.ItemType.bullet)
            {
                if(item.ThisBulletType == gun.Clone_HaveGun.GetComponent<Item>().BulletType)
                {
                    index = value;
                   // Debug.Log("a");
                }
            }
        }
        //Debug.Log(index);
        if(index != -1 && player.ItemCount[index] > 0 )
        {
            HaveBullet = player.ItemCount[index];
        }
        else
        {
            HaveBullet = 0;
        }
        

        if (HaveBullet < 10)
        {
            BulletHave.text = "0" + HaveBullet.ToString();
        }
        else
        {
            BulletHave.text = HaveBullet.ToString();
        }
       

    }

    public void CreateInventory()
    {

        
       
    }
    public void DestroyButton()
    {
        
    }
}
