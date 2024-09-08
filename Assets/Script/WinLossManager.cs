using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
public class WinLossManager : MonoBehaviour
{
    public GameObject WimUI;
    public GameObject LostUI;
    public ScoreCounter ScoreCounter;
   public void Win()
   {
        WimUI.SetActive(true);
        PlayerPrefs.SetInt("win",1);
        PlayerPrefs.Save();
   }
   public void Loss()
   {
        LostUI.SetActive(true);
        PlayerPrefs.SetInt("win", 0);
        PlayerPrefs.Save();
    }
    public void OnClick(bool Clear)
    {
        ScoreCounter.save();
        SceneManager.LoadScene("Clear");
    }
}
