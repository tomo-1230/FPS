using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int ShotCount = 0;
    public int HitCount = 0;
    public int HedShotCount = 0;
    public float time = 0;
    public static ScoreCounter Count;
    public void Start()
    {
        Count = this.gameObject.GetComponent<ScoreCounter>();
    }
    public void Update()
    {
        time += Time.deltaTime;
    }
    public void Shot()
    {
        ShotCount++;
    }
    public void Hit(bool HedShot)
    {
        HitCount++;
        if (HedShot)
        {
            HedShotCount++;
        }
    }
    public voidÅ@save()
    {
        PlayerPrefs.SetInt("ShotCount", ShotCount);
        PlayerPrefs.SetInt("HitCount", HitCount);
        PlayerPrefs.SetInt("HedShotCount", HedShotCount);
        PlayerPrefs.SetFloat("time", time);
        PlayerPrefs.Save();
    }
}
