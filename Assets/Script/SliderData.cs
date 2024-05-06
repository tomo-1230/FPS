using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderData : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public int Data;
    public SettingUI settingUI;
    // Start is called before the first frame update
    void Start()
    {
        if(slider == null)
        {
            // this.gameObject.GetComponent<Dropdown>().value = int.Parse(settingUI.SettingData[Data]);
            this.gameObject.GetComponent<Dropdown>().value = (int)settingUI.SettingData[Data];
        }
        else
        {
            slider.value = settingUI.SettingData[Data];

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (settingUI.auto.isOn &&!(Data == 0 || Data == 1 ||Data == 8 || 10<Data))
        {
           
            if (slider == null)
            {
                settingUI.Slider(this.gameObject.GetComponent<Dropdown>().value, Data);
            }
            else
            {
                slider.value = settingUI.SettingData[Data];
                settingUI.DisplayText(slider.value, text);
                settingUI.Slider((int)slider.value, Data);
            }
        }
        else
        {
            if (slider == null)
            {
                settingUI.Slider(this.gameObject.GetComponent<Dropdown>().value, Data);
            }
            else
            {
                if(Data == 9)
                {
                    settingUI.DisplayText(slider.value, text);
                    settingUI.Slider((int)(slider.value*100), Data);
                }
                else
                { 
                    settingUI.DisplayText(slider.value, text);
                    settingUI.Slider((int)slider.value, Data);

                }
               
            }
        }
       
       
    }
}
