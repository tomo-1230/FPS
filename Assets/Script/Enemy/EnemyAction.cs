using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public void Action(EnemyData enemyData)
    {
        if (enemyData.Status == EnemyData.Action.Wait)
        {
            enemyData.nav.stoppingDistance = 100;
            enemyData.WaitTime -= Time.deltaTime;
            return;

        }
        else if (enemyData.Status == EnemyData.Action.chase)
        {
            if (enemyData.PlayerView)
            {
                enemyData.nav.stoppingDistance = enemyData.PlayerDistance;
                if (Vector3.Distance(this.gameObject.transform.position, enemyData.TargetPosition) >= enemyData.RunDistance)
                {//run
                    enemyData.nav.speed = enemyData.RunSpeed;
                }
                else
                {
                    enemyData.nav.speed = enemyData.WalkSpeed;
                }

            }
            else
            {
                enemyData.nav.stoppingDistance = 0;

            }
            enemyData.nav.SetDestination(enemyData.TargetPosition);
            return;
        }
        else if (enemyData.Status == EnemyData.Action.attack)
        {
            enemyData.ThisEnemeyObj.transform.LookAt(enemyData.PlayerObj.transform.position);
            Vector3 vector = enemyData.ThisEnemeyObj.transform.localEulerAngles;
            vector.x = 0;
            enemyData.ThisEnemeyObj.transform.localEulerAngles = vector;
            return;
        }
        else if (enemyData.Status == EnemyData.Action.patrol)
        {
            enemyData.nav.stoppingDistance = 0;
            enemyData.nav.speed = enemyData.WalkSpeed;
            enemyData.nav.SetDestination(enemyData.PatrolPoint[enemyData.Point]);
            if (Vector3.Distance(enemyData.PatrolPoint[enemyData.Point], this.transform.position) < 1)
            {
                if (enemyData.PatrolPoint.Count - 1 <= enemyData.Point)
                {

                    enemyData.Point = 0;
                    return;
                }
                enemyData.Point++;
            }
            return;
        }
    }
}
