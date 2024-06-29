using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 FiringPosition;
    public Item item;
    public bool blast;
    public int blastDistance;
    public Clone clone;
    private void Start()
    {
        FiringPosition = this.transform.position;
    }
    void Update()
    {
        this.transform.position += this.gameObject.transform.forward * 50 * Time.deltaTime;
        if (Vector3.Distance(FiringPosition, this.transform.position) >= item.distance)
        {
            Destroy(this.gameObject);
        }
        Ray();
    }
    public void Ray()
    {
        GameObject RayPosition = this.gameObject;
        int RayDistance = 1;
        Ray ray = new Ray(RayPosition.transform.position, RayPosition.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * RayDistance, Color.yellow);
        bool angle = (RayPosition.transform.localEulerAngles.x >= 0 && RayPosition.transform.localEulerAngles.x <= 90) || (RayPosition.transform.localEulerAngles.x >= 270 && RayPosition.transform.localEulerAngles.x <= 360);
        if (Physics.Raycast(ray, out hit, RayDistance))
        {
            Hit(hit.collider.gameObject);
        }
    }
    public void Hit(GameObject HitObj)
    {
        if(HitObj == null)
        {
            return;
        }

        if (HitObj.GetComponent<Enemy>() != null)
        {
            Enemy enemy = HitObj.GetComponent<Enemy>();
            enemy.Damage_(item.Damage, IsHedShot(HitObj));
            
        }
        else if (HitObj.GetComponent<Player>() != null) 
        {
            Player player = HitObj.GetComponent<Player>();
            player.PlayerHP = player._hp.Decrease(player.PlayerHP, item.Damage);
        }

        if(blast && clone != null)
        {
            foreach (GameObject Enemy in clone.ClonedEnemyObj)
            {
                if (Vector3.Distance(Enemy.transform.position, this.transform.position) <= blastDistance)//’e‚Æ“G‚Ì‹——£‚ª”š•—‹——£‚æ‚è‹ß‚©‚Á‚½‚ç
                {
                    float Distance = Vector3.Distance(Enemy.transform.position, this.transform.position);

                }
            }
        }
        if (item.BulletType == 4)
        {
            Instantiate(item.effect, this.transform.position, Quaternion.identity);
        }
    }
    public bool IsHedShot(GameObject HitObj)
    {
        bool HedShot = false;
        this.gameObject.transform.parent = HitObj.transform;
        if (this.gameObject.transform.localPosition.y >= HitObj.GetComponent<Enemy>().HedShotYPosi)
        {
            HedShot = true;
        }
        return HedShot;
    }
    public void Hit2(GameObject HitObj)
    {
        if (HitObj != null)
        {
            
            if (HitObj.tag != "gun" && HitObj.tag != "bullet")
            {
                if (item.ThisBulletType == 4)
                {
                    this.transform.GetChild(0).gameObject.SetActive(false);
                    this.transform.GetChild(1).gameObject.SetActive(true);


                }
                else
                    Destroy(this.gameObject);
            }
           
        }
    }
}
