using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    public GameObject RayPosition;
    public GameObject PlayerObject;
    public float RayDistance;
    public void settings(GameObject ray, GameObject Player, float dis)
    {
        RayPosition = ray;
        PlayerObject = Player;
        RayDistance = dis;
    }
    public EnemyData PlayerView(EnemyData enemyData)
    {
        EnemyData data = enemyData;
        RayPosition.transform.LookAt(PlayerObject.transform);
        Ray ray = new Ray(RayPosition.transform.position, RayPosition.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * RayDistance, Color.red);
        bool angle = (RayPosition.transform.localEulerAngles.x >= 0 && RayPosition.transform.localEulerAngles.x <= 90) || (RayPosition.transform.localEulerAngles.x >= 270 && RayPosition.transform.localEulerAngles.x <= 360);
        if (Physics.Raycast(ray, out hit, RayDistance))
        {

            data.PlayerView = hit.collider.gameObject.tag == "player" && angle;
        }
        if (data.PlayerView)
        {
            data.TargetPosition = PlayerObject.transform.position;
        }
        return data;
    }
}
