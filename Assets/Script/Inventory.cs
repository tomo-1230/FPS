using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Player player;
    public GameObject CloneButton;
    public GameObject content;
    public int select;
    [HideInInspector]
    public List<GameObject> bur;
    [Header("UI")]
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public Image ImageGun;
    public List<GameObject> search;
    // Start is called before the first frame update
    void Start()
    {
        select = 0;
        RoadItem();
    }

    // Update is called once per frame
    void Update()
    {

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
        }
        if(select == 1)
        {
            panel1.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            panel2.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel3.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            ImageGun.sprite = search[0].GetComponent<Item>().GumImage;
        }
        if (select == 2)
        {
            panel1.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel2.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            panel3.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            ImageGun.sprite = search[1].GetComponent<Item>().GumImage;
        }
        if (select == 3)
        {
            panel1.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel2.GetComponent<Image>().color = new Color32(255, 255, 255, 101);
            panel3.GetComponent<Image>().color = new Color32(0, 0, 0, 101);
            ImageGun.sprite = search[2].GetComponent<Item>().GumImage;
        }

    }

    public void CreateInventory()
    {

        Item.ItemType  Type = Item.ItemType.Gun;
        //Debug.Log(Type);

        while (Type != Item.ItemType.recovery)
        {
           // Debug.Log(Type);
            int i = 0;
            foreach (GameObject a in player.ItemObject)
            {
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
    public void DestroyButton()
    {
        foreach(GameObject a in bur)
        {
            Destroy(a);
        }
        bur.Clear();
    }
}
