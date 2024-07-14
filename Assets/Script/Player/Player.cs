using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public  float WalkSpeed;
    public  float RunSpeed;
    public  float JumpPower;
    public  float CameraSpeedNormal;
    public  float Zoom_Long;
    public  float Zoom_Short;
    public  float Zoom_Moderate;
    public enum CameraSpeed
    {
        Normal,Long,Short, Moderate
    }
    public  GameObject PlayerObject;
    public  GameObject CameraObject;
    public  GameObject PlayerMesh;
    public Animator PlayerAnim;
    public Animator GageAnim;
    public ItemData ItemData;
    public static ItemData _itemData;
    public HP _hp;
    public int PlayerHP;
    public int PlayerRecoveryQuantity;
    public Gun _gun;
    public  Clone clone;
    private PlayerMove playerMove;
    private MoveAnimation moveAnimation;
    private PlayerCamera playerCamera;
    private PlayerJump playerJump;
    private MoveDate moveData;

    // Start is called before the first frame update
    void Awake()
    {
        _itemData = ItemData;
    }
    void Start()
    {
        PlayerHP = PlayerPrefs.GetInt("PlayerHP");

        playerJump = this.gameObject.AddComponent<PlayerJump>();
        playerMove = this.gameObject.AddComponent<PlayerMove>();
        moveAnimation = this.gameObject.AddComponent<MoveAnimation>();
        playerCamera = this.gameObject.AddComponent<PlayerCamera>();
        moveData = playerMove.Assignment(this.transform);
        playerJump.Acquisition(JumpPower);
        playerCamera.ChangeSpeed(CameraSpeedNormal);
        
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Animation();
        Camera();
        _hp.PlayerHP(PlayerHP);
        if(this.transform.position.y <= -100)
        {
            PlayerHP = 0;
        }
    }
    public void Animation()
    {
        PlayerAnim = moveAnimation.MoveAnimationControl(moveData, PlayerAnim);
    }
    public void Move()
    {
        moveData = playerMove.Assignment(this.transform, moveData);
        moveData = playerMove.Move(moveData, WalkSpeed, RunSpeed);
        this.transform.position = moveData.TargetTransform.position;
        playerJump.Jump();
       moveData =  _gun.PlayerAnim(moveData);
    }
    public void Camera()
    {
        CameraObject = playerCamera.CameraMove(CameraObject);
        PlayerObject = playerCamera.PlayerCameraMove(PlayerObject, PlayerMesh);
    }
    public void ChangeCameraSpeed(CameraSpeed speed)
    {
        if(speed == CameraSpeed.Normal)
        {
            playerCamera.ChangeSpeed(CameraSpeedNormal);
        }
        if (speed == CameraSpeed.Long)
        {
            playerCamera.ChangeSpeed(Zoom_Long);
        }
        if (speed == CameraSpeed.Short)
        {
            playerCamera.ChangeSpeed(Zoom_Short);
        }
        if (speed == CameraSpeed.Moderate)
        {
            playerCamera.ChangeSpeed(Zoom_Moderate);
        }
    }
    public void Recovery()
    {
        PlayerHP = _hp.Recovery(PlayerHP, PlayerRecoveryQuantity);
    }

}
