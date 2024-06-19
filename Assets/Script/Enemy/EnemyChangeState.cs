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
        if (Data.PlayerView)//���E�ɓ����Ă���
        {
            if(Range <= AttackDistance)//�U���\����
            {
                Data.Status = EnemyData.Action.attack;
                return Data;
            }
            Debug.Log("B");
            Data.Status = EnemyData.Action.chase;
            return Data;
        }
        if(Data.Status == EnemyData.Action.chase)//��������
        {
            Data.Status = EnemyData.Action.Wait;
            return Data;
        }
        if(Data.WaitTime <= 0)//�ҋ@�I��
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
