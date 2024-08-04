using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public int HP;
    public float WalkSpeed;
    public float RunSpeed;
    public float RayDistance;//å©Ç¶ÇÈãóó£
    public float PlayerDistance;//çUåÇÇ™énÇ‹ÇÈãóó£
    public float RunDistance;//ëñÇËénÇﬂÇÈãóó£
    public float WaitTime;
    public float Aim;
    public float HedShotYPosi;
    public Vector3 TargetPosition;
    public Action Status;
    public bool PlayerView;
    public List<Vector3> PatrolPoint;
    public int PointObject;
    public GameObject PlayerObject;
    public GameObject ViewRayPosition;
    public GameObject DamageCanvas;
    public GameObject CloneText;
    
    public GameObject Target;
    public GameObject Head;
    public GameObject HPCampus;
    public Text text;
    public Animator anim;
    public Player player;
    public Color HitColor;
    public Color HedColor;
    public Color Transparency;
    public Material Enemy_anim;
    public SkinnedMeshRenderer skinned;
    public GameObject ThisObj;
    [SerializeField]
    public int ListNumber;
    public Clone clone;
    public ItemData itemData;
    public enum Action
    {
        Wait, patrol, chase, attack
    }
    public NavMeshAgent nav;
    private float time;
    private int Point;
    //private bool firng = false;
    //private bool reloading = false;

    public GameObject HavePosition;
    public GameObject PrefabGun;
    public GameObject CloneGun;
    public GameObject ShotEffect;
    public GameObject GunRayPositon;

    public EnemyData enemyData;
    public EnemyChangeState enemyChangeState;
    public EnemyAction enemyAction;
    public EnemyRay enemyRay;
    public MoveDate enemyMoveData;
    public MoveAnimation moveAnimation;
    public HaveGun haveGun;
    public SetBulletData setBulletData;
    public GunShoot gunShoot;
    public GunReRoad gunReRoad;
    // Start is called before the first frame update
    void Awake()
    {
        nav = this.gameObject.GetComponent<NavMeshAgent>();

       
    }
    void Start()
    {
        WalkSpeed = PlayerPrefs.GetInt("WalkSpeed");
        RunSpeed = PlayerPrefs.GetInt("WalkSpeed") * 2;
        RayDistance = PlayerPrefs.GetInt("Ray");
        Aim = PlayerPrefs.GetInt("aim") / 100;
        HP = PlayerPrefs.GetInt("EnemyHP");
        GetCloneGun();
        enemyData = this.gameObject.AddComponent<EnemyData>();
        enemyData.Initialization(nav, anim, PlayerObject, this.gameObject, PatrolPoint);
        enemyData.SettingValue(RunDistance, PlayerDistance, WalkSpeed, RunSpeed);
        enemyChangeState = this.gameObject.AddComponent<EnemyChangeState>();
        enemyChangeState.settings(new List<int>(), PlayerDistance, WaitTime);
        enemyAction = this.gameObject.AddComponent<EnemyAction>();
        enemyRay = this.gameObject.AddComponent<EnemyRay>();
        enemyRay.settings(ViewRayPosition, PlayerObject, RayDistance);
        enemyMoveData = new MoveDate();
        moveAnimation = this.gameObject.AddComponent<MoveAnimation>();
        haveGun = this.gameObject.AddComponent<HaveGun>();
        haveGun.settings(HavePosition, null, null);
        setBulletData = this.gameObject.AddComponent<SetBulletData>();
        setBulletData.Clear();
        gunShoot = this.gameObject.AddComponent<GunShoot>();
        gunShoot.settings(GunRayPositon, null, null, ShotEffect,Aim);
        gunReRoad = this.gameObject.AddComponent<GunReRoad>();
        gunReRoad.settings(null, anim);
       CloneGun =  haveGun.CloneGun(PrefabGun, 1, setBulletData);
    }
    public void GetCloneGun()
    {
       int number =  PlayerPrefs.GetInt("EnemyCloneGun");
        PrefabGun = itemData.ItemObject[number];
    }
    // Update is called once per frame
    async void Update()
    {
        action();
        animation_();
        conditions();
        Ray();
        DamageCanvas.transform.LookAt(player.CameraObject.transform.position);
        skinned.material.color = Transparency;
        if(enemyData.Status == EnemyData.Action.attack)
        {
            anim.SetBool("Have", true);
            anim.SetLayerWeight(2, 1f);
            await Task.Delay(1300);
            Firing();
        }
        else
        {
            anim.SetBool("Have", false);
            anim.SetLayerWeight(2, 0f);
        }
        if (HP <= 0)
        {
            erase();
        }
    }
    public void conditions()
    {
        enemyData = enemyChangeState.ChangeState(enemyData);
        enemyMoveData = enemyChangeState.ConvertingToMoveData(enemyMoveData, enemyData);

    }
    public void action()
    {
        enemyAction.Action(enemyData);
        if(CloneGun.GetComponent<Item>().SetBullet == 0)
        {
            gunReRoad.ReRoad(CloneGun, null);
        }
    }
    public void animation_()
    {
        moveAnimation.MoveAnimationControl(enemyMoveData, anim);
    }
    public void Firing()
    {
        gunShoot.Shooting(CloneGun);
    }
    public void Ray()
    {
        enemyData = enemyRay.PlayerView(enemyData);
    }
    public void ShortestPoint()
    {
        int i = 0;
        Point = 0;
        foreach (Vector3 a in PatrolPoint)
        {
            if (Point == 0)
            {
                i++;
                return;
            }
            if (Vector3.Distance(PatrolPoint[Point], this.transform.position) > Vector3.Distance(PatrolPoint[i], this.transform.position))
            {
                Point = i;
            }
            i++;
        }
    }

    public void Damage_(int value, bool HedShot)
    {
        GameObject cloneObj = Instantiate(CloneText);
        HPAnimation hP = cloneObj.GetComponent<HPAnimation>();
        hP.Player = PlayerObject;
        hP.WaitTime = 1000;
        hP.transform.parent = DamageCanvas.gameObject.transform;
        RectTransform rect = cloneObj.GetComponent<RectTransform>();
        rect.position = new Vector3(0, 0, 0);
        rect.rotation = new Quaternion(0, 180, 0, 0);
        rect.sizeDelta = new Vector2(900, 500);
        cloneObj.transform.localPosition = new Vector3(0, 0, 0);
        cloneObj.transform.localScale = new Vector3(1, 1, 1);
        if (HedShot)
        {
            hP.Damagae(value, HedColor);
        }
        else
        {
            hP.Damagae(value, HitColor);

        }
        Player.player.scoreCounter.Hit(HedShot);
        HP -= value;
        if (HP <= 0)
        {
            erase();
        }
    }
    async void erase()
    {
        player.clone.Removed(ListNumber);
        player.Recovery();
        enemyData.Status = EnemyData.Action.Wait;
        if(this.gameObject != null && this.gameObject.GetComponent<CapsuleCollider>() != null)
        {
            Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        }
        enemyData.nav.speed = 0;
        enemyData.Detoroy = true;
        EnemyDyingAnim Danim = new EnemyDyingAnim();
        Danim.Anim(Enemy_anim, skinned,this.gameObject);
        Instantiate(PrefabGun, this.transform.position, Quaternion.identity);
        await Task.Delay(1000);
    }
}
