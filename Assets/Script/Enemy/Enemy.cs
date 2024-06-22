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
    public float RayDistance;//�����鋗��
    public float PlayerDistance;//�U�����n�܂鋗��
    public float RunDistance;//����n�߂鋗��
    public float WaitTime;
    public float Aim;
    public float HedShotYPosi;
    public Vector3 TargetPosition;
    public Action Status;
    public bool PlayerView;
    public List<Vector3> PatrolPoint;
    public int PointObject;
    public GameObject PlayerObject;
    public GameObject RayPosition;
    public GameObject DamageCanvas;
    public GameObject CloneText;
    public GameObject HaveGun;
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
    public enum Action
    {
        Wait, patrol, chase, attack
    }
    public NavMeshAgent nav;
    private float time;
    private int Point;
    private bool firng = false;
    private bool reloading = false;

    public EnemyData enemyData;
    public EnemyChangeState enemyChangeState;
    public EnemyAction enemyAction;
    public EnemyRay enemyRay;
    public MoveDate enemyMoveData;
    public MoveAnimation moveAnimation;
    // Start is called before the first frame update
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        WalkSpeed = PlayerPrefs.GetInt("WalkSpeed");
        RunSpeed = PlayerPrefs.GetInt("WalkSpeed") * 2;
        RayDistance = PlayerPrefs.GetInt("Ray");
        Aim = PlayerPrefs.GetInt("aim") / 100;
        HP = PlayerPrefs.GetInt("EnemyHP");

        enemyData = this.gameObject.AddComponent<EnemyData>();
        enemyData.Initialization(nav, anim,PlayerObject,this.gameObject,PatrolPoint);
        enemyData.SettingValue(RunDistance, PlayerDistance, WalkSpeed, RunSpeed);
        enemyChangeState = this.gameObject.AddComponent<EnemyChangeState>();
        enemyChangeState.settings(new List<int>(), PlayerDistance, WaitTime);
        enemyAction = this.gameObject.AddComponent<EnemyAction>();
        enemyRay = this.gameObject.AddComponent<EnemyRay>();
        enemyRay.settings(RayPosition, PlayerObject, RayDistance);
        enemyMoveData = new MoveDate();
        moveAnimation = this.gameObject.AddComponent<MoveAnimation>();

    }
    // Update is called once per frame
    void Update()
    {
        action();
        animation_();
        conditions();
        Ray();
        DamageCanvas.transform.LookAt(player.CameraObject.transform.position);
        skinned.material.color = Transparency;
    }
    public void conditions()
    {
        enemyData = enemyChangeState.ChangeState(enemyData);
        enemyMoveData = enemyChangeState.ConvertingToMoveData(enemyMoveData, enemyData);
      
    }
    public void action()
    {
        enemyAction.Action(enemyData);
    }
    public void animation_()
    {
        moveAnimation.MoveAnimationControl(enemyMoveData, anim);
        //if (Status == Action.Wait)
        //{
        //    anim.SetBool("move", false);
        //    anim.SetLayerWeight(2, 0f);
        //    anim.SetBool("Have", false);
        //    anim.SetBool("WS", false);
        //    anim.SetBool("AD", false);
        //    anim.SetFloat("Blend", 0f);
        //}
        //else if (Status == Action.patrol)
        //{
        //    anim.SetBool("move", true);
        //    anim.SetBool("Have", false);
        //    anim.SetLayerWeight(2, 0f);
        //    anim.SetBool("WS", true);
        //    anim.SetBool("AD", false);
        //    anim.SetFloat("Blend", 0.5f);

        //}
        //else if (Status == Action.chase)
        //{
        //    if (PlayerView)
        //    {
        //        if (Vector3.Distance(this.gameObject.transform.position, TargetPosition) >= RunDistance)
        //        {//run
        //            anim.SetBool("move", true);
        //            anim.SetLayerWeight(2, 0f);
        //            anim.SetBool("Have", false);
        //            anim.SetBool("WS", true);
        //            anim.SetBool("AD", false);
        //            anim.SetFloat("Blend", 1);
        //        }
        //        else
        //        {
        //            anim.SetBool("move", true);
        //            anim.SetLayerWeight(2, 0f);
        //            anim.SetBool("Have", false);
        //            anim.SetBool("WS", true);
        //            anim.SetBool("AD", false);
        //            anim.SetFloat("Blend", 0.5f);
        //        }

        //    }
        //    else
        //    {
        //        anim.SetBool("Have", false);
        //        anim.SetLayerWeight(2, 0f);
        //        anim.SetBool("move", true);
        //        anim.SetBool("WS", true);
        //        anim.SetBool("AD", false);
        //        anim.SetFloat("Blend", 0.5f);
        //    }
        //}
        //else if (Status == Action.attack)
        //{
        //    anim.SetBool("Have", true);
        //    anim.SetLayerWeight(2, 1f);
        //    anim.SetBool("move", false);
        //    anim.SetBool("WS", false);
        //    anim.SetBool("AD", false);
        //    anim.SetFloat("Blend", 0f);

        //}
    }
    async void Firing()
    {
        if (firng)
        {
            return;
        }
        Item item = HaveGun.GetComponent<Item>();
        if (item.SetBullet <= 0)
        {
            reload(item);
            return;
        }
        firng = true;
        Ray ray = new Ray(Head.transform.position, Head.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * item.distance, Color.black);
        GameObject CloneObject = Instantiate(item.BulletObj);
        CloneObject.transform.position = item.MuzzleObj.transform.position;
        Vector3 HitPoint = new Vector3(0, 0, 0);
        if (Physics.Raycast(ray, out hit, item.distance))
        {
            if (hit.point != new Vector3(0, 0, 0) && hit.collider.gameObject.tag == "player")
            {
                HitPoint = hit.point;
            }
            else
            {
                HitPoint = ray.GetPoint(item.distance);
            }
        }
        if (Random.Range(0, 1) == 1)//�����_��
        {
            HitPoint.x += Random.Range(0, Aim);
        }
        else
        {
            HitPoint.x -= Random.Range(0, Aim);
        }
        if (Random.Range(0, 1) == 1)
        {
            HitPoint.y += Random.Range(0, Aim);
        }
        else
        {
            HitPoint.y -= Random.Range(0, Aim);
        }
        if (Random.Range(0, 1) == 1)
        {
            HitPoint.z += Random.Range(0, Aim);
        }
        else
        {
            HitPoint.z -= Random.Range(0, Aim);
        }
        CloneObject.transform.LookAt(HitPoint);
        Bullet bullet = CloneObject.AddComponent<Bullet>();
        bullet.item = item;
        bullet.clone = player.clone;
        item.SetBullet--;
        await Task.Delay(item.FiringInterval);
        firng = false;
    }
    async void reload(Item item)
    {
        if (reloading)
        {
            return;
        }
        reloading = true;
        anim.SetBool("reload", true);
        anim.SetFloat("speed", (item.ReRoadTiem / 100) * item.TimeTweak);
        await Task.Delay(item.ReRoadTiem);
        item.SetBullet = item.MaxBullet;
        anim.SetBool("reload", false);

        reloading = false;
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
        HP -= value;
        if (HP <= 0)
        {
            erase();
        }
    }
    public void erase()
    {
        player.clone.Removed(ListNumber);
        Destroy(ThisObj);
        //// Destroy(this.gameObject.GetComponent<CapsuleCollider>());
        // Situation = Action.Wait;
        // skinned.material = Enemy_anim;
        // anim.SetBool("move", false);
        // anim.SetLayerWeight(2, 0f);
        // anim.SetBool("Have", false);
        // anim.SetBool("WS", false);
        // anim.SetBool("AD", false);
        // anim.SetFloat("Blend", 0f);
        // anim.SetBool("disappear", true);
        // await Task.Delay(1000);
        // ThisDestroy();
    }
    public void ThisDestroy()
    {

    }
}
