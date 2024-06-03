using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HaveGun : MonoBehaviour
{
    public GameObject HavePosition;
    public Inventory inventory;
    public Image pointer;

    private GameObject CloneedHaveGun;

    public void settings(GameObject Haveposi,Inventory inve, Image point)
    {
        HavePosition = Haveposi;
        inventory = inve;
        pointer = point;
    }
   public GameObject CloneGun(GameObject CloneObj)
    {
        DestroyGun();
        if(CloneObj!= null)
        {
            Debug.Log("HaveGun.CloneGun:Error");
            return null;
        }
        if(CloneObj.GetComponent<Item>() != null || CloneObj.GetComponent<Item>().ThisType == Item.ItemType.Gun)
        {
            Debug.Log("HaveGun.CloneGun:Error");
            return null;
        }
        Item CloneObjItem = CloneObj.GetComponent<Item>();
        GameObject ClonedObject = Instantiate(CloneObj) ;
        ClonedObject.transform.parent = HavePosition.transform;
        ClonedObject.transform.localPosition = new Vector3(0, 0, 0);
        HavePosition.transform.localPosition = CloneObjItem.HavePosition;
        ClonedObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        Destroy(ClonedObject.GetComponent<BoxCollider>());
        Destroy(ClonedObject.GetComponent<Rigidbody>());
        GunUI(CloneObjItem.Have_cross_hair);
        inventory.ReRoad();
        CloneedHaveGun = ClonedObject;
        return ClonedObject;
    }
    public void DestroyGun()
    {
        if (CloneedHaveGun != null)
        {
            Destroy(CloneedHaveGun);
        }
    }
    public void GunUI(Sprite Reticle)
    {
        pointer.sprite = Reticle;
    }
}
