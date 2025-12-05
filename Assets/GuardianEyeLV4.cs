using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuardianEyeLV4 : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    public Transform target; // Player
    private Vector2 moveDirection;

    public float followRange = 8f;     // Distance where eyeball starts chasing
    public float stopChaseRange = 12f; // Distance where eyeball returns to idle

    private Vector3 idlePosition;      // Original position to return to

    public int damage = 1;
    public int maxHealth = 3;
    private int currentHealth;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        idlePosition = transform.position; // Save starting position
    }

    void Start()
    {
        // Auto find player if not assigned
        if (target == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    void Update()
    {
        if (target == null)
            return;

        float distance = Vector2.Distance(transform.position, target.position);

        // If player is close: Chase
        if (distance <= followRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
        // If player is far: Return to idle position
        else if (distance > stopChaseRange)
        {
            Vector3 direction = (idlePosition - transform.position).normalized;
            moveDirection = direction;

            // If very close to idle position, stop moving
            if (Vector2.Distance(transform.position, idlePosition) < 0.2f)
                moveDirection = Vector2.zero;
        }

        // Flip sprite for correct facing direction
        if (spriteRenderer != null && moveDirection.x != 0)
        {
            spriteRenderer.flipX = moveDirection.x < 0;
        }
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = moveDirection * moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
            
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (GameManagerLV4.enemiesRemaining > 0)
        {
            GameManagerLV4.enemiesRemaining--;
            GameManagerLV4.instance.CheckForLevelCompletion();
        }
    }
}
