using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public Vector3 FiringPosition;
    public Item item;
    public bool blast;
    public float blastDistance;
    public Clone clone;
    private List<GameObject> HitObjList = new List<GameObject>();
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
        GameObject RayPosition;
        if (item.CloneObjectNumber == 5)
        {
            RayPosition = this.gameObject.transform.Find("RayPositionObj").gameObject;
            Debug.Log(RayPosition);
        }
        else
        {
            RayPosition = this.gameObject;
        }
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


        if (blast && clone != null)
        {
            List<GameObject> EnemyObj = new List<GameObject>(clone.ClonedEnemyObj);
            foreach (GameObject Enemy in EnemyObj)
            {
                float Distance = Vector3.Distance(Enemy.transform.position, this.transform.position);
                if (Distance <= blastDistance)//’e‚Æ“G‚Ì‹——£‚ª”š•—‹——£‚æ‚è‹ß‚©‚Á‚½‚ç
                {
                    float DamageValue = item.BlastDamage /(blastDistance - Distance);
                    Debug.Log((int)DamageValue);
                    Enemy.GetComponent<Enemy>().Damage_((int)DamageValue, false);

                }
            }
        }
        if (item.BulletType == 4)
        {
            Instantiate(item.effect, this.transform.position, Quaternion.identity);
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
    public void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.GetComponent<Enemy>() != null)
       {
            HitObjList.Add(collision.gameObject);
       }
    }
}