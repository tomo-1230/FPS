using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public bool firing;
    public GameObject RayPositonObject;
    public Player player;
    public Inventory inventory;
    public GameObject FiringEffect;
    public Ray ray;
    public void settings(GameObject rpo,Player pl,Inventory inve,GameObject eff)
    {
        RayPositonObject = rpo;
        player = pl;
        inventory = inve;
        FiringEffect = eff;
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
        bool OnButton;
        if (item.RapidFire)
        { 
            OnButton = Input.GetMouseButton(0);
        }
        else 
        {
            OnButton = Input.GetMouseButtonDown(0); 
        }
        if (firing || OnButton == false)
        {
            return;
        }
        firing = true;
       
        if (item.CloneObjectNumber != 2)
        {
            RaycastHit hit = Ray(CloneGun.GetComponent<Item>().distance);
            GameObject CloneBulletObj = CloneBullet(CloneGun, CloneGun.GetComponent<Item>());
            NormalProcess(hit, CloneBulletObj, CloneGun);
            CommonProcessing(CloneBulletObj, CloneGun);
        }
        else if (OnButton && !firing && CloneGun.GetComponent<Item>().CloneObjectNumber == 2)
        {
            int Amount = 0;
            do
            {
                RaycastHit hit = Ray(CloneGun.GetComponent<Item>().distance);
                GameObject CloneBulletObj = CloneBullet(CloneGun, CloneGun.GetComponent<Item>());
                ShrapnelProcess(hit, CloneBulletObj, CloneGun);
                CommonProcessing(CloneBulletObj, CloneGun);
                Amount++;
            } while (Amount <= CloneGun.GetComponent<Item>().ShotAmount);
        }
        item.SetBullet--;
        inventory.ReRoad(true,true);
        await Task.Delay(item.FiringInterval);
        firing = false;
    }
    public void CommonProcessing(GameObject CloneObject,GameObject CloneGun)
    {
        Bullet bullet = CloneObject.AddComponent<Bullet>();
        bullet.item = CloneGun.GetComponent<Item>();
        bullet.clone = player.clone;
    }
    public void NormalProcess(RaycastHit hit,GameObject CloneObject,GameObject CloneGun)
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
    public void ShrapnelProcess(RaycastHit hit,GameObject CloneObject,GameObject CloneGun)
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
    }
    public GameObject CloneBullet(GameObject CloneGun,Item item)
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
