using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class CharacterMovementController
{
    public UnityEvent<Vector2, Bounds> onReceiveMovementInputs;

    [ReadOnly]public float moveSpeed = 10f;
    private Rigidbody2D rb;

    public void Awake()
    {
        RegisterEvents();
    }

    public void OnDestroy()
    {
        UnRegisterEvents();
    }

    void RegisterEvents()
    {
        onReceiveMovementInputs.AddListener(MoveCharacter);
    }

    void UnRegisterEvents()
    {
        if(onReceiveMovementInputs != null)
        {
            onReceiveMovementInputs.RemoveListener(MoveCharacter);
        }
    }

    public void Initialize(Rigidbody2D rb, float moveSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    public void MoveCharacter(Vector2 direction, Bounds bounds)
    {
        // Move player
        Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

        // Clamp position within BoxCollider2D bounds
        newPosition.x = Mathf.Clamp(newPosition.x, bounds.min.x, bounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.min.y, bounds.max.y);

        // Apply movement
        rb.MovePosition(newPosition);
    }
}
