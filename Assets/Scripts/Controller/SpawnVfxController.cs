using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnVfxController : MonoBehaviour
{
    public VisualEffectsDatabase database;
    public List<GameObject> playerDamagePool = new List<GameObject>();

    public void SpawnEffects(Vector2 position, string vfxName)
    {
        GameObject vfxGo = null;
        Debug.Log("Once Only!");
        if (playerDamagePool.Count == 0 || playerDamagePool.All(x => x.activeSelf))
        {
            GameObject prefabVfx = database.GetVisualEffect(vfxName);

            vfxGo = GameObject.Instantiate(prefabVfx, position, Quaternion.identity);

            playerDamagePool.Add(vfxGo);
        }
        else
        {
            vfxGo = playerDamagePool.First(x => !x.activeSelf);
        }

        vfxGo.transform.position = position;
        vfxGo.SetActive(true);
    }
}
