using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyCounter : MonoBehaviour
{
    public int EnemyCount;
    public Text text;
    public int MaxValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Žc‚è‚Ì“G" + EnemyCount + "/" + MaxValue;
    }
    public void EnemyKill()
    {
        EnemyCount--;
    }
    public void SetCount(int EnemeyClonedCount)
    {
        EnemyCount = EnemeyClonedCount;
        MaxValue = EnemeyClonedCount;
    }
}
