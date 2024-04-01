using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP : MonoBehaviour
{
    public Slider HPSlider;
    public Text HPvaule;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerHP(float value)
    {
        if(value > 10)
        {
            HPvaule.text = "0" + value.ToString();
        }
        else
        {
            HPvaule.text = value.ToString();
        }
    }
}
