using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float JumpPower;
    public float CameraSpeed;
    public float CameraSpeedNormal;
    public float CameraSpeedZoom_Long;
    public float CameraSpeedZoom_Short;
    public float CameraSpeedZoom_Moderate;
    public float IsGround_maxDistance;
    public int TakeItem_maxDistance;
    public GameObject PlayerObject;
    public GameObject CameraObject;
    public GameObject RayPosition;
    public GameObject Player_Mesh;
    public Animator PlayerAnim;
    public Animator GageAnim;
    public ItemData ItemData;
    public static ItemData _itemData;
    [SerializeField]
    public bool IsGround;
    [Header("UI")]
    public GameObject inventory;
    [Header("inventory")]
    public List<string> ItemName;
    public List<int> ItemCount;
    public List<GameObject> ItemObject;
    public Inventory _Inventory;
    public HP _hp;
    public int PlayerHP;
    public Gun _gun;
    public static bool HaveGun;
    public Clone clone;

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

        if (Input.GetKey(KeyCode.Y))
        {
            _Inventory.ReRoad();
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

        if (HaveGun)
        {
            PlayerAnim.SetBool("Have", HaveGun);
        }
    }
    public void Camera()
    {
        CameraObject = playerCamera.CameraMove(CameraObject);
        PlayerObject = playerCamera.PlayerCameraMove(PlayerObject, Player_Mesh);
    }
    public void ReMoveItem(int index, int count)
    {


    }

}
