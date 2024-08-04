using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChangeState : MonoBehaviour
{
    private List<int> PatrolPoint;
    private float AttackDistance;
    private float MaxWaitTime;

    public void settings(List<int> PatrolPointList, float attackDistance, float Time)
    {
        PatrolPoint = PatrolPointList;
        AttackDistance = attackDistance;
        MaxWaitTime = Time;
    }
    public EnemyData ChangeState(EnemyData enemyData)
    {
        
        EnemyData Data = enemyData;
        if (Data.Detoroy)
        {
            Data.Status = EnemyData.Action.Wait;
            return Data;
        }
        Vector3 PlayerPosition = enemyData.PlayerObj.transform.position;
        Vector3 EnemyPosition = enemyData.ThisEnemeyObj.transform.position;
        float Range = Vector3.Distance(PlayerPosition,EnemyPosition);
        if (Data.PlayerView)
        {
            if (Range <= AttackDistance)
            {
                Data.Status = EnemyData.Action.attack;
                return Data;
            }
            Data.Status = EnemyData.Action.chase;
            return Data;
        }
        if (Data.Status == EnemyData.Action.chase)
        {
            if (Vector3.Distance(EnemyPosition, Data.TargetPosition) > Data.PlayerDistance)
            {
                Data.Status = EnemyData.Action.chase;
                return Data;
            }
            Data.Status = EnemyData.Action.Wait;
            return Data;
        }
        if (Data.WaitTime <= 0)
        {
            Data.Status = EnemyData.Action.patrol;
            return Data;
        }
        return Data;
    }
    public MoveDate ConvertingToMoveData(MoveDate moveDate, EnemyData enemyData)
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
            moveDate.Forward = true;
            moveDate.Run = false;
        }
        return moveDate;
    }
}
