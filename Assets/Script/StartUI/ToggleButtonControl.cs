using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ToggleButtonControl : MonoBehaviour
{
    public GameObject ONButton;
    public GameObject OFFButton;
    public GameObject Background;
    public Text OFFText;
    public Text ONText;
    public bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        ChangeStatus(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(float MoveTime = 0.3f)
    {
        
        if (!isOn)
        {
            Background.transform.DOMoveX(transform.TransformPoint(ONButton.transform.localPosition).x, MoveTime, true);
            Color TextColor =  ONText.color;
            TextColor.a = 1f;
            ONText.color = TextColor ;
            TextColor = OFFText.color;
            TextColor.a = 0.3f;
            OFFText.color = TextColor;
        }
        else
        {
            Background.transform.DOMoveX(transform.TransformPoint(OFFButton.transform.localPosition).x, MoveTime, true);
            Color TextColor = ONText.color;
            TextColor.a = 0.3f;
            ONText.color = TextColor;
            TextColor = OFFText.color;
            TextColor.a = 1f;
            OFFText.color = TextColor;
        }
       
        isOn = !isOn;

        
    }
    public void ChangeStatus(bool a)
    {
       if(a != isOn)
       {
            OnClick(0);
       }
       
    }
}
