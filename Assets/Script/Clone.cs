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
    public List<GameObject> ClonedEnemyObj = new List<GameObject>();
    private bool addition = false;
    public Clone clone;
    // Start is called before the first frame update
    void OnEnable()
    {
        EnemyAmount = PlayerPrefs.GetInt("EnemyAmount");
        foreach (int i in UsePoint)
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
            random2 = UsePoint[random];
            UsePoint.RemoveAt(random);
            Transform[] chilleds = new Transform[PointObj[random2].transform.childCount];
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
                enemy.nav.Warp(chilleds[Random.Range(0, chilleds.Length)].position);
            }
            else
            {
                enemy.nav.Warp(chilleds[0].position);
            }

            enemy.Status = Enemy.Action.patrol;
            ClonedEnemyObj.Add(CloneObject);
            enemy.ListNumber = ClonedEnemyObj.Count - 1;
            a++;
        } while (a <= EnemyAmount - 1);


    }

    // Update is called once per frame
    void Update()
    {
        if (ClonedEnemyObj.Count == 0)
        {
            StartScene.back(true);
        }
    }
    public void Removed(int value)
    {

        ClonedEnemyObj.RemoveAt(value);
        if (ClonedEnemyObj.Count == 0)
        {
            return;
        }
        int i = 0;
        foreach (GameObject a in ClonedEnemyObj)
        {
            a.GetComponent<Enemy>().ListNumber = i;
        }
    }
    public static void GetList(ref List<GameObject> a)
    {

        /// a = new List<GameObject>(clone.ClonedEnemyObj);
    }
}
