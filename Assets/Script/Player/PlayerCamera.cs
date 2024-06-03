using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private float CameraSpeed;
    public void ChangeSpeed(float speed)
    {
        CameraSpeed = speed;
    }
    public GameObject CameraMove(GameObject Camera)
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        float y_Rotation = Input.GetAxis("Mouse Y");
        GameObject result = Camera;

        Vector3 vector = result.transform.localEulerAngles;
        vector.y -= y_Rotation / 10 * CameraSpeed;
        result.transform.localEulerAngles = vector;
        vector = result.transform.localEulerAngles;
        if (vector.y < 300 && vector.y > 180)
        {
            vector.y = 300;
        }
        if (vector.y > 60 && vector.y < 180)
        {
            vector.y = 60;

        }
        result.transform.localEulerAngles = vector;
        return result;
    }
    public GameObject PlayerCameraMove(GameObject Player, GameObject Player_Mesh)
    {
        float x_Rotation = Input.GetAxis("Mouse X");
        GameObject result = Player;

        Vector3 vector = Player.transform.localEulerAngles;
        vector.y += x_Rotation / 10 * CameraSpeed;
        Player_Mesh.SetActive(vector.y < 30);
        result.transform.localEulerAngles = vector;
        return result;
    }
}
