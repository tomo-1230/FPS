using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpPower;
    public float CameraSpeed;
    public float CameraSpeedNormal;
    public float CameraSpeedZoom_Long;
    public float CameraSpeedZoom_Short;
    public float CameraSpeedZoom_Moderate;
    public float IsGround_maxDistance;
    public int TakeItem_maxDistance;
    public GameObject PlayerObject;
    public GameObject CameraObject;
    public GameObject RayPosition;
    public GameObject Player_Mesh;
    public Animator PlayerAnim;
    public Animator GageAnim;
    public ItemData ItemData;
    public static ItemData _itemData;
    [SerializeField]
    public bool IsGround;
    [Header("UI")]
    public GameObject inventory;
    [Header("inventory")]
    public List<string> ItemName;
    public List<int> ItemCount;
    public List<GameObject> ItemObject;
    public Inventory _Inventory;
    public HP _hp;
    public int PlayerHP = 100;
    public Gun _gun;
    private bool OpenInventory;
    public static bool HaveGun;
    private Item a;
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
        _hp.PlayerHP(PlayerHP);

        if (Input.GetKey(KeyCode.Y))
        {
            _Inventory.RoadItem();
        }
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
                PlayerAnim.SetBool("move", true);
                PlayerAnim.SetBool("WS", true);
                PlayerAnim.SetBool("AD", false);
                PlayerAnim.SetFloat("Blend", 1f);
                PlayerAnim.SetLayerWeight(2, 0f);
                run = false;
            }
            else
            {
                vector += PlayerObject.transform.forward * WalkSpeed * Time.deltaTime;
                PlayerAnim.SetBool("move", true);
                PlayerAnim.SetBool("WS", true);
                PlayerAnim.SetBool("AD", false);
                PlayerAnim.SetFloat("Blend", 0.5f);
                PlayerAnim.SetLayerWeight(2, 1f);
                run = true;
            }
          
        }
        if (HaveGun)
        {
            PlayerAnim.SetBool("Have", run);
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector += PlayerObject.transform.forward * WalkSpeed * Time.deltaTime * -1;
            PlayerAnim.SetBool("move", true);
            PlayerAnim.SetBool("WS", true);
            PlayerAnim.SetBool("AD", false);
            PlayerAnim.SetFloat("Blend", 0.5f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector += PlayerObject.transform.right * WalkSpeed * Time.deltaTime;
            PlayerAnim.SetBool("move", true);
            PlayerAnim.SetBool("WS", false);
            PlayerAnim.SetBool("AD", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            vector += PlayerObject.transform.right * WalkSpeed * Time.deltaTime * -1;
            PlayerAnim.SetBool("move", true);
            PlayerAnim.SetBool("WS", false);
            PlayerAnim.SetBool("AD", true);
        }
        if (PlayerObject.transform.position == vector)
        {
            PlayerAnim.SetBool("move", false);
            PlayerAnim.SetBool("WS", false);
            PlayerAnim.SetBool("AD", false);
            PlayerAnim.SetFloat("Blend", 0f);
        }

        PlayerObject.transform.position = vector;
        if (Input.GetKey(KeyCode.Space) && IsGround)
        {
            PlayerObject.GetComponent<Rigidbody>().velocity = Vector3.up * JumpPower;
            PlayerAnim.SetBool("Jump", true);
            PlayerAnim.SetBool("move", true);
        }
        else
        {
            PlayerAnim.SetBool("Jump", false);
        }
    }
    public void Camera()
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        
        Vector3 vector = CameraObject.transform.localEulerAngles;
        vector.y -= y_Rotation/10 * CameraSpeed;
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
        vector.y += x_Rotation/10*CameraSpeed;
        PlayerObject.transform.localEulerAngles = vector;
    }
    public void Contact()
    {
        Ray ray = new Ray(RayPosition.transform.position, RayPosition.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * IsGround_maxDistance, Color.green);
        if (Physics.Raycast(ray, out hit, IsGround_maxDistance))
        {
            IsGround = (hit.collider.gameObject.tag == "Ground");
        }
        else
        {
            IsGround = false;
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
        Debug.DrawRay(ray.origin, ray.direction * TakeItem_maxDistance, Color.blue);
        

        if (Physics.Raycast(ray, out hit, TakeItem_maxDistance))
        {
            GameObject HitObj = hit.collider.gameObject;
            Item item = hit.collider.gameObject.GetComponent<Item>();
            if (a!= null)
            {
               // Debug.Log("D");
                a.ShowDescription(false);
            }
            if (hit.collider != null && item != null && HitObj != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    if (item.ThisType == Item.ItemType.Gun)
                    {
                        AddInventory(HitObj, item.CloneObjectNumber, true);

                    }
                    if (item.ThisType == Item.ItemType.bullet)
                    {
                        // Debug.Log(item.CloneObjectNumber);
                        AddInventory(HitObj, item.CloneObjectNumber, true);
                    }

                }
                if (item != null && item.ShowCanvas)
                {
                    item.ShowDescription(true);
                    item.ItemCanvas.transform.LookAt(CameraObject.transform.position);
                    a = item;
                }
                
            }
            else if (a != null)
            {
                ///Debug.Log("B");
                a.ShowDescription(false);
            }

        }
        else if(a!= null)
            a.ShowDescription(false); //Debug.Log("C"); 
    }
    public void AddInventory(GameObject hit,int CloneObjectNumber, bool delete)
    {
        GameObject CloneObject = _itemData.ItemObject[CloneObjectNumber];

        Item item = hit.GetComponent<Item>();
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
         
           // Debug.Log(i);
            if (item.ThisType == Item.ItemType.Gun)
            {
                int i = 0;
                foreach (GameObject a in ItemObject)
                {
                    if ((item.ThisType == Item.ItemType.Gun || item.ThisType == Item.ItemType.recovery) && (a.GetComponent<Item>().ThisType == Item.ItemType.Gun || a.GetComponent<Item>().ThisType == Item.ItemType.recovery))//’Ç‰Á‚µ‚æ‚¤‚Æ‚µ‚Ä‚¢‚éŽí—Þ‚Æinventory“à‚ÌƒAƒCƒeƒ€‚ÌŽí—Þ‚Æˆê’v‚µ‚½‚ç
                    {
                        i++;
                    }
                }

                if(i < 3)
                {
                    ItemName.Add(item.ItemName);
                    ItemCount.Add(1);
                    ItemObject.Add(CloneObject);

                    if (i == 0)
                    {
                        _Inventory.select = 1;
                    }
                    _Inventory.RoadItem();
                    _gun.GanHave();
                    _Inventory.RoadItem();
                }
               
            }
            else if (item.ThisType == Item.ItemType.bullet)
            {
                ItemName.Add(item.ItemName);
                ItemCount.Add(item.count);
                ItemObject.Add(CloneObject);
                _Inventory.RoadItem();
            }
            else
                return;

          
        }
       // Debug.Log(delete + "" + hit);
        if (delete && hit != null)
        {
            Destroy(hit);
        }
    }
    public void ReMoveItem(int index, int count)
    {
        ItemCount[index] -= count;
        if (ItemCount[index] <= 0)
        {
            ItemName.RemoveAt(index);
            ItemCount.RemoveAt(index);
            ItemObject.RemoveAt(index);
        }

    }
  
}
