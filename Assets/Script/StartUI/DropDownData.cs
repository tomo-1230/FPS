using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownData : MonoBehaviour
{
    public SettingUI settingUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        settingUI.Slider(this.GetComponent<DropDownControl>().value, 17);
    }
}
