using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HP : MonoBehaviour
{
    public Slider HPSlider;
    public Slider DecreaseSlider;
    public Text HPvaule;
    public Image Fill;

    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;
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
        if(value < 1 || value > 100)
        {
            return;
        }
        if(value < 10)
        {
            HPvaule.text = "0" + value.ToString();
        }
        else
        {
            HPvaule.text = value.ToString();
        }

        float R = 255;
        float G = 255;
        if (value > 51)//—Î
        {
            // Debug.Log("A");
            R = 0;
            G = 255;
        }
        else if (value > 26 && value <= 51)//—Î@‰©  51=255  26=0 
        {

            float x = value - 26;
            x = 25 - x;
            R = x * 10.2f;
            G = 255;
            //Debug.Log("B" + x);
        }
        else if (value <= 26)//‰©@Ô
        {

            float x = value;
            R = 255;
            G = x * 10.2f;
            // Debug.Log("C" + x );
        }
        else
        {
            Debug.Log("HPColorError");
        }
       // Debug.Log(float.Parse(R.ToString("f0")) + " " + float.Parse(G.ToString("f0")));
        Color fillColor = new Color(0f,0f,0f);
        fillColor.r = float.Parse(R.ToString("f0"))/ 255f;
        fillColor.g = float.Parse(G.ToString("f0"))/ 255f;
        fillColor.b = 0f/ 255f;
        Fill.color = fillColor;

        HPSlider.value = value;

    }

  
    public int Decrease(int HP,int damage) 
    {
        HP -= damage;
        //float time = 0; 
        //while (time >= 1f)
        //{
        //    time += Time.deltaTime;
        //}

        //do
        //{
        //    DecreaseSlider.value--;
        //} while (DecreaseSlider.value == HP);
        transform.DOShakePosition(duration / 2f, strength, vibrate);
        return HP;
    }
}
