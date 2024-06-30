using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GunReRoad : MonoBehaviour
{
    public bool Reloading;
    public GameObject CircleGauge;
    public Animator anim;
    public Inventory inventory;
    public void settings(GameObject circle, Animator ani, Inventory inve = null)
    {
        CircleGauge = circle;
        anim = ani;
        inventory = inve;
        Reloading = false;
    }
    public async void ReRoad(GameObject CloneGun, InventoryData inventoryData)
    {
        if (!Input.GetKeyDown(KeyCode.R))
        {
            return;
        }
        Reloading = true;
        if (!Check(CloneGun, inventoryData))
        {
            Reloading = false;
            return;
        }
        int BulletValue = Calculation(CloneGun, inventoryData);
        Anim(CloneGun, anim, true);
        await Task.Delay(CloneGun.GetComponent<Item>().ReRoadTiem);
        Anim(CloneGun, anim, false);
        Assignment(CloneGun, BulletValue);
        if (inventory != null)
        {
            int index = IntBulletSearch(CloneGun, inventoryData);
            inventory.RemoveItem(index, BulletValue);
            inventory.ReRoad(true, true);
        }
        Reloading = false;
    }
    public bool Check(GameObject CloneGun, InventoryData inventoryData)
    {
        if (!Reloading)
        {
            return false;
        }
        if (CloneGun == null || CloneGun.GetComponent<Item>() == null)
        {
            return false;
        }
        Item item = CloneGun.GetComponent<Item>();
        if (item.SetBullet >= item.MaxBullet)
        {
            return false;
        }
        bool existence = BoolBulletSearch(CloneGun, inventoryData);

        if (!existence)
        {
            Debug.Log("Not existence");
            return false;
        }

        return true;
    }
    public int Calculation(GameObject CloneGun, InventoryData inventoryData)
    {
        int value = IntBulletSearch(CloneGun, inventoryData);
        Item item = CloneGun.GetComponent<Item>();
        int Havebullet = inventoryData.GetCountList()[value];
        int consumableBullets = 0;
        int difference = item.MaxBullet - item.SetBullet;
        if (difference <= Havebullet)
        {
            consumableBullets = difference;
        }
        else
        {
            consumableBullets = Havebullet;
        }
        return consumableBullets;
    }
    public void Assignment(GameObject CloneGun, int consumableBullets)
    {
        if(CloneGun == null)
        {
            return;
        }
        Item item = CloneGun.GetComponent<Item>();
        item.SetBullet += consumableBullets;
    }
    public void Anim(GameObject CloneGun, Animator anim, bool a)
    {
        if(CloneGun == null)
        {
            return;
        }
        if (CircleGauge != null)
        {
            CircleGauge.SetActive(true);
        }
        Item item = CloneGun.GetComponent<Item>();
        if (a)
        {
            anim.SetBool("reload", true);
            anim.SetFloat("speed", item.ReRoadTiem / 100 * item.TimeTweak);
        }
        else
        {
            anim.SetBool("reload", false);
            anim.SetBool("reload", false);
        }
        if (CircleGauge != null)
        {
            CircleGauge.SetActive(false);
        }
    }
    public bool BoolBulletSearch(GameObject CloneGun, InventoryData inventoryData)
    {
        bool Check = false;
        foreach (GameObject a in inventoryData.GetObjectList())
        {
            Item CloneGunItem = a.GetComponent<Item>();
            if (CloneGunItem == null)
            {
                continue;
            }

            if (CloneGunItem.ThisBulletType != CloneGun.GetComponent<Item>().BulletType)
            {
                continue;
            }
            else if (CloneGunItem.ThisType != Item.ItemType.bullet)
            {
                continue;
            }
            else
            {
                Check = true;
                break;
            }
        }
        return Check;
    }
    public int IntBulletSearch(GameObject CloneGun, InventoryData inventoryData)
    {
        int index = 0;
        foreach (GameObject a in inventoryData.GetObjectList())
        {
            Item CloneGunItem = a.GetComponent<Item>();
            if (CloneGunItem == null)
            {
                continue;
            }

            if (CloneGunItem.ThisBulletType != CloneGun.GetComponent<Item>().BulletType)
            {
                index++;
                continue;
            }
            else if (CloneGunItem.ThisType != Item.ItemType.bullet)
            {
                index++;
                continue;
            }
            else
            {
                break;
            }
        }
        return index;
    }
}

