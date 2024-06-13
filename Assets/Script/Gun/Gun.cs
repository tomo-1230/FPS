using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Inventory inventory;
    public Player player;
    public GameObject HavePosition;
    public GameObject Reticle_Object;
    public GameObject HitObj;
    public GameObject FiringEffect;

    public GameObject pointerObj;
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
    public GameObject PrefabGun;
    public GameObject CloneGun;
    public int CloneGunSelect;

    public HaveGun haveGun;
    public SetBulletData setBulletData;
    public GunZoom gunZoom;
    // Start is called before the first frame update
    void Start()
    {
        CircleGauge.SetActive(false);
        pointer.sprite = cross_hair;
        player.PlayerAnim.SetLayerWeight(1, 1f);

        haveGun = this.gameObject.AddComponent<HaveGun>();
        setBulletData = this.gameObject.AddComponent<SetBulletData>();
        gunZoom = this.gameObject.AddComponent<GunZoom>();
        haveGun.settings(HavePosition,inventory, pointer);
        gunZoom.settings(player,ZoomValue,pointerObj,Reticle_u,Reticle_b);
        setBulletData.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //GunSelect();
        zoom();
        Firing(Input.GetMouseButtonDown(0), Input.GetMouseButton(0));
        if (CloneGun != null)
        {
            Vector3 vector1 = CloneGun.transform.localEulerAngles;
            vector1.z = player.CameraObject.transform.localEulerAngles.y * -1;
            CloneGun.transform.localEulerAngles = vector1;
        }
        Reload(Input.GetKeyDown(KeyCode.R));
    }
   
    public void GanHave(List<GameObject> search,bool IsClone)
    {
        player.PlayerAnim.SetBool("Have", true);
        player.PlayerAnim.SetLayerWeight(2, 1f);
        Player.HaveGun = true;
        GameObject CloneObj = null;
        int select = inventory.select;
        if (select == 1) { CloneObj = search[0]; }
        if (select == 2) { CloneObj = search[1]; }
        if (select == 3) { CloneObj = search[2]; }
       
        PrefabGun = CloneObj;
        if (CloneObj == null)
        {
            Debug.Log("GunHaveErrer");
            return;
        }
        if (IsClone)
        {
            return;
        }
        haveGun.DestroyGun(CloneGunSelect, setBulletData);
        CloneGun = haveGun.CloneGun(CloneObj, select, setBulletData);
        CloneGunSelect = select;
    }
    public void zoom()
    {
        RectTransform rect = Reticle_Object.GetComponent<RectTransform>();
        if (reloading)
        {
            player.CameraObject.GetComponent<Camera>().fieldOfView = ZoomValue;
            pointer.sprite = cross_hair;
            player.CameraSpeed = player.CameraSpeedNormal;
            rect.localScale = new Vector3(Reticle_u, Reticle_u, Reticle_u);
            return;
        }
       // Debug.Log(gunZoom, PrefabGun);
        gunZoom.Zoom(PrefabGun);
    }
    async void Firing(bool LeftButtonDown, bool LeftButton)
    {
        Item item = null;
        if (CloneGun == null)
        {
            return;
        }
        item = CloneGun.GetComponent<Item>();
        if (item.SetBullet <= 0 || reloading)
        {
            return;
        }
        bool Trigger;
        if (item.RapidFire) { Trigger = LeftButton; }
        else { Trigger = LeftButtonDown; }

        if (Trigger && !firing && item.CloneObjectNumber != 2)
        {
            firing = true;
            Ray ray = new Ray(player.CameraObject.transform.position, player.CameraObject.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, item.distance)) { }
            GameObject CloneObject = Instantiate(item.BulletObj);
            CloneObject.SetActive(false); ;
            Vector3 ClonePosition = item.MuzzleObj.transform.position;
            CloneObject.transform.position = ClonePosition;
            CloneObject.SetActive(true);
            if (hit.point != new Vector3(0, 0, 0))
            {
                CloneObject.transform.LookAt(hit.point);
            }
            else
            {
                CloneObject.transform.LookAt(ray.GetPoint(item.distance));
            }

            Vector3 vector = CloneObject.transform.localEulerAngles;
            CloneObject.transform.localEulerAngles = vector;
            Bullet bullet = CloneObject.AddComponent<Bullet>();
            bullet.item = item;
            bullet.clone = player.clone;

            bullet.FiringPosition = item.MuzzleObj.transform.position;
            CloneObject = Instantiate(FiringEffect);
            CloneObject.transform.position = item.MuzzleObj.transform.position;
            CloneObject.transform.parent = CloneGun.transform;
            item.SetBullet--;
            inventory.ReRoad();
            await Task.Delay(item.FiringInterval);
            firing = false;
        }
        else if (Trigger && !firing && CloneGun.GetComponent<Item>().CloneObjectNumber == 2)
        {
            firing = true;
            int Amount = 0;
            do
            {
                Ray ray = new Ray(player.CameraObject.transform.position, player.CameraObject.transform.forward);
                RaycastHit hit;
                //Debug.DrawRay(ray.origin, ray.direction * item.distance, Color.red);
                if (Physics.Raycast(ray, out hit, item.distance)) { }
                GameObject CloneObject = Instantiate(item.BulletObj);
                CloneObject.SetActive(false);
                Vector3 ClonePosition = CloneGun.GetComponent<Item>().MuzzleObj.transform.position;
                CloneObject.transform.position = ClonePosition;
                CloneObject.SetActive(true);
                Vector3 HitPoint = new Vector3(0, 0, 0);
                if (hit.point != new Vector3(0, 0, 0))
                {
                    HitPoint = hit.point;
                }
                else
                {
                    HitPoint = ray.GetPoint(item.distance);
                }
                float aim = CloneGun.GetComponent<Item>().ShotAim;
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
                Bullet bullet = CloneObject.AddComponent<Bullet>();
                bullet.item = item;
                bullet.clone = player.clone;
                //Debug.Log("A");
                Amount++;
            } while (Amount <= CloneGun.GetComponent<Item>().ShotAmount);

            item.SetBullet--;
            //inventory.ReRoad();
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

        if (CloneGun != null)
        {
            item = CloneGun.GetComponent<Item>();
        }
        else
        {
            reloading = false;
            return;
        }

        if (item.SetBullet >= item.MaxBullet)
        {
            reloading = false;
            return;
        }
        bool existence = false;
        int value = 0;
        foreach (GameObject a in player.ItemObject)
        {
            Item InventoryItem = a.GetComponent<Item>();
            if (InventoryItem == null)
            {
                return;
            }

            if (InventoryItem.ThisBulletType != PrefabGun.GetComponent<Item>().BulletType)
            {
                value++;
            }
            else if (InventoryItem.ThisType != Item.ItemType.bullet)
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
        //Reload

        CircleGauge.SetActive(true);

        player.PlayerAnim.SetBool("reload", true);
        player.PlayerAnim.SetFloat("speed", (item.ReRoadTiem / 100) * item.TimeTweak);
        await Task.Delay(CloneGun.GetComponent<Item>().ReRoadTiem);
        player.PlayerAnim.SetBool("reload", false);
        player.GageAnim.SetBool("reload", false);

        item.SetBullet += consumableBullets;


        player.ReMoveItem(value, consumableBullets);
        inventory.ReRoad();
        CircleGauge.SetActive(false);
        reloading = false;
    }
}
