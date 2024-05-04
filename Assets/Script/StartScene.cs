using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public GameObject start;
    public GameObject setting;
    public SettingUI settingUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
