using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData : MonoBehaviour
{
    public enum Action
    {
        Wait, patrol, chase, attack
    }
    public Action Status;
    public NavMeshAgent nav;
    public Animator anim;
    public bool PlayerView;
    public GameObject PlayerObj;
    public GameObject ThisEnemeyObj;
    public float WaitTime;
    public Vector3 TargetPosition;
    public float RunDistance;
    public float PlayerDistance;
    public float WalkSpeed;
    public float RunSpeed;
    public List<Vector3> PatrolPoint;
    public int Point;
    public void Initialization(NavMeshAgent n, Animator ani, GameObject player, GameObject enemy,List<Vector3> list)
    {
        Status = Action.Wait;
        nav = n;
        anim = ani;
        PlayerView = false;
        PlayerObj = player;
        ThisEnemeyObj = enemy;
        PatrolPoint = list;
        Point = 0;
    }
    public void SettingValue(float run,float player,float walk,float runsp)
    {
        RunDistance = run;
        PlayerDistance = player;
        WalkSpeed = walk;
        RunSpeed = runsp;
    }
}