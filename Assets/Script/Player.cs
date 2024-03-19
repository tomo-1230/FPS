using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpPower;
    public float CameraSpeed;
    public int IsGround_maxDistance;
    public int TakeItem_maxDistance;
    public GameObject PlayerObject;
    public GameObject CameraObject;
    public GameObject RayPosition;
    public GameObject Player_Mesh;
    public Animator anim;
    public ItemData ItemData;
    public static ItemData _itemData;
    [SerializeField]
    private bool IsGround;
    [Header("UI")]
    public GameObject inventory;
    [Header("inventory")]
    public List<string> ItemName;
    public List<int> ItemCount;
    public List<GameObject> ItemObject;
    public Inventory _Inventory;
    public Gun _gun;
    private bool OpenInventory;
    public static bool HaveGun;
    // Start is called before the first frame update
    void Awake()    {
        _itemData = ItemData;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Camera();
        Contact();
        TakeItem();
        Inventory();
    }
    public void Move()
    {
        Vector3 vector = PlayerObject.transform.position;
        bool run = HaveGun ;
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                vector += PlayerObject.transform.forward * RunSpeed * Time.deltaTime;
                anim.SetBool("move", true);
                anim.SetBool("WS", true);
                anim.SetBool("AD", false);
                anim.SetFloat("Blend", 1f);
                run = false;
            }
            else
            {
                vector += PlayerObject.transform.forward * WalkSpeed * Time.deltaTime;
                anim.SetBool("move", true);
                anim.SetBool("WS", true);
                anim.SetBool("AD", false);
                anim.SetFloat("Blend", 0.5f);
                run = true;
            }
          
        }
        if (HaveGun)
        {
            anim.SetBool("Have", run);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector += PlayerObject.transform.forward * WalkSpeed * Time.deltaTime * -1;
            anim.SetBool("move", true);
            anim.SetBool("WS", true);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0.5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector += PlayerObject.transform.right * WalkSpeed * Time.deltaTime;
            anim.SetBool("move", true);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            vector += PlayerObject.transform.right * WalkSpeed * Time.deltaTime * -1;
            anim.SetBool("move", true);
            anim.SetBool("WS", false);
            anim.SetBool("AD", true);
        }
        if (PlayerObject.transform.position == vector)
        {
            anim.SetBool("move", false);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0f);
        }

        PlayerObject.transform.position = vector;
        if (Input.GetKey(KeyCode.Space) && IsGround)
        {
            PlayerObject.GetComponent<Rigidbody>().velocity = Vector3.up * JumpPower;
            anim.SetBool("Jump", true);
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }
    public void Camera()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        
        Vector3 vector = CameraObject.transform.localEulerAngles;
        vector.y -= y_Rotation;
        CameraObject.transform.localEulerAngles = vector;
        vector = CameraObject.transform.localEulerAngles;
        if (vector.y < 300 && vector.y > 180)
        {
            vector.y = 300;
        }
        if (vector.y > 60 && vector.y < 180)
        {
            vector.y = 60;
           
        }
        Player_Mesh.SetActive(vector.y < 30);
        CameraObject.transform.localEulerAngles = vector;

        vector = PlayerObject.transform.localEulerAngles;
        vector.y += x_Rotation;
        PlayerObject.transform.localEulerAngles = vector;
    }
    public void Contact()
    {
        Ray ray = new Ray(RayPosition.transform.position, RayPosition.transform.forward);
        RaycastHit hit;
      //  Debug.DrawRay(ray.origin, ray.direction * IsGround_maxDistance, Color.red);
        if (Physics.Raycast(ray, out hit, IsGround_maxDistance))
        {
            IsGround = (hit.collider.gameObject.tag == "Ground");
        }
    }
    public void Inventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (OpenInventory)
            {

                inventory.SetActive(false);
                OpenInventory = false;
                _Inventory.DestroyButton();
            }
            else
            {
                inventory.SetActive(true);
                OpenInventory = true;
                _Inventory.CreateInventory();
            }
        }
    }
    public void TakeItem()
    {
        Ray ray = new Ray(CameraObject.transform.position, CameraObject.transform.forward);
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * TakeItem_maxDistance, Color.blue);
        if (Physics.Raycast(ray, out hit, TakeItem_maxDistance))
        {
            GameObject HitObj = hit.collider.gameObject;
            Item item = hit.collider.gameObject.GetComponent<Item>();
            if(hit.collider != null && item != null && HitObj != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                     
                    if(item.ThisType == Item.ItemType.Gun)
                    {
                        AddInventory(HitObj, item.CloneObjectNumber,true);

                    }
                    if(hit.collider.gameObject.GetComponent<Item>().ThisType == Item.ItemType.bullet)
                    {
                        AddInventory(HitObj, item.CloneObjectNumber, true);
                    }
                   
                }
            }
        }
    }
    public void AddInventory(GameObject hit,int CloneObjectNumber, bool delete)
    {
        GameObject CloneObject = _itemData.ItemObject[CloneObjectNumber];

        Item item = CloneObject.GetComponent<Item>();
        if (ItemName.Contains(item.ItemName))
        {
            int ListNumber = ItemName.IndexOf(item.ItemName);
           if(item.ThisType == Item.ItemType.Gun)
           {
                ItemCount[ListNumber] += 1;
           }
           if (item.ThisType == Item.ItemType.bullet)
           {
               ItemCount[ListNumber] += item.count;
           }
        }
        else
        {
            int i = 0;
            foreach(GameObject a in ItemObject)
            {
                if ((item.ThisType == Item.ItemType.Gun || item.ThisType == Item.ItemType.recovery) && (a.GetComponent<Item>().ThisType == Item.ItemType.Gun || a.GetComponent<Item>().ThisType == Item.ItemType.recovery))//’Ç‰Á‚µ‚æ‚¤‚Æ‚µ‚Ä‚¢‚éŽí—Þ‚Æinventory“à‚ÌƒAƒCƒeƒ€‚ÌŽí—Þ‚Æˆê’v‚µ‚½‚ç
                {
                    i++;
                }
            }
           // Debug.Log(i);
            if (item.ThisType == Item.ItemType.Gun && i < 3)
            {
                ItemName.Add(item.ItemName);
                ItemCount.Add(1);
                ItemObject.Add(CloneObject);
                
                if(i == 0)
                {
                    _Inventory.select = 1;
                }
                _Inventory.RoadItem();
                _gun.GanHave();
            }
            if (item.ThisType == Item.ItemType.bullet && i < 3)
            {
                ItemName.Add(item.ItemName);
                ItemCount.Add(item.count);
                ItemObject.Add(CloneObject);

                if (i == 0)
                {
                    _Inventory.select = 1;
                }
                _Inventory.RoadItem();
            }
            else
                return;

          
        }
        if (delete && hit != null)
        {
            Destroy(hit);
        }
    }
   
  
}
