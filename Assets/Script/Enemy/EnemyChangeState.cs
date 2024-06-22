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
            if (Vector3.Distance(this.gameObject.transform.position, Data.TargetPosition) > Data.PlayerDistance)
            {
                Data.Status = EnemyData.Action.chase;
                return Data;
            }
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
    public MoveDate ConvertingToMoveData(MoveDate moveDate,EnemyData enemyData)
    {
        MoveDate MData = moveDate;
        EnemyData Edata = enemyData;
        moveDate.Stop = false;
        moveDate.Forward = false;
        moveDate.Back = false;
        moveDate.Left = false;
        moveDate.Right = false;
        moveDate.Right = false;
        if (enemyData.Status == EnemyData.Action.Wait)
        {
            moveDate.Stop = true;
        }
        else if (enemyData.Status == EnemyData.Action.chase)
        {
                 moveDate.Forward = true;
                if (Vector3.Distance(this.gameObject.transform.position, enemyData.TargetPosition) >= enemyData.RunDistance)
                {//run
                    moveDate.Run = true;

                }
            
        }
        else if (enemyData.Status == EnemyData.Action.patrol)
        {
            moveDate.Run = true;
        }
        return moveDate;
    }
}
