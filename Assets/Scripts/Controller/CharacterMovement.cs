using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CharacterMovement
{
    public UnityEvent<Vector2> onReceiveMovementInputs;

    [ReadOnly]public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

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
        onReceiveMovementInputs.RemoveAllListeners();
    }

    public void Initialize(Rigidbody2D rb, float moveSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    public void MoveCharacter(Vector2 direction)
    {
        rb.velocity = direction.normalized * moveSpeed;
    }
}
