using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Gun :MonoBehaviour
{
    public Inventory inventory;
    public Player player;
    public GameObject HavePosition;  
    public GameObject Reticle_Object;
    public GameObject HitObj;
    public GameObject FiringEffect;

    public Image pointer;
    public float Reticle_u;
    public float Reticle_b;
    public Sprite cross_hair;
    public GameObject CircleGauge;
    public int ZoomValue;
    //public List<Transform> childrens;
    private GameObject CloneObject;
    public bool firing = false;
    private bool reloading;
    public GameObject Prefab_HaveGun;
    public GameObject Clone_HaveGun;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        CircleGauge.SetActive(false);
        pointer.sprite = cross_hair;
        player.PlayerAnim.SetLayerWeight(1, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        GunSelect();
        //inventory.player.anim.SetLayerWeight(2,Weight) ;
        // Weight += 0.1f;
        zoom(Input.GetMouseButton(1));
        Firing(Input.GetMouseButtonDown(0), Input.GetMouseButton(0));
        if(Clone_HaveGun != null)
        {
            Vector3 vector1 = Clone_HaveGun.transform.localEulerAngles;
            vector1.z = player.CameraObject.transform.localEulerAngles.y * -1;
            Clone_HaveGun.transform.localEulerAngles = vector1;
        }
        Reload(Input.GetKeyDown(KeyCode.R));

       // Debug.Log(Clone_HaveGun);
    }
    public void GunSelect()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.panel1.activeSelf)
        {
            inventory.select = 1;
            GanHave();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.panel2.activeSelf)
        { 
            inventory.select = 2;
            GanHave();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory.panel3.activeSelf)
        { 
            inventory.select = 3;
            GanHave();
        }

    }
    public void GanHave()
    {
        player.PlayerAnim.SetBool("Have", true);
        player.PlayerAnim.SetLayerWeight(2, 1f);
        Player.HaveGun = true;
        //player.anim.SetLayerWeight(1, 0.5f);
        if(CloneObject != null)
        {
            Destroy(CloneObject);
        }
         Prefab_HaveGun = null;
        int select = inventory.select;
        if(select == 1) { Prefab_HaveGun = inventory.search[0]; }
        if (select == 2) { Prefab_HaveGun = inventory.search[1]; }
        if (select == 3) { Prefab_HaveGun = inventory.search[2]; }
        if (Prefab_HaveGun != null)
        {
            CloneObject = Instantiate(Prefab_HaveGun);
            Clone_HaveGun = CloneObject;
        }
        CloneObject.transform.parent = HavePosition.transform;
        CloneObject.transform.localPosition = new Vector3(0, 0, 0);
        HavePosition.transform.localPosition = Prefab_HaveGun.GetComponent<Item>().HavePosition;
        CloneObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        Destroy(CloneObject.GetComponent<BoxCollider>());
        Destroy(CloneObject.GetComponent<Rigidbody>());
        pointer.sprite = Prefab_HaveGun.GetComponent<Item>().Have_cross_hair;
        inventory.RoadItem();
    }
    public void zoom(bool button)
    {
        if (reloading)
        {
            player.CameraObject.GetComponent<Camera>().fieldOfView = ZoomValue;
            pointer.sprite = cross_hair;
            player.CameraSpeed = player.CameraSpeedNormal;

            Reticle_Object.GetComponent<RectTransform>().localScale = new Vector3(Reticle_u, Reticle_u, Reticle_u);
           // Debug.Log("a");
            return;
        }

        if (button && Prefab_HaveGun != null)
        {
          //  Debug.Log("b");
            if (Clone_HaveGun.GetComponent<Item>().SetBullet <= 0 || reloading)
            {
                return;
            }
            player.CameraObject.GetComponent<Camera>().fieldOfView = Prefab_HaveGun.GetComponent<Item>().ZoomValue;
            pointer.sprite = Prefab_HaveGun.GetComponent<Item>().Set_cross_hair;
            int a = Prefab_HaveGun.GetComponent<Item>().CloneObjectNumber;
            if (a == 0 || a == 1 || a == 2 || a == 3)
            {
               player.CameraSpeed = player.CameraSpeedZoom_Short;
            }
            else if(a == 5)
            {
                player.CameraSpeed = player.CameraSpeedZoom_Moderate;
            }
            else if(a == 4)
            {
                player.CameraSpeed = player.CameraSpeedZoom_Long;
            }
            if (Prefab_HaveGun.GetComponent<Item>().CloneObjectNumber == 4)
            {
                Reticle_Object.GetComponent<RectTransform>().localScale = new Vector3(Reticle_b, Reticle_b, Reticle_b);
            }
            else
            {
                Reticle_Object.GetComponent<RectTransform>().localScale = new Vector3(Reticle_u, Reticle_u, Reticle_u);
            }
        }
        else
        {
            player.CameraObject.GetComponent<Camera>().fieldOfView = ZoomValue;
            player.CameraSpeed = player.CameraSpeedNormal;
            if (Prefab_HaveGun != null)
            {
                pointer.sprite = Prefab_HaveGun.GetComponent<Item>().Have_cross_hair;
            }
            Reticle_Object.GetComponent<RectTransform>().localScale = new Vector3(Reticle_u, Reticle_u, Reticle_u);
        }
    }
    async void Firing(bool LeftButtonDown, bool LeftButton)
    {
        
        
        Item item = null;

        if (Clone_HaveGun != null)
        {
            item = Clone_HaveGun.GetComponent<Item>();
        }
        else
        {
            return;
        }

        if (item.SetBullet <= 0 || reloading)
        {
            return;
        }
      
        bool Trigger;
        if (item.RapidFire)
        {
            Trigger = LeftButton;
        }
        else
        {
            Trigger = LeftButtonDown;
        }

        if (Trigger && !firing && Clone_HaveGun.GetComponent<Item>().CloneObjectNumber != 2)
        {
            firing = true;
            Ray ray = new Ray(player.CameraObject.transform.position,player.CameraObject.transform.forward);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * item.distance, Color.red);
            if (Physics.Raycast(ray, out hit, item.distance))
            {

            }
            GameObject CloneObject = Instantiate(item.BulletObj);
            CloneObject.SetActive(false);
            Vector3 ClonePosition = Clone_HaveGun.GetComponent<Item>().MuzzleObj.transform.position;
            CloneObject.transform.position = ClonePosition;
            CloneObject.SetActive(true);
            if (hit.point != new Vector3(0, 0, 0))
            {
                //Debug.Log(hit.point);
                CloneObject.transform.LookAt(hit.point);
            }
            else
            {
                CloneObject.transform.LookAt(ray.GetPoint(item.distance));
                //Debug.Log(ray.GetPoint(item.distance) +"a");
            }

            Vector3 vector = CloneObject.transform.localEulerAngles;
            //vector.y += 90;
            CloneObject.transform.localEulerAngles = vector;
            CloneObject.AddComponent<Bullet>().item = item;
            CloneObject.GetComponent<Bullet>().FiringPosition = Clone_HaveGun.GetComponent<Item>().MuzzleObj.transform.position;
            CloneObject = Instantiate(FiringEffect);
            CloneObject.transform.position = Clone_HaveGun.GetComponent<Item>().MuzzleObj.transform.position;
            CloneObject.transform.parent = Clone_HaveGun.transform;
            item.SetBullet--;
            inventory.RoadItem();
            await Task.Delay(item.FiringInterval);
            firing = false;
        }
        else if(Trigger && !firing && Clone_HaveGun.GetComponent<Item>().CloneObjectNumber == 2)
        {
            firing = true;
            int Amount = 0;
            do
            {
                Ray ray = new Ray(player.CameraObject.transform.position, player.CameraObject.transform.forward);
                RaycastHit hit;
                //Debug.DrawRay(ray.origin, ray.direction * item.distance, Color.red);
                if (Physics.Raycast(ray, out hit, item.distance))
                {

                }
                GameObject CloneObject = Instantiate(item.BulletObj);
                CloneObject.SetActive(false);
                Vector3 ClonePosition = Clone_HaveGun.GetComponent<Item>().MuzzleObj.transform.position;
                CloneObject.transform.position = ClonePosition;
                CloneObject.SetActive(true);
                Vector3 HitPoint = new  Vector3(0,0,0);
                if (hit.point != new Vector3(0, 0, 0))
                {
                    //Debug.Log(hit.point);
                    HitPoint = hit.point;
                }
                else
                {
                  HitPoint = ray.GetPoint(item.distance);
                    //Debug.Log(ray.GetPoint(item.distance) +"a");
                }
                float aim = Clone_HaveGun.GetComponent<Item>().ShotAim;
                if (Random.Range(0, 1) == 1)//ƒ‰ƒ“ƒ_ƒ€
                {
                    HitPoint.x += Random.Range(0, aim);
                }
                else
                {
                    HitPoint.x -= Random.Range(0, aim);
                }
                if (Random.Range(0, 1) == 1)
                {
                    HitPoint.y += Random.Range(0, aim);
                }
                else
                {
                    HitPoint.y -= Random.Range(0, aim);
                }
                if (Random.Range(0, 1) == 1)
                {
                    HitPoint.z += Random.Range(0, aim);
                }
                else
                {
                    HitPoint.z -= Random.Range(0, aim);
                }
                CloneObject.transform.LookAt(HitPoint);
                Vector3 vector = CloneObject.transform.localEulerAngles;
                //vector.y += 90;
                CloneObject.transform.localEulerAngles = vector;
                CloneObject.AddComponent<Bullet>().item = item;
                Debug.Log("A");
                Amount++;
            } while (Amount <= Clone_HaveGun.GetComponent<Item>().ShotAmount);
            
            item.SetBullet--;
            inventory.RoadItem();
            await Task.Delay(item.FiringInterval);
            firing = false;
        }
       
    }
    public async void Reload(bool ButtonDown)
    {
        if (!ButtonDown || reloading)
        {
            return;
        }

        //check
        reloading = true;
        Item item = null;

        if(Clone_HaveGun != null)
        {
            item = Clone_HaveGun.GetComponent<Item>();
        }
        else
        {
            reloading = false;
            return;
        }

        if(item.SetBullet >= item.MaxBullet)
        {
            reloading = false;
            return;
        }
        bool existence = false;
        int value = 0;
        foreach (GameObject a in player.ItemObject)
        {
            if(a.GetComponent<Item>() == null)
            {
                return;
            }
            Item InventoryItem = a.GetComponent<Item>();
            if (InventoryItem.ThisBulletType != Prefab_HaveGun.GetComponent<Item>().BulletType)
            {
                value++;
            }
            else  if (InventoryItem.ThisType != Item.ItemType.bullet)
            {
               value++;   
            }
            else
            {
                existence = true;
            }

        }

        if (!existence)
        {
            Debug.Log("Not existence");
            reloading = false;
            return;
        }

        int Havebullet = player.ItemCount[value];

        //count
        

        int consumableBullets = 0;
        int difference = item.MaxBullet - item.SetBullet;
        if (difference <= Havebullet)
        {
            consumableBullets = difference;
        }
        else
        {
            consumableBullets = Havebullet;
        }

       //Debug.Log(consumableBullets);
        if (consumableBullets >= item.MaxBullet)
        {
            //Debug.Log("era-");
        }
        //Reload

        CircleGauge.SetActive(true);
        
        player.PlayerAnim.SetBool("reload", true);
        player.PlayerAnim.SetFloat("speed", (item.ReRoadTiem / 100)*item.TimeTweak);
        //Debug.Log(item.ReRoadTiem);
        //player.GageAnim.SetBool("reload", true);
        //player.GageAnim.SetFloat("speed", item.ReRoadTiem / 100);
        await Task.Delay(Clone_HaveGun.GetComponent<Item>().ReRoadTiem);
        player.PlayerAnim.SetBool("reload", false);
        player.GageAnim.SetBool("reload", false);

        item.SetBullet += consumableBullets;

       
        player.ReMoveItem(value, consumableBullets);
        inventory.RoadItem();
        CircleGauge.SetActive(false);
        reloading = false;
    }
}
