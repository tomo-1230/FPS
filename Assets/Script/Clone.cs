using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    public GameObject CloneEnemy;
    public GameObject PlayerObject;
    public GameObject target;
    public int EnemyAmount;
    public List<GameObject> PointObj;
    public List<int> UsePoint;
    // Start is called before the first frame update
    void Start()
    {
        
        int a = 0;
        do
        {
            GameObject CloneObject = Instantiate(CloneEnemy);
            Enemy enemy = CloneObject.GetComponent<Enemy>();
            enemy.PlayerObject = PlayerObject;
            enemy.Target = target;
            enemy.player = PlayerObject.GetComponent<Player>();
            int random;
            int random2;
            random = Random.Range(0, UsePoint.Count);
            random2 = UsePoint[random];
            UsePoint.RemoveAt(random);
            Transform[] chilleds = new Transform[PointObj[random2].transform.childCount];
            //enemy.nav.Warp(chilleds[1].position);
            for (int i = 0; i < PointObj[random2].transform.childCount; i++)
            {
                chilleds[i] = PointObj[random2].transform.GetChild(i);
            }
            enemy.PatrolPoint.Clear();
            foreach (Transform b in chilleds)
            {
                enemy.PatrolPoint.Add(b.position);
            }
            enemy.nav.Warp(chilleds[0].position);
            enemy.Situation = Enemy.Action.patrol;
            a++;
        } while (a <= EnemyAmount-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
