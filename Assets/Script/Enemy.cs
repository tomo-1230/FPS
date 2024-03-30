using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections.Generic;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public float WalkSpeed;
    public float RunSpeed;
    public float RayDistance;
    public float PlayerDistance;
    public float RunDistance;
    public GameObject PlayerObject;
    public GameObject RayPosition;
    public GameObject Canvas;
    public GameObject Clone_HaveGun;
    public GameObject Target;
    public GameObject Head;
    public Text text;
    public Animator anim;
    public Player player;
    public enum Action
    {
        Wait, patrol, chase, attack
    }
    public Vector3 TargetPosition;
    public Action situation;
    public bool PlayerView;
    public int HP;
    private NavMeshAgent nav;
    public float WaitTime;
    public float time;
    public List<Vector3> PatrolPoint;
    public int Point;
    public float aim;
    private bool firng;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        action();
        conditions();
    }
    public void conditions()
    {
        if (situation == Action.Wait)
        {
            Ray();
            if (PlayerView)
            {
                situation = Action.chase;
            }
            else if (time >= WaitTime)
            {
                ShortestPoint();
                situation = Action.patrol;
            }

        }
        else if (situation == Action.patrol)
        {
            Ray();
            if (PlayerView)
            {
                situation = Action.chase;
            }
            else
            {
                situation = Action.patrol;
            }
        }
        else if (situation == Action.chase)
        {
            Ray();
            if (!PlayerView && Vector3.Distance(this.gameObject.transform.position, TargetPosition) < 0.5f)
            {
                time = 0;
                situation = Action.Wait;
            }
            if (Vector3.Distance(this.gameObject.transform.position, TargetPosition) < PlayerDistance && PlayerView)
            {
               // Debug.Log("A");
                situation = Action.attack;
            }
            if(Vector3.Distance(this.gameObject.transform.position, TargetPosition) > PlayerDistance)
            {
               // Debug.Log("B");
                situation = Action.chase;
            }
           // Debug.Log(Vector3.Distance(this.gameObject.transform.position, TargetPosition));
        }
        else if(situation == Action.attack)
        {
            if (!PlayerView)
            {
                time = 0;
                //situation = Action.Wait;
            }
            else if (Vector3.Distance(this.gameObject.transform.position, player.transform.position) < PlayerDistance)
            {
                //Debug.Log("A");
                situation = Action.attack;
            }
            else
            {
                //Debug.Log("B");
                situation = Action.chase;
            }
        }
    }
    public void action()
    {
       
        //Debug.Log(nav.stoppingDistance);
        if (situation == Action.Wait)
        {
            nav.stoppingDistance = 100;
            time += Time.deltaTime;
            anim.SetBool("move", false);
            anim.SetLayerWeight(2, 0f);
            anim.SetBool("Have", false);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0f);
        }
        else if (situation == Action.patrol)
        {
            anim.SetBool("move", true);
            anim.SetBool("Have", false);
            anim.SetLayerWeight(2, 0f);
            anim.SetBool("WS", true);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0.5f);
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
        else if (situation == Action.chase)
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
                    anim.SetBool("move", true);
                    anim.SetLayerWeight(2, 0f);
                    anim.SetBool("Have", false);
                    anim.SetBool("WS", true);
                    anim.SetBool("AD", false);
                    anim.SetFloat("Blend", 1);
                }
                else
                {
                    nav.speed = WalkSpeed;
                    TargetPosition = PlayerObject.transform.position;
                    nav.stoppingDistance = PlayerDistance;
                    nav.SetDestination(TargetPosition);
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
                nav.stoppingDistance = 0;
                nav.SetDestination(TargetPosition);
                anim.SetBool("Have", false);
                anim.SetLayerWeight(2, 0f);
                anim.SetBool("move", true);
                anim.SetBool("WS", true);
                anim.SetBool("AD", false);
                anim.SetFloat("Blend", 0.5f);
            }
        }
        else if(situation == Action.attack)
        {
            Ray();
            anim.SetBool("Have", true);
            anim.SetLayerWeight(2, 1f);
            anim.SetBool("move", false);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0f);
            this.transform.LookAt(PlayerObject.transform.position);
            Vector3 vector = this.transform.localEulerAngles;
            vector.x = 0;
            this.transform.localEulerAngles = vector;
            Firing();
        }
        Canvas.transform.LookAt(player.CameraObject.transform.position);
    }
    async void Firing()
    {
       
        if (firng)
        {
            return;
        }
        firng = true;
        Item item = Clone_HaveGun.GetComponent<Item>();
        Ray ray = new Ray(Head.transform.position, Head.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * item.distance, Color.black);
        
      //  Debug.Log(hit.point);
        GameObject CloneObject = Instantiate(item.BulletObj);
        //CloneObject.SetActive(false);
        CloneObject.transform.position = Clone_HaveGun.GetComponent<Item>().MuzzleObj.transform.position;
        Vector3 HitPoint = new Vector3(0,0,0);
        if (Physics.Raycast(ray, out hit, item.distance))
        {
            if (hit.point != new Vector3(0, 0, 0) && hit.collider.gameObject.tag == "player")
            {
                Debug.Log("A");
                HitPoint = hit.point;
            }
            else
            {
                HitPoint = ray.GetPoint(item.distance);
            }
        }
        if (Random.Range(0, 1) == 1)
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
       // Debug.Log(RayPosition.transform.localEulerAngles.x + "" + angle);
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
