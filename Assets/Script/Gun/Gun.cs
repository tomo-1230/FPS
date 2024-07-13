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
    public GameObject FiringEffect;

    public GameObject pointerObj;
    public Image pointer;
    public float Reticle_u;
    public float Reticle_b;
    public Sprite cross_hair;
    public GameObject CircleGauge;
    public int ZoomValue;
    public GameObject PrefabGun;
    public GameObject CloneGun;
    public int CloneGunSelect;

    public HaveGun haveGun;
    public SetBulletData setBulletData;
    public GunZoom gunZoom;
    public GunShoot gunShoot;
    public GunReRoad gunReRoad;
    // Start is called before the first frame update
    void Start()
    {
        CircleGauge.SetActive(false);
        pointer.sprite = cross_hair;
        player.PlayerAnim.SetLayerWeight(1, 1f);

        haveGun = this.gameObject.AddComponent<HaveGun>();
        setBulletData = this.gameObject.AddComponent<SetBulletData>();
        gunZoom = this.gameObject.AddComponent<GunZoom>();
        gunShoot = this.gameObject.AddComponent<GunShoot>();
        gunReRoad = this.gameObject.AddComponent<GunReRoad>();
        haveGun.settings(HavePosition, inventory, pointer);
        gunZoom.settings(player, ZoomValue, pointerObj, Reticle_u, Reticle_b);
        gunShoot.settings(player.CameraObject, player, inventory, FiringEffect);
        gunReRoad.settings(CircleGauge, player.PlayerAnim, inventory);
        setBulletData.Clear();
        CloneGun = null;
        PrefabGun = null;
        CloneGunSelect = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //GunSelect();
        zoom();
        Firing();
        if (CloneGun != null)
        {
            Vector3 vector1 = CloneGun.transform.localEulerAngles;
            vector1.z = player.CameraObject.transform.localEulerAngles.y * -1;
            CloneGun.transform.localEulerAngles = vector1;
        }
        Reload();
    }

    public void GanHave(List<GameObject> search, bool IsClone)
    {
        if (IsClone)
        {
            return;
        }
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

        haveGun.DestroyGun(CloneGunSelect, setBulletData);
        CloneGun = haveGun.CloneGun(CloneObj, select, setBulletData);
        CloneGunSelect = select;
    }
    public void zoom()
    {
        RectTransform rect = Reticle_Object.GetComponent<RectTransform>();
        if(CloneGun != null)
        {
            gunZoom.Zoom(PrefabGun);
        }
    }
    public void Firing()
    {
        if(CloneGun == null)
        {
            return;
        }
        bool OnButton;
        if (CloneGun.GetComponent<Item>().RapidFire)
        {
            OnButton = Input.GetMouseButton(0);
        }
        else
        {
            OnButton = Input.GetMouseButtonDown(0);
        }
        if (OnButton)
        {
            gunShoot.Shooting(CloneGun);
        }
       
    }
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            gunReRoad.ReRoad(CloneGun, inventory.inventoryData);
        }
       
    }
    public MoveDate PlayerAnim(MoveDate moveDate)
    {
        MoveDate  data = moveDate;
        data.Shot = (CloneGun != null);
        return data;
    }
}
