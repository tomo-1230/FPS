using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public Rigidbody PlayerRigidbody;
    public Transform PlayerTransform;
    public float JumpPower;
    public bool IsGround;
    public void Acquisition(float jp = 0)
    {
        PlayerRigidbody = this.gameObject.GetComponent<Rigidbody>() ;
        PlayerTransform = this.gameObject.transform;
        if(jp != 0)
        {
            JumpPower = jp;
        }
    }
    public void Jump()
    {
        Acquisition();
        if(PlayerRigidbody == null || PlayerTransform == null)
        {
            Debug.LogError("No initial settings have been made");
            return;
        }
        Ray();
        if (Input.GetKey(KeyCode.Space) && IsGround)
        {
            PlayerRigidbody.velocity = Vector3.up * JumpPower;
        }
    }
    public void Ray()
    {
        float maxDistance = 1f;
        Ray ray = new Ray(PlayerTransform.position, PlayerTransform.up * -1);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            IsGround = (hit.collider.gameObject.tag == "Ground");
        }
        else
        {
            IsGround = false;
        }
    }
}
