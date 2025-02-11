using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;


public class CharacterMovementController
{
    public UnityEvent<Vector2, Bounds> onReceiveMovementInputs;

    public bool destinationReached;
    public float moveSpeed = 10f;
    private Rigidbody2D rb;
    private Transform transform;

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

    public void Initialize(Rigidbody2D rb)
    {
        this.rb = rb;
        transform = rb.transform;
    }

    public void SetSpeed(float speed)
    {
        this.moveSpeed = speed;
    }

    public void MoveCharacterWithDistanceThreshold(Vector3 targetPosition, float hitDistance)
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        if (distance > hitDistance) // Move only if outside stop range
        {
            Vector2 direction = (targetPosition - transform.position).normalized;
            Vector2 newPosition = rb.position + direction * moveSpeed * Time.fixedDeltaTime;

            // Apply movement
            rb.MovePosition(newPosition);

            destinationReached = false;
        }
        else
        {

            destinationReached = true;
        }
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
