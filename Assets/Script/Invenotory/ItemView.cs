using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    private int SelectGun = 0;
    public List<GameObject> search = new List<GameObject>();
    public List<int> SetBulletList = new List<int>();
    private GunUIObj GunUIObj;
    private Inventory inventory;
    public void setting(GunUIObj gunUIObj,Inventory inve)
    {
        GunUIObj = gunUIObj;
        inventory = inve;
    }
    public void ReRoad(InventoryData inventoryData)
    {
        
        search.Clear();
        search.Add(null);
        search.Add(null);
        search.Add(null);
        if(inventoryData.GetNameList().Count == 0)
        {
            return;
        }
        int i = 0;
        foreach (GameObject a in inventoryData.GetObjectList())
        {
            if (a.GetComponent<Item>().ThisType == Item.ItemType.Gun)
            {
                search[i] = a;
                SetBulletList.Add(a.GetComponent<Item>().SetBullet);
                i++;
            }
            if(i >= 3)
            {
                break;
            }
        }
        if (search[0] != null)
        {
            GunUIObj.panel.SetActive(true);
        }
        else
        {
            return;
        }
        UnderGunDisplay();
        UnderPanelColor();
        TextView(SelectGun-1, inventoryData);
    }
    public void  UnderGunDisplay()
    {
        if (search[0] == null)
        {
            return;
        }
        GunUIObj.panel1.SetActive(true);
        Text panel1Text = GunUIObj.panel1.transform.Find("GunName").gameObject.GetComponent<Text>();
        panel1Text.text = search[0].GetComponent<Item>().ItemName.ToString();
        if (search[1] == null)
        {
            GunUIObj.panel2.SetActive(false);
            GunUIObj.panel3.SetActive(false);
            return;
        }
        GunUIObj.panel2.SetActive(true);
        Text panel2Text = GunUIObj.panel2.transform.Find("GunName").gameObject.GetComponent<Text>();
        panel2Text.text = search[1].GetComponent<Item>().ItemName.ToString();
        if (search[2] == null)
        {
            GunUIObj.panel3.SetActive(false);
            return;
        }
        GunUIObj.panel3.SetActive(true);
        Text panel3Text = GunUIObj.panel3.transform.Find("GunName").gameObject.GetComponent<Text>();
        panel3Text.text = search[2].GetComponent<Item>().ItemName.ToString();
    }
    public void UnderPanelColor()
    {
        GunUIObj.panel1.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
        GunUIObj.panel2.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
        GunUIObj.panel3.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
        if (SelectGun == 0)
        {
            GunUIObj.panel.SetActive(false);
            return;
        }
        if (SelectGun == 1 && search[0] != null)
        {
            GunUIObj. panel1.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            GunUIObj.ImageGun.sprite = search[0].GetComponent<Item>().GumImage;
            GunUIObj.ImageBullet.sprite = search[0].GetComponent<Item>().BulletImage;
            return;
        }
        if (SelectGun == 2 && search[1] != null)
        {
            GunUIObj.panel2.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            GunUIObj.ImageGun.sprite = search[1].GetComponent<Item>().GumImage;
            GunUIObj.ImageBullet.sprite = search[1].GetComponent<Item>().BulletImage;
            return;
        }
        if (SelectGun == 3 && search[2] != null)
        {
            GunUIObj.panel3.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            GunUIObj.ImageGun.sprite = search[2].GetComponent<Item>().GumImage;
            GunUIObj.ImageBullet.sprite = search[1].GetComponent<Item>().BulletImage;
            return;
        }
        SelectGun --;
        UnderPanelColor();
    }
    public void TextView(int index,InventoryData inventoryData)
    {
        //取得
        int setBulletValue = SetBulletList[index];
        Item ItemData = search[index].GetComponent<Item>();
        int HaveBulletvalue = 0;
        string HaveBulletText;
        string SetBulletText;
        int i = 0;
        foreach(GameObject a in inventoryData.GetObjectList())
        {
            if(a.GetComponent<Item>().ThisBulletType == ItemData.BulletType)
            {
                List<int> count = inventoryData.GetCountList();
                HaveBulletvalue = count[i];
            }
            i++;
        }
        
        //編集
        if(setBulletValue <10)
        {
            SetBulletText = "0" + setBulletValue.ToString();
        }
        else
        {
            SetBulletText = setBulletValue.ToString();
        }
        if (HaveBulletvalue < 10)
        {
            HaveBulletText = "0" + HaveBulletvalue.ToString();
        }
        else
        {
            HaveBulletText = HaveBulletvalue.ToString();
        }
        //代入
        GunUIObj.BulletSet.text = SetBulletText;
        GunUIObj.BulletHave.text = HaveBulletText;
    }
    public void select()//常時
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectGun = 1;
            inventory.ReRoad();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectGun = 2;
            inventory.ReRoad();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectGun = 3;
            inventory.ReRoad();
        }
        inventory.select = SelectGun;
    }
   
}
