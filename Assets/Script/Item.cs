using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    [Header("--------------------")]
    [Header("すべて入力")]
    [Header("--------------------")]
    [Space(5)]
    public string ItemName;
    public enum ItemType
    {
        Gun, bullet, recovery
    }
    public ItemType ThisType;
    public int CloneObjectNumber;
    public bool ShowCanvas;
    [Space(10)]
    [Header("--------------------")]
    [Header("ItemType == Gun のみ")]
    [Header("--------------------")]
    [Space(5)]
    [Header("UI・レティクル系")]
    [Space(1)]
    public Vector3 HavePosition; 
    public Sprite GumImage;
    public Sprite BulletImage;
    public Sprite Have_cross_hair;
    public Sprite Set_cross_hair;
    public int ZoomValue;
    [Space(3)]
    [Header("リロード")]
    [Space(1)]
    public int SetBullet;
    public int MaxBullet;
    public int ReRoadTiem;
    public float TimeTweak;
    [Space(3)]
    [Header("弾オブジェクト")]
    [Space(1)]
    public GameObject BulletObj;
    public GameObject MuzzleObj;
    public int BulletType;
    [Space(3)]
    [Header("距離・ダメージ・クールタイム")]
    [Space(1)]
    public int distance;
    public int Damage;
    public int HeadDamage;
    public int FiringInterval;
    public bool RapidFire;
    [Space(3)]
    [Header("その他")]
    [Space(1)]
    public float ShotAim;
    public int ShotAmount;
    public GameObject RocketBullet;
    [Space(10)]
    [Header("--------------------")]
    [Header("ItemType == bullet のみ")]
    [Header("--------------------")]
    [Space(5)]
    public int ThisBulletType;
    public int count;
    public GameObject Firing_Bullet;
    [HideInInspector]
    public GameObject CLoneObject;
    [Space(10)]
    [Header("--------------------")]
    [Header("必要に応じて")]
    [Header("--------------------")]
    [Space(5)]
    [Header("UI")]
    [Space(1)]
    public GameObject ItemCanvas;
    public Text TextName;
    public Text TextBullet;
    public Image ImageBullet;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
       
        CLoneObject = Player._itemData.ItemObject[CloneObjectNumber]; ShowDescription(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(CloneObjectNumber == 5)
        {
            RocketBullet.SetActive(SetBullet != 0);
        }
       
    }
    public void ShowDescription(bool a)
    {
        if (!ShowCanvas)
        {
            return;
        }
        ItemCanvas.SetActive(a);
        if (a)
        {
            TextName.text = ItemName;
            int set = SetBullet;
            int max = MaxBullet;
            if(set < 10) 
            {
                TextBullet.text = "0"+set + "/";
            }
            else
            {
                TextBullet.text =  set + "/";
            }
            if(max < 10)
            {
                TextBullet.text = TextBullet.text +"0"+ max;
            }
            else
            {
                TextBullet.text = TextBullet.text + max;
            }
        }
    }
}
