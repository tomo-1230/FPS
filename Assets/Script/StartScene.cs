using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public GameObject start;
    public GameObject setting;
    public SettingUI settingUI;
    public static StartScene startScene;

    public void Start()
    {
        startScene = this.gameObject.GetComponent<StartScene>();
    }

    public void OnClick(int a)
    {
        switch (a)
        {
            case 1:
                start.SetActive(false);
                setting.SetActive(true);
                break;
            case 2:
                start.SetActive(true);
                setting.SetActive(false);
                break;
            case 3:
                settingUI.DataSeve();
                SceneManager.LoadScene("Game");
                break;
        }
    }
    public static void Staticback(bool clear)
    {
        SceneManager.LoadScene("Start");
        
    }
    public void back(bool clear)
    {
        SceneManager.LoadScene("Start");
        settingUI.DataLoad();
    }
}
