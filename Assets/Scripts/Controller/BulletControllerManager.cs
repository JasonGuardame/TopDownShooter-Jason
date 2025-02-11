using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BulletControllerManager : MonoBehaviour
{
    [ReadOnly]public Vector2 curDirection;
    private float speed;
    public float lifetime = 3f; // Bullet despawns after this time

    [SerializeField]private Rigidbody2D rb;

    void Start()
    {
        // Destroy the bullet after some time to prevent memory leaks
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        rb.velocity = curDirection * speed;
    }

    public void Initialize(Vector2 direction, float newSpeed)
    {
        this.speed = newSpeed;
        curDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyControllerManager enemy = collision.GetComponent<EnemyControllerManager>();

        if (enemy == null) return;

        // Try getting the enemy's health component
        if (enemy != null)
        {
            enemy.OnBulletHit(); // Apply damage
        }

        Destroy(this.gameObject); // Destroy the bullet on impact
    }
}
