using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownControl : MonoBehaviour
{
    public List<string> Options;
    public int value;
    public Text ViewName;
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        SetValue(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValue(int a)
    {
        value = a;
        if (value > Options.Count-1)
        {
            value = Options.Count -1;
            Debug.LogError("DropDownControlError");
        }
        if(value < 0)
        {
            value = 0;
            Debug.LogError("DropDownControlError");
        }
        ViewName.text = Options[value];
    }
    public void OnClick(int Increase)
    {
        value += Increase;
        if(value > Options.Count-1)
        {
            value = 0;
        }
        if(value < 0)
        {
            value = Options.Count - 1;
        }
        ViewName.text = Options[value];
    }

}
