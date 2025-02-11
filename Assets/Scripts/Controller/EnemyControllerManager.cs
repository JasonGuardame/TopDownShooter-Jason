using Model;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyControllerManager : MonoBehaviour
{
    public CharacterInfoBase enemyInfo;

    [Header("Enemy Mechanics")]
    public float hitDistance = 1.0f;
    public float interval = 1.5f;
    public float curInterval = 0.0f;

    public void InitializeInfo(CharacterInfoBase newEnemyInfo)
    {
        enemyInfo = newEnemyInfo;
    }

    public void Update()
    {

    }

    public void OnBulletHit()
    {
        this.gameObject.SetActive(false);
    }
}
