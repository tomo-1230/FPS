using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    private bool firing;
    private GameObject RayPositonObject;
    public Player player;
    private Inventory inventory;
    private GameObject FiringEffect;
    private Ray ray;
    private float AIM;
    private bool BulletInfinite;
    public void settings(GameObject rpo, Player pl, Inventory inve, GameObject eff, float aim = 0)
    {
        RayPositonObject = rpo;
        BulletInfinite = false;
        if(pl == null)
        {
            FiringEffect = eff;
            return;
        }
        BulletInfinite = PlayerPrefs.GetInt("BulletInfinite") == 1;
        player = pl;
        inventory = inve;
        FiringEffect = eff;
        AIM = aim;
    }
    public async void Shooting(GameObject CloneGun)
    {

        if (CloneGun == null)
        {
            return;
        }
        Item item = null;
        item = CloneGun.GetComponent<Item>();
        if (item.SetBullet <= 0)
        {
            return;
        }
     
        if (firing)
        {
            return;
        }
        firing = true;

        if (item.CloneObjectNumber != 2)
        {
            RaycastHit hit = Ray(CloneGun.GetComponent<Item>().distance);
            GameObject CloneBulletObj = CloneBullet(CloneGun, CloneGun.GetComponent<Item>());
            NormalProcess(hit, CloneBulletObj, CloneGun);
            if(player == null)
            {
                ShrapnelProcess(hit, CloneBulletObj, CloneGun);
            }
            BulletComponent(CloneBulletObj, CloneGun);
        }
        else if (CloneGun.GetComponent<Item>().CloneObjectNumber == 2)
        {
            int Amount = 0;
            do
            {
                RaycastHit hit = Ray(CloneGun.GetComponent<Item>().distance);
                GameObject CloneBulletObj = CloneBullet(CloneGun, CloneGun.GetComponent<Item>());
                ShrapnelProcess(hit, CloneBulletObj, CloneGun);
                BulletComponent(CloneBulletObj, CloneGun);
                Amount++;
            } while (Amount <= CloneGun.GetComponent<Item>().ShotAmount);
        }
        if (!BulletInfinite)
        {
            item.SetBullet--;
        }
        if (inventory != null)
        {
           inventory.ReRoad(true, true);
            player.scoreCounter.Shot();
        }
        
        await Task.Delay(item.FiringInterval);
        firing = false;
       
    }
    public void BulletComponent(GameObject CloneObject, GameObject CloneGun)
    {
        //Debug.Log("Clone");
        Bullet bullet = CloneObject.AddComponent<Bullet>();
        bullet.item = CloneGun.GetComponent<Item>();
        if (player == null)
        {
            return;
        }
        bullet.clone = player.clone;
        if(CloneGun.GetComponent<Item>().CloneObjectNumber == 5)
        {
            bullet.blast = true;
            bullet.blastDistance = CloneGun.GetComponent<Item>().BlastDistance;
        }
    }
    public void NormalProcess(RaycastHit hit, GameObject CloneObject, GameObject CloneGun)
    {
        Item item = CloneGun.GetComponent<Item>();
        if (hit.point != new Vector3(0, 0, 0))
        {
            CloneObject.transform.LookAt(hit.point);
        }
        else
        {
            CloneObject.transform.LookAt(ray.GetPoint(item.distance));
        }
        //bullet.FiringPosition = item.MuzzleObj.transform.position;
        CloneObject = Instantiate(FiringEffect);
        CloneObject.transform.position = item.MuzzleObj.transform.position;
        CloneObject.transform.parent = CloneGun.transform;
    }
    public void ShrapnelProcess(RaycastHit hit, GameObject CloneObject, GameObject CloneGun)
    {
        Item item = CloneGun.GetComponent<Item>();
        Vector3 HitPoint = new Vector3(0, 0, 0);
        if (hit.point != new Vector3(0, 0, 0))
        {
            HitPoint = hit.point;
        }
        else
        {
            HitPoint = ray.GetPoint(item.distance);

        }
        float aim;
        if (player == null || CloneGun.GetComponent<Item>().CloneObjectNumber == 2)
        {
             aim = CloneGun.GetComponent<Item>().ShotAim;
        }
        else
        {
            aim = AIM;
        }
        
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
    }
    public GameObject CloneBullet(GameObject CloneGun, Item item)
    {
        GameObject CloneObject = Instantiate(item.BulletObj);
        CloneObject.SetActive(false);
        Vector3 ClonePosition = CloneGun.GetComponent<Item>().MuzzleObj.transform.position;
        CloneObject.transform.position = ClonePosition;
        CloneObject.SetActive(true);
        return CloneObject;
    }
    public RaycastHit Ray(float RayDistance)
    {
        ray = new Ray(RayPositonObject.transform.position, RayPositonObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, RayDistance)) { }
        return hit;
    }
}
