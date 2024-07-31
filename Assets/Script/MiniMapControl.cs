using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapControl : MonoBehaviour
{

    public GameObject PlayerObject;
    public GameObject PlayerPin;
    //public GameObject CriteriaObject;
    public float Value;

    public Sprite FastFloor;
    public Sprite SecondFloor;
    public Image MapImage;
    private int PlayerFloor;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerObject.transform.position.y < 2)
        {
            PlayerFloor = 1;
        }   
        else
        {
            PlayerFloor = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {

        PlayerPinMove();
        MapChange();
    }
    public void PlayerPinMove()
    {
        if (Value == 0)
        {
            Value = 1;
        }
        Vector3 PinPosition = PlayerObject.transform.position;
        PinPosition.x *= Value;
        PinPosition.z *= Value;
        RectTransform rect = PlayerPin.GetComponent<RectTransform>();
        Vector3 rectPosition = rect.transform.localPosition;
        rectPosition.x = PinPosition.x;
        rectPosition.y = PinPosition.z;
        rect.transform.localPosition = rectPosition;
    }
    public void MapChange()
    {
        if (PlayerFloor == 1 && PlayerObject.transform.position.y > 3) //2F‚É‚·‚é
        {
            MapImage.sprite = SecondFloor;
            PlayerFloor = 2;
        }
        if (PlayerFloor == 2 && PlayerObject.transform.position.y < 2)//1F‚É‚·‚é
        {
            MapImage.sprite = FastFloor;
            PlayerFloor = 1;
        }
    }
}
