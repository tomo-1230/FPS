using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{

    private GameObject CameraObject;
    private float maxDistance;
    private Inventory inventory;
    private GameObject History = null;
    public void Settings(GameObject camera, float max, Inventory inven)
    {
        CameraObject = camera;
        maxDistance = max;
        inventory = inven;
    }
    private GameObject Ray()
    {

        Ray ray = new Ray(CameraObject.transform.position, CameraObject.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    public void Take(InventoryData inventoryData)
    {

        GameObject TakeItem = Ray();

        if (TakeItem == null || TakeItem.GetComponent<Item>() == null)
        {
            ShowCanvas();
            return;
        }
        ShowCanvas(TakeItem);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (TakeItem == null)
            {
                Debug.Log("TakeItemError");
                return;
            }
            //1ŒÂ–Ú‚¾‚Á‚½‚ç
            bool HaveGun = false;
            int i = 0;
            foreach (GameObject a in inventoryData.GetObjectList())
            {
                if (a.GetComponent<Item>().ThisType == Item.ItemType.Gun)
                {
                    HaveGun = true;
                    i++;
                }
            }
            inventory.AddItem(TakeItem);
            if (!HaveGun)
            {
                inventory.ReRoad(true, false);
                return;
            }
            //2.3ŒÂ–Ú‚¾‚Á‚½‚ç
            if (i == 1 || i == 2)
            {
                inventory.ReRoad(true, true);
            }
        }
        return;
    }
    public void ShowCanvas(GameObject obj = null)
    {

        if (History != null)
        {
            History.GetComponent<Item>().ShowDescription(false);
        }
        if (obj == null || obj.GetComponent<Item>() == null)
        {
            return;
        }
        obj.GetComponent<Item>().ShowDescription(true);
        History = obj;
    }
}
