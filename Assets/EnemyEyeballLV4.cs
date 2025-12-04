using UnityEngine;

public class EnemyEyeballLV4 : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rigidBody;
    public Transform target;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    public int damage = 1; 
    public int maxHealth = 3;
    private int currentHealth;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        // Optionally find the player automatically if not assigned in Inspector
        if (target == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            
            if (spriteRenderer != null)
            {
                if (direction.x > 0)
                    spriteRenderer.flipX = false;  
                else if (direction.x < 0)
                    spriteRenderer.flipX = true;   
            }

            
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rigidBody.linearVelocity = moveDirection * moveSpeed;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        if(currentHealth <= 0)
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
        if(GameManagerLV4.enemiesRemaining > 0)
        {
            GameManagerLV4.enemiesRemaining--;
            GameManagerLV4.instance.CheckForLevelCompletion();
        }
    }
}
