using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 FiringPosition;
    public Item item;
    private void Start()
    {
        FiringPosition = this.transform.position;
    }
    void Update()
    {
        this.transform.position += this.gameObject.transform.forward * 50 * Time.deltaTime;
        if (Vector3.Distance(FiringPosition, this.transform.position) >= item.distance)
        {
           // Debug.Log("destroy2");
            Destroy(this.gameObject);
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.GetComponent<Enemy>() != null)
            {
               // Debug.Log("Damage");
                collision.gameObject.GetComponent<Enemy>().damage(item.Damage);
                if(item.BulletType == 4)
                {
                    Instantiate(item.effect, this.transform.position,Quaternion.identity);
                }
            }
            else if (collision.gameObject.GetComponent<Player>() != null)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.PlayerHP = player._hp.Decrease(player.PlayerHP, item.Damage);
            }
            if(collision.gameObject.tag != "gun" && collision.gameObject.tag != "bullet")
            {
                Debug.Log("destroy");
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
