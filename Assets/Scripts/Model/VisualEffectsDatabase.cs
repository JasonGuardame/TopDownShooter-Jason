using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Vfx_Database", menuName = "ScriptableObjects/Vfx")]
public class VisualEffectsDatabase : ScriptableObject
{
    public List<VisualEffectsModel> vfxModels;

    public GameObject GetVisualEffect(string vfxName)
    {
        return vfxModels.First(x => x.name == vfxName).vfx;
    }
}
