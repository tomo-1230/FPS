using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClone : MonoBehaviour
{
    public int CloneItem;
    public GameObject ItemPoint;
    public List<GameObject> _Item;
    public List<int> Probability;
    public List<GameObject> Bullet;
    public List<GameObject> Copy;
    public List<int> Number;
    public List<Transform> point;
    // Start is called before the first frame update
    void Start()
    {
        CloneItem = PlayerPrefs.GetInt("CloneItem");
        if(CloneItem == 0)
        {
            return;
        }
        int i = 0;
        foreach(GameObject a in _Item)
        {
            for(int b = 0; b < Probability[i]-1; b++)
            {
                Copy.Add(a);
            }
            i++;
        }
        i = 0;
        foreach(GameObject a in Copy)
        {
            _Item.Add(a);
            Number.Add(i);
        }
        Copy.Clear();
        Transform[] chilleds = new Transform[ItemPoint.transform.childCount];
        //enemy.nav.Warp(chilleds[1].position);
        for (i = 0; i < ItemPoint.transform.childCount; i++)
        {
            chilleds[i] = ItemPoint.transform.GetChild(i);
        }
        foreach(Transform a in chilleds)
        {
            point.Add(a);
        }
        i = 0;
        do
        {
            int CloneObjNumber = RandomNumber(_Item.Count);
            int PositionNumber = RandomNumber(point.Count);
            GameObject CloneObject = Instantiate(_Item[CloneObjNumber]);
            CloneObject.transform.position = point[PositionNumber].position;
            point.RemoveAt(PositionNumber);

            Item item = CloneObject.GetComponent<Item>();
            if(item.ThisType == Item.ItemType.Gun)
            {
                GameObject CloneBulletObj = Instantiate(Bullet[item.BulletType-1]);
                Vector3 position = CloneObject.transform.position;
                position.x += Random.Range(-1f, 1f);
                position.z += Random.Range(-1f, 1f);
                CloneBulletObj.transform.position = position;
            }

            i++;
        } while (i < CloneItem && point.Count > 0);
       
        


    }
    public int RandomNumber(int max)
    {
        return Random.Range(0, max);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
