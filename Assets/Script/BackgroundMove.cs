using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public enum Type
    {
        Enemy,walk,Bullet,Camera
    }
    public Type ObjectType;
    private Vector3 BulletPositon;
    void Start()
    {
        if (ObjectType == Type.Bullet)
        {
            BulletPositon = this.transform.position;
            BulletPositon.x  -= 3.5f;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectType == Type.Enemy)
        {
            Animator anim = this.gameObject.GetComponent<Animator>();
            anim.SetBool("Have", true);
        }
        if (ObjectType == Type.walk)
        {
            Animator anim = this.gameObject.GetComponent<Animator>();
            anim.SetInteger("Forward", 1);
            anim.SetFloat("Blend", 0.5f);
        }
        if (ObjectType == Type.Bullet)
        {
            this.transform.position  = BulletPositon;
        }
        if (ObjectType == Type.Camera)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                this.gameObject.transform.localEulerAngles = new Vector3(0, 290.718994f, 0);
            }
            Vector3 vector = this.gameObject.transform.localEulerAngles;
            vector.y += 0.05f;
            this.gameObject.transform.localEulerAngles = vector;
        }

    }
}
