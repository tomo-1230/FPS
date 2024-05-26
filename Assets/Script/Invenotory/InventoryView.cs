using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private GameObject CloneButton;
    private GameObject content;
    private GameObject InventoryObject;
    private List<GameObject> bur = new List<GameObject>();
    public void Settings(GameObject Button,GameObject Countent, GameObject InvenObj)
    {
        CloneButton = Button;
        content = Countent;
        InventoryObject = InvenObj;
    }
    public void InventoryCreate(List<GameObject> ItemObject)
    {
        if(CloneButton == null || content == null)
        {
            Debug.Log("CreateInventoryError");
            return;
        }
        Item.ItemType Type = Item.ItemType.Gun;
        //Debug.Log(Type);

        while (Type != Item.ItemType.recovery)
        {
            // Debug.Log(Type);
            int i = 0;
            foreach (GameObject a in ItemObject)
            {
                if (a.GetComponent<Item>() == null)
                {
                    Debug.Log("CreateInventoryError");
                    return;
                }

                if (a.GetComponent<Item>().ThisType == Type)
                {
                    GameObject clonebutton = Instantiate(CloneButton);
                    clonebutton.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 250 - (10 * i));
                    clonebutton.transform.parent = content.transform;

                    GameObject CloneText = clonebutton.transform.Find("CloneText").gameObject;
                    CloneText.GetComponent<Text>().text = a.GetComponent<Item>().ItemName;
                    clonebutton.SetActive(true);
                    // Debug.Log(clonebutton);
                    bur.Add(clonebutton);
                }
                i++;
            }
            if (Type == Item.ItemType.Gun)
            {
                Type = Item.ItemType.bullet;
            }
            else if (Type == Item.ItemType.bullet)
            {
                Type = Item.ItemType.recovery;
            }
        }
    }
    public void InventoryDestroy()
    {
        if(InventoryObject == null)
        {
            return;
        }
        if(bur.Count == 0)
        {
            return;
        }
        foreach (GameObject a in bur)
        {
            Destroy(a);
        }
        bur.Clear();
    }
}
