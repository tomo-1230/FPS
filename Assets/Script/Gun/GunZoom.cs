using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunZoom : MonoBehaviour
{
    public Player player;
    public int NormalFieldOfView;
    public GameObject ReticulePointer;
    public float NormalReticleScale;
    public float BigReticleScale;
    public void settings(Player play, int Noraml, GameObject Reticule, float NR, float BR)
    {
        player = play;
        NormalFieldOfView = Noraml;
        ReticulePointer = Reticule;
        NormalReticleScale = NR;
        BigReticleScale = BR;
        player.CameraObject.GetComponent<Camera>().fieldOfView = NormalFieldOfView;
    }
    public void Zoom(GameObject GunObject)
    {
        if (GunObject == null || GunObject.GetComponent<Item>() == null)
        {
            Debug.Log("GunZoom.Zoom Error A");
            return;
        }
        if (GunObject.GetComponent<Item>().ThisType != Item.ItemType.Gun)
        {
            Debug.Log("GunZoom.Zoom Error B");
            return;
        }

        if (Input.GetMouseButton(1))
        {
            if (GunObject.GetComponent<Item>().SetBullet <= 0)
            {
                Normal(GunObject);
                return;
            }
            int a = GunObject.GetComponent<Item>().CloneObjectNumber;
            if (a == 0 || a == 1 || a == 2 || a == 3)
            {
                NormalZoom(GunObject);
            }
            else if (a == 4 || a == 5)
            {
                HighZoom(GunObject);
            }
        }
        else
        {
            Normal(GunObject);
        }
    }
    private void Normal(GameObject GunObject)
    {
        player.CameraObject.GetComponent<Camera>().fieldOfView = NormalFieldOfView;
        player.ChangeCameraSpeed(Player.CameraSpeed.Normal);
        if (GunObject != null)
        {
            Image PointerImage = ReticulePointer.GetComponent<Image>();
            PointerImage.sprite = GunObject.GetComponent<Item>().Have_cross_hair;
        }
        RectTransform PointerRect = ReticulePointer.GetComponent<RectTransform>();
        PointerRect.localScale = new Vector3(NormalReticleScale, NormalReticleScale, NormalReticleScale);
    }
    private void NormalZoom(GameObject GunObject)
    {
        player.CameraObject.GetComponent<Camera>().fieldOfView = GunObject.GetComponent<Item>().ZoomValue;
        Image PointerImage = ReticulePointer.GetComponent<Image>();
        PointerImage.sprite = GunObject.GetComponent<Item>().Set_cross_hair;
        player.ChangeCameraSpeed(Player.CameraSpeed.Normal);
        RectTransform PointerRect = ReticulePointer.GetComponent<RectTransform>();
        PointerRect.localScale = new Vector3(NormalReticleScale, NormalReticleScale, NormalReticleScale);

    }
    private void HighZoom(GameObject GunObject)
    {
        player.CameraObject.GetComponent<Camera>().fieldOfView = GunObject.GetComponent<Item>().ZoomValue;
        Image PointerImage = ReticulePointer.GetComponent<Image>();
        PointerImage.sprite = GunObject.GetComponent<Item>().Set_cross_hair;
        int a = GunObject.GetComponent<Item>().CloneObjectNumber;
        if (a == 5)
        {
            player.ChangeCameraSpeed(Player.CameraSpeed.Moderate);
        }
        else if (a == 4)
        {
            player.ChangeCameraSpeed(Player.CameraSpeed.Long);
        }
        RectTransform PointerRect = ReticulePointer.GetComponent<RectTransform>();
        if (GunObject.GetComponent<Item>().CloneObjectNumber == 4)
        {
            PointerRect.localScale = new Vector3(BigReticleScale, BigReticleScale, BigReticleScale);
        }
        else
        {
            PointerRect.localScale = new Vector3(BigReticleScale, BigReticleScale, BigReticleScale);
        }
    }
}
