using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBulletData : MonoBehaviour
{
    public List<int> SetBulletList = new List<int>();
    public void Clear()
    {
        SetBulletList.Clear();
        SetBulletList.Add(-1);
        SetBulletList.Add(-1);
        SetBulletList.Add(-1);
        SetBulletList.Add(-1);
    }
    public void NewCloneGun(int index, GameObject gameObject)
    {
        if (gameObject.GetComponent<Item>() == null)
        {
            Debug.Log("SetBulletData.NewCloneGun Error");
            return;
        }

        Item item = gameObject.GetComponent<Item>();
        SetBulletList[index] = item.SetBullet;
    }
    public void Overwriting(int index, int SetBulletVGunalue)
    {
        if (SetBulletList[index] == -1)
        {
            Debug.Log("SetBulletData.Overwriting Error");
            return;
        }
        SetBulletList[index] = SetBulletVGunalue;
    }
    public int GetSetBulletValue(int index)
    {
        return SetBulletList[index];
    }
    public void RemoveData(int index)
    {
        SetBulletList[index] = -1;
    }
    public bool IsExistsData(int index)
    {
        return SetBulletList[index] != -1;
    }
}
