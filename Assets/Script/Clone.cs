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
    public List<int> copy;
    public static List<GameObject> ClonedEnemyObj = new List<GameObject>();
    private bool addition = false;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAmount = PlayerPrefs.GetInt("EnemyAmount");
       foreach(int i in UsePoint)
       {
            copy.Add(i);
       }
        
        int a = 0;
        do
        {
            if (UsePoint.Count == 0)
            {
                addition = true;
                foreach (int i in copy)
                {
                    UsePoint.Add(i);
                }
            }
            GameObject CloneObject = Instantiate(CloneEnemy);
            Enemy enemy = CloneObject.GetComponent<Enemy>();
            enemy.PlayerObject = PlayerObject;
            enemy.Target = target;
            enemy.player = PlayerObject.GetComponent<Player>();
            int random;
            int random2;
            random = Random.Range(0, UsePoint.Count);
           // Debug.Log(random);
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
            if (addition)
            {
                enemy.nav.Warp(chilleds[Random.Range(0,chilleds.Length)].position);
            }
            else
            {
                enemy.nav.Warp(chilleds[0].position);
            }
           
            enemy.Situation = Enemy.Action.patrol;
            ClonedEnemyObj.Add(CloneObject);
            a++;
        } while (a <= EnemyAmount-1);
    }

    // Update is called once per frame
    void Update()
    {
        if(ClonedEnemyObj.Count == 0)
        {
            StartScene.back(true);
        }
    }
}
