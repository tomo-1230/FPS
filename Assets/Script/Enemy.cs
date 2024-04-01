using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{    
    public int HP;
    public float WalkSpeed;
    public float RunSpeed;
    public float RayDistance;//見える距離
    public float PlayerDistance;//攻撃が始まる距離
    public float RunDistance;//走り始める距離
    public float WaitTime;
    public float aim;
    public Vector3 TargetPosition;
    public Action Situation;
    public bool PlayerView;
    public List<Vector3> PatrolPoint;
    public GameObject PlayerObject;
    public GameObject RayPosition;
    public GameObject DamageCanvas;
    public GameObject HaveGun;
    public GameObject Target;
    public GameObject Head;
    public Text text;
    public Animator anim;
    public Player player;
    public enum Action
    {
        Wait, patrol, chase, attack
    }
    private NavMeshAgent nav;
    private float time;
    private int Point;
    private bool firng = false;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        action();
        animation_();
        conditions();
    }
    public void conditions()
    {
        if (Situation == Action.Wait)
        {
            Ray();
            if (PlayerView)
            {
                Situation = Action.chase;
            }
            else if (time >= WaitTime)
            {
                ShortestPoint();
                Situation = Action.patrol;
            }

        }
        else if (Situation == Action.patrol)
        {
            Ray();
            if (PlayerView)
            {
                Situation = Action.chase;
            }
            else
            {
                Situation = Action.patrol;
            }
        }
        else if (Situation == Action.chase)
        {
            Ray();
            if (!PlayerView && Vector3.Distance(this.gameObject.transform.position, TargetPosition) < 0.5f)//プレイヤーが見えなくてtargetpositionに近づいたら
            {
                time = 0;
                Situation = Action.Wait;
            }
            if (Vector3.Distance(this.gameObject.transform.position, TargetPosition) < PlayerDistance && PlayerView)
            {
                Situation = Action.attack;
            }
            if(Vector3.Distance(this.gameObject.transform.position, TargetPosition) > PlayerDistance)
            {
                Situation = Action.chase;
            }
        }
        else if(Situation == Action.attack)
        {
            if (!PlayerView)
            {
                time = 0;
            }
            else if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < PlayerDistance)
            {
                Situation = Action.attack;
            }
            else
            {
                Situation = Action.chase;
            }
        }
    }
    public void action()
    {
        if (Situation == Action.Wait)
        {
            nav.stoppingDistance = 100;
            time += Time.deltaTime;
         
        }
        else if (Situation == Action.patrol)
        {
            nav.stoppingDistance = 0;
            nav.speed = WalkSpeed;
            nav.SetDestination(PatrolPoint[Point]);
            if(Vector3.Distance(PatrolPoint[Point],this.transform.position) < 1)
            {
                if(PatrolPoint.Count-1 <= Point)
                {
                    
                    Point = 0;
                    return;
                }
                Point++;
            }
        }
        else if (Situation == Action.chase)
        {
            Ray();
            if (PlayerView)
            {
                if(Vector3.Distance(this.gameObject.transform.position, TargetPosition) >= RunDistance)
                {//run
                    nav.speed = RunSpeed;
                    TargetPosition = PlayerObject.transform.position;
                    nav.stoppingDistance = PlayerDistance;
                    nav.SetDestination(TargetPosition);
                }
                else
                {
                    nav.speed = WalkSpeed;
                    TargetPosition = PlayerObject.transform.position;
                    nav.stoppingDistance = PlayerDistance;
                    nav.SetDestination(TargetPosition);
                }
               
            }
            else
            {
                nav.stoppingDistance = 0;
                nav.SetDestination(TargetPosition);
            }
        }
        else if(Situation == Action.attack)
        {
            Ray();
            this.transform.LookAt(PlayerObject.transform.position);
            Vector3 vector = this.transform.localEulerAngles;
            vector.x = 0;
            this.transform.localEulerAngles = vector;
            Firing();
        }
        DamageCanvas.transform.LookAt(player.CameraObject.transform.position);
    }
    public void animation_()
    {
        if (Situation == Action.Wait)
        {
            anim.SetBool("move", false);
            anim.SetLayerWeight(2, 0f);
            anim.SetBool("Have", false);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0f);
        }
        else if (Situation == Action.patrol)
        {
            anim.SetBool("move", true);
            anim.SetBool("Have", false);
            anim.SetLayerWeight(2, 0f);
            anim.SetBool("WS", true);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0.5f);
            
        }
        else if (Situation == Action.chase)
        {
            if (PlayerView)
            {
                if (Vector3.Distance(this.gameObject.transform.position, TargetPosition) >= RunDistance)
                {//run
                    anim.SetBool("move", true);
                    anim.SetLayerWeight(2, 0f);
                    anim.SetBool("Have", false);
                    anim.SetBool("WS", true);
                    anim.SetBool("AD", false);
                    anim.SetFloat("Blend", 1);
                }
                else
                {
                    anim.SetBool("move", true);
                    anim.SetLayerWeight(2, 0f);
                    anim.SetBool("Have", false);
                    anim.SetBool("WS", true);
                    anim.SetBool("AD", false);
                    anim.SetFloat("Blend", 0.5f);
                }

            }
            else
            {
                anim.SetBool("Have", false);
                anim.SetLayerWeight(2, 0f);
                anim.SetBool("move", true);
                anim.SetBool("WS", true);
                anim.SetBool("AD", false);
                anim.SetFloat("Blend", 0.5f);
            }
        }
        else if (Situation == Action.attack)
        {
            anim.SetBool("Have", true);
            anim.SetLayerWeight(2, 1f);
            anim.SetBool("move", false);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0f);
          
        }
    }
    async void Firing()
    {
       
        if (firng)
        {
            return;
        }
        firng = true;
        Item item = HaveGun.GetComponent<Item>();
        Ray ray = new Ray(Head.transform.position, Head.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * item.distance, Color.black);
        GameObject CloneObject = Instantiate(item.BulletObj);
        CloneObject.transform.position = HaveGun.GetComponent<Item>().MuzzleObj.transform.position;
        Vector3 HitPoint = new Vector3(0,0,0);
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
        if (Random.Range(0, 1) == 1)//ランダム
        {
            HitPoint.x += Random.Range(0, aim);
        }
        else
        {
            HitPoint.x -= Random.Range(0, aim);
        }
        if (Random.Range(0, 1) == 1)
        {
            HitPoint.y += Random.Range(0, aim);
        }
        else
        {
            HitPoint.y -= Random.Range(0, aim);
        }
        if (Random.Range(0, 1) == 1)
        {
            HitPoint.z += Random.Range(0, aim);
        }
        else
        {
            HitPoint.z -= Random.Range(0, aim);
        }
        CloneObject.transform.LookAt(HitPoint);
        CloneObject.AddComponent<Bullet>().item = item;
        item.SetBullet--;
        await Task.Delay(item.FiringInterval);
        firng = false;
    }
   public void Ray()
    {

        RayPosition.transform.LookAt(PlayerObject.transform);
        Ray ray = new Ray(RayPosition.transform.position, RayPosition.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * RayDistance, Color.red);
        bool angle = ((RayPosition.transform.localEulerAngles.x >= 0 && RayPosition.transform.localEulerAngles.x <= 90) || (RayPosition.transform.localEulerAngles.x >= 270 && RayPosition.transform.localEulerAngles.x <= 360));
        if (Physics.Raycast(ray, out hit, RayDistance))
        {

            PlayerView = (hit.collider.gameObject.tag == "player" && angle);
        }
    }
    public void ShortestPoint()
    {
        int i = 0;
        Point = 0;
        foreach(Vector3 a in PatrolPoint)
        {
            if(Point == 0)
            {
                i++;
                return;
            }
            if (Vector3.Distance(PatrolPoint[Point],this.transform.position) > Vector3.Distance(PatrolPoint[i], this.transform.position))
            {
                Point = i;
            }
            i++;
        }
    }

    public async void damage(int value)
    {
        HP -= value;
        text.text = value.ToString();
        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            await Task.Delay(1000);
            
            try
            {
               text.text = "";
            }
            catch
            {

            }
         
        }
        
    }
}
