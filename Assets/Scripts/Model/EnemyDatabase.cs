using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Database", menuName = "ScriptableObjects/Enemy Data")]
public class EnemyDatabase : ScriptableObject
{
    public List<CharacterInfoBase> enemyInfoBase;

    public CharacterInfoBase GetNewCharacterInfo()
    {
        int randIdx = Random.Range(0, enemyInfoBase.Count);

        return new CharacterInfoBase(enemyInfoBase[randIdx]);
    }
}
