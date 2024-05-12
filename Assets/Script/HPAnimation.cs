using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
public class HPAnimation : MonoBehaviour
{
    public GameObject Player;
    public Animator anim;
    public int WaitTime;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
       // anim.SetBool("move", false);
        text = this.gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
           // this.transform.LookAt(Player.transform.position);
        }

    }
    public async  void Damagae(int DamageValue,Color TextColor)
    {
        text.color = TextColor;
        text.text = DamageValue.ToString();
        //anim.SetBool("move", true);
        await Task.Delay(WaitTime);
       // anim.SetBool("move", false);
    }
}
