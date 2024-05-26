using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{

    private GameObject CameraObject;
    private float maxDistance;
    private Inventory inventory;
    private GameObject History = null;
    public void Settings(GameObject camera, float max,Inventory inven)
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
        if(Physics.Raycast(ray, out hit, maxDistance))
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    public void Take()
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
            if(TakeItem == null)
            {
                Debug.Log("TakeItemError");
                return;
            }
            inventory.AddItem(TakeItem);
        }
        return ;
    }
    public void ShowCanvas(GameObject obj = null)
    {
        
        if(History != null)
        {
            History.GetComponent<Item>().ShowDescription(false);
        }
        if(obj == null || obj.GetComponent<Item>() == null)
        {
            return;
        }
        obj.GetComponent<Item>().ShowDescription(true);
        History = obj;
    }
}
