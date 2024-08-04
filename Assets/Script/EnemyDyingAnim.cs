using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class EnemyDyingAnim : MonoBehaviour
{
    async public void Anim(Material BasicMaterial, SkinnedMeshRenderer Enemy,GameObject thisObj)
    {
        Material CloneMaterial = new Material(BasicMaterial);
        Enemy.material = CloneMaterial;
        float i = 1;
        if(thisObj == null)
        {
            return;
        }
        do
        {
            Color color = CloneMaterial.color;
            color.a = i;
            CloneMaterial.color = color;
            i -= 0.01f;
            await Task.Delay(1);

        } while (i >= 0);
        Destroy(thisObj);
        
    }
}
