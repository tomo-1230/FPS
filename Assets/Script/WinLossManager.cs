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
   public void Win()
   {
        WimUI.SetActive(true);
   }
   public void Loss()
   {
        LostUI.SetActive(true);
   }
    public void OnClick(bool Clear)
    {
        SceneManager.LoadScene("Score");
    }
}
