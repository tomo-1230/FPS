using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeState : MonoBehaviour
{
   private List<int> PatrolPoint;
    private float AttackDistance;
    private float MaxWaitTime;

    public void settings(List<int> PatrolPointList,float attackDistance,float Time)
    {
        PatrolPoint = PatrolPointList;
        AttackDistance = attackDistance;
        MaxWaitTime = Time;
    }
    public EnemyData ChangeState(EnemyData enemyData)
    {
        Debug.Log("A");
        EnemyData Data = enemyData;
        float Range = Vector3.Distance(enemyData.PlayerObj.transform.position, enemyData.ThisEnemeyObj.transform.position);
        if (Data.PlayerView)//Ž‹ŠE‚É“ü‚Á‚Ä‚¢‚é
        {
            if(Range <= AttackDistance)//UŒ‚‰Â”\‹——£
            {
                Data.Status = EnemyData.Action.attack;
                return Data;
            }
            Debug.Log("B");
            Data.Status = EnemyData.Action.chase;
            return Data;
        }
        if(Data.Status == EnemyData.Action.chase)//Œ©Ž¸‚Á‚½
        {
            Data.Status = EnemyData.Action.Wait;
            return Data;
        }
        if(Data.WaitTime <= 0)//‘Ò‹@I—¹
        {
            Data.Status = EnemyData.Action.patrol;
            return Data;
        }
        return Data;
    }
    private void ShortestPoint()
    {

    }
}
