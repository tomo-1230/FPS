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
    [Space(10)]
    [Header("--------------------")]
    [Header("ItemType == Gun のみ")]
    [Header("--------------------")]
    [Space(5)]
    [Header("UI・レティクル系")]
    [Space(1)]
    public Vector3 HavePosition; 
    public Sprite GumImage;
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
    // Start is called before the first frame update
    void Start()
    {
       
        CLoneObject = Player._itemData.ItemObject[CloneObjectNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if(CloneObjectNumber == 5)
        {
            RocketBullet.SetActive(SetBullet != 0);
        }
    }
}
