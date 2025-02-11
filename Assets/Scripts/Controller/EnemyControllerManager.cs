using Controller;
using Model;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerManager : MonoBehaviour
{
    public UnityEvent OnEnemyHitEvent;
    public UnityEvent<EnemyControllerManager> OnEnemyHitVfxEvent;

    public CharacterInfoBase enemyInfo;
    public PlayerControllerManager targetPlayer;

    public CharacterMovementController movement;

    [Header("VFX")]
    public string onDeathVFX;
    public string onDamagedVFX;

    [Header("Enemy Mechanics")]
    public float hitDistance = 1.0f;
    public float interval = 1.5f;
    public float curInterval = 0.0f;

    public bool isAlive = false;

    Rigidbody2D rb;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new CharacterMovementController();
        movement.Initialize(rb);
    }

    public void InitializeInfo(CharacterInfoBase newEnemyInfo, PlayerControllerManager newTargetPlayer)
    {
        isAlive = true;
        curInterval = 0.0f;
        enemyInfo = newEnemyInfo;
        targetPlayer = newTargetPlayer;
        movement.SetSpeed(enemyInfo.Speed);
    }

    public void Update()
    {
        movement.MoveCharacterWithDistanceThreshold(targetPlayer.transform.position, hitDistance);

        if(curInterval > 0.0f)
        {
            curInterval -= Time.deltaTime;
        }

        if (!movement.destinationReached) return;

        if(curInterval <= 0.0f)
        {
            targetPlayer.ReceiveDamage(1);
            curInterval += interval;
        }
    }


    public void OnBulletHit()
    {
        //Note: to simplify the gameplay, you may see that the enemy database contains health, but I simply didnt use it anymore
        // not required in the documentation.
        OnEnemyHitVfxEvent?.Invoke(this);
        OnEnemyHitVfxEvent.RemoveAllListeners();
        OnEnemyHitEvent?.Invoke();
        OnEnemyHitEvent?.RemoveAllListeners();

        isAlive = false;
        this.gameObject.SetActive(false);
    }
}
