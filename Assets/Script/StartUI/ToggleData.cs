using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleData : MonoBehaviour
{
    public int ListNumber;
    public SettingUI settingUI;
    private ToggleButtonControl toggle;
    private int isOn;
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.GetComponent<ToggleButtonControl>() != null)
        {
            toggle = this.gameObject.GetComponent<ToggleButtonControl>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle == null || settingUI == null)
        {
            Debug.LogError("ToggleError");
            return;
        }
        if (toggle.isOn)
        {
            isOn = 1;
        }
        else
        {
            isOn = 0;
        }
        settingUI.Slider(isOn, ListNumber);
    }
}
