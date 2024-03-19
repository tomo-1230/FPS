using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public float WalkSpeed;
    public float maxDistance;
    public GameObject PlayerObject;
    public GameObject RayPosition;
    public GameObject Canvas;
    public Text text;
    public Animator anim;
    public Player player;
    public enum Action
    {
        Wait, patrol, chase, attack
    }
    private Vector3 TargetPosition;
    public Action situation;
    public bool PlayerView;
    public int HP;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
          nav.speed = WalkSpeed;
         
        if(situation == Action.Wait)
        {
            anim.SetBool("move", false);
            anim.SetBool("WS", false);
            anim.SetBool("AD", false);
            anim.SetFloat("Blend", 0f);
        }
        else if(situation == Action.patrol)
        {

        }
        else if(situation == Action.chase)
        {
            Ray();
            if (PlayerView)
            {
                TargetPosition = PlayerObject.transform.position;
                nav.SetDestination(TargetPosition);
                anim.SetBool("move", true);
                anim.SetBool("WS", true);
                anim.SetBool("AD", false);
                anim.SetFloat("Blend", 0.5f);
            }
            if (Vector3.Distance(this.gameObject.transform.position, TargetPosition) > 3)
            {
                anim.SetBool("move", false);
                anim.SetBool("WS", false);
                anim.SetBool("AD", false);
                anim.SetFloat("Blend", 0f);
            }
            
        }
        Canvas.transform.LookAt(player.CameraObject.transform.position);
    }
   public void Ray()
    {
       
        RayPosition.transform.LookAt(PlayerObject.transform);
        Ray ray = new Ray(RayPosition.transform.position, RayPosition.transform.forward);
        RaycastHit hit;
       // Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
        bool angle = ((RayPosition.transform.localEulerAngles.x >= 0 && RayPosition.transform.localEulerAngles.x <= 90) || (RayPosition.transform.localEulerAngles.x >= 270 && RayPosition.transform.localEulerAngles.x <= 360));
       // Debug.Log(RayPosition.transform.localEulerAngles.x +""+ angle);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
          
            PlayerView = (hit.collider.gameObject.tag == "player" && angle);
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
