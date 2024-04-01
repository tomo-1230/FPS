using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 FiringPosition;
    public Item item;
    void Update()
    {
        this.transform.position += this.gameObject.transform.forward * 10 * Time.deltaTime;
        if (Vector3.Distance(FiringPosition, this.transform.position) >= item.distance)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != null)
        {
            if(collision.gameObject.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().damage(item.Damage);
            }
            Destroy(this.gameObject);
 
        }
    }
}
