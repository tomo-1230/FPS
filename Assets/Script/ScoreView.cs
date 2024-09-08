using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
public class ScoreView : MonoBehaviour
{
    public GameObject ShotScore;
    public GameObject HitScore;
    public GameObject HedShotScore;
    public GameObject HitRateScore;
    public GameObject HeadShotRateScore;
    public GameObject TimeScore;
    public GameObject PerOnePersonTimeScore;
    public GameObject MovePosition;
    public Text Game;
    public Button clause;
    public void Start()
    {
        if(PlayerPrefs.GetInt("win") == 1)
        {
            Game.text = "GameClear";
        }
        else
        {
            Game.text = "GameOver";
        }
        view(true);   
    }
    async void view(bool clear)
    {
        ScoreMove(ShotScore, PlayerPrefs.GetInt("ShotCount") + "‰ñ");
        await Task.Delay(500);
        ScoreMove(HitScore, PlayerPrefs.GetInt("HitCount") + "‰ñ");
        await Task.Delay(500);
        ScoreMove(HedShotScore, PlayerPrefs.GetInt("HedShotCount") + "‰ñ");
        await Task.Delay(500);
        float rate = (float)PlayerPrefs.GetInt("HitCount") / (float)PlayerPrefs.GetInt("ShotCount");
        ScoreMove(HitRateScore,((int)(rate*100) + "%"));
        Debug.Log(PlayerPrefs.GetInt("HitCount")+":"+PlayerPrefs.GetInt("ShotCount"));
        await Task.Delay(500);
        rate = (float)PlayerPrefs.GetInt("HedShotCount") / (float)PlayerPrefs.GetInt("HitCount");
        ScoreMove(HeadShotRateScore, ((int)(rate * 100) + "%"));
        await Task.Delay(500);
        int m = (int)(PlayerPrefs.GetFloat("time") / 60);
        int s = (int)(PlayerPrefs.GetFloat("time") % 60);
        string Time;
        if (m == 0)
        {
            Time = s + "•b";
        }
        else
        {
            Time = m + "•ª" + s + "•b";
        }
        ScoreMove(TimeScore,Time);
        await Task.Delay(500);
        int PerOnePersonTime = (int)PlayerPrefs.GetFloat("time") / PlayerPrefs.GetInt("EnemyAmount");
        m = PerOnePersonTime / 60;
        s = PerOnePersonTime % 60;
        if (m == 0)
        {
            Time = s + "•b";
        }
        else
        {
            Time = m + "•ª" + s + "•b";
        }
        ScoreMove(PerOnePersonTimeScore, Time);
    }
    public void ScoreMove(GameObject Score,string value)
    {
        Text valueText = Score.transform.Find("value").gameObject.GetComponent<Text>();
        valueText.text = value;
        Score.SetActive(true);
    }
}
