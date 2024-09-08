using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SettingUI : MonoBehaviour
{
    [Header("上のボタン")]
    [Space(1)]
    public List<GameObject> Button;
    public List<GameObject> Obj;
    public GameObject back;
    [Space(3)]
    [Header("All")]
    [Space(1)]
    public ToggleButtonControl auto;
    public Slider sDifficulty;
    public Text tDifficulty;
    public List<int> SettingData;
    //public static List<int> SettingDataCopy;
    public Color color;
    [Space(3)]
    [Header("LevelData")]
    [Space(1)]
    public LevelData PlayerHP;
    public LevelData EnemyAmount;
    public LevelData EnemyMoveSpeed;
    public LevelData EnemyView;
    public LevelData EnemyHP;
    public LevelData OffensePower;
    public LevelData EnemyAIM;
    public LevelData ItemAmount;
    private int Level;
    private GameObject ShowButton = null;
    private GameObject ShowObj = null;
    // Start is called before the first frame updateo
    void Awake()
    {
        ButtonClick(3);
        ButtonClick(2);
        ButtonClick(1);
        ButtonClick(0);
        for (int a = 0; a > 20; a++)
        {
            SettingData.Add(0);
        }
        sDifficulty.value = 10;
        Level = 0;
        SettingData[2] = PlayerHP.Data[Level];
        SettingData[3] = EnemyAmount.Data[Level];
        SettingData[4] = EnemyMoveSpeed.Data[Level];
        SettingData[5] = EnemyView.Data[Level];
        SettingData[6] = EnemyHP.Data[Level];
        SettingData[7] = OffensePower.Data[Level];
        SettingData[9] = EnemyAIM.Data[Level];
        SettingData[10] = ItemAmount.Data[Level];

    }

    // Update is called once per frame
    void Update()
    {
        Level = (int)sDifficulty.value - 1;
        if (auto.isOn)
        {
            sDifficulty.GetComponent<SliderData>().enabled = true;
            // DisplayText(S_difficulty, T_difficulty);
        }
        else
        {
            sDifficulty.GetComponent<SliderData>().enabled = false;
            tDifficulty.text = "カスタム";
        }
        //All
        if (auto.isOn)
        {
            SettingData[2] = PlayerHP.Data[Level];
            SettingData[3] = EnemyAmount.Data[Level];
            SettingData[4] = EnemyMoveSpeed.Data[Level];
            SettingData[5] = EnemyView.Data[Level];
            SettingData[6] = EnemyHP.Data[Level];
            SettingData[7] = OffensePower.Data[Level];
            SettingData[9] = EnemyAIM.Data[Level];
            SettingData[10] = ItemAmount.Data[Level];
            SettingData[11] = 5;
            SettingData[12] = 5;
            SettingData[13] = 5;
            SettingData[14] = 5;
            SettingData[15] = 5;
            SettingData[16] = 5;
        }

    }
    public void DisplayText(float sli, Text text)
    {


        text.text = sli.ToString();

    }
    public void ButtonClick(int number)
    {
        GameObject ClickButton = Button[number];
        GameObject IndicationObj = Obj[number];
        if (ShowButton == ClickButton)
        {
            return;
        }
        Button button = ClickButton.GetComponent<Button>();
        back.transform.DOMoveX(ClickButton.transform.position.x, 0.3f);
        //ColorBlock colorBlock = button.colors;
        //colorBlock.normalColor = Color.white;
        //button.colors = colorBlock;
        IndicationObj.SetActive(true);
        if (ShowButton != null && ShowObj != null)
        {
            //button = ShowButton.GetComponent<Button>();
            //colorBlock = button.colors;
            //colorBlock.normalColor = color;
            //button.colors = colorBlock;
            ShowObj.SetActive(false);
        }
        ShowButton = ClickButton;
        ShowObj = IndicationObj;
    }
    public void Slider(int sliderdData, int Data)
    {
        SettingData[Data] = sliderdData;
    }
    public void DataSeve()
    {
        PlayerPrefs.SetInt("PlayerHP", SettingData[2]);
        PlayerPrefs.SetInt("CloneItem", SettingData[10]);
        PlayerPrefs.SetInt("EnemyAmount", SettingData[3]);
        PlayerPrefs.SetInt("WalkSpeed", SettingData[4]);
        PlayerPrefs.SetInt("Ray", SettingData[5]);
        PlayerPrefs.SetInt("aim", SettingData[9]);
        PlayerPrefs.SetInt("EnemyHP", SettingData[6]);
        PlayerPrefs.SetInt("0", SettingData[11]);
        PlayerPrefs.SetInt("1", SettingData[12]);
        PlayerPrefs.SetInt("2", SettingData[13]);
        PlayerPrefs.SetInt("3", SettingData[14]);
        PlayerPrefs.SetInt("4", SettingData[15]);
        PlayerPrefs.SetInt("5", SettingData[16]);
        PlayerPrefs.SetInt("EnemyCloneGun", SettingData[17]);
        PlayerPrefs.SetInt("BulletInfinite", SettingData[18]);

        PlayerPrefs.Save();
    }
    public void DataLoad()
    {
        SettingData[2] =  PlayerPrefs.GetInt("PlayerHP");
        SettingData[10] =  PlayerPrefs.GetInt("CloneItem");
        SettingData[3] =  PlayerPrefs.GetInt("EnemyAmount");
        SettingData[4] =  PlayerPrefs.GetInt("WalkSpeed");
        SettingData[5] =  PlayerPrefs.GetInt("Ray");
        SettingData[9] =  PlayerPrefs.GetInt("aim");
        SettingData[6] =  PlayerPrefs.GetInt("EnemyHP");
        SettingData[11] =  PlayerPrefs.GetInt("0");
        SettingData[12] =  PlayerPrefs.GetInt("1");
        SettingData[13] =  PlayerPrefs.GetInt("2");
        SettingData[14] =  PlayerPrefs.GetInt("3");
        SettingData[15] =  PlayerPrefs.GetInt("4");
        SettingData[16] =  PlayerPrefs.GetInt("5");
        SettingData[17] = PlayerPrefs.GetInt("EnemyCloneGun");
        SettingData[18] = PlayerPrefs.GetInt("BulletInfinite");
        
    }

}
