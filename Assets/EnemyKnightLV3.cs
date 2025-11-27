using UnityEngine;

public class EnemyKnightLV3 : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rigidBody;
    private Transform currentPoint;
    public float enemySpeed = 1f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool wasGrounded; // track previous frame’s ground state
    private bool touchingPlayer; // flag to prevent jumping when colliding with player
    private float scaledEnemySpeed;
    private float lastJumpTime = 0f;
    public float jumpCooldown = 0.2f; // seconds

    public int damage = 1; 
    public int maxHealth = 5;
    private int currentHealth;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        currentHealth = maxHealth;

        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        if (spriteRend != null)
        {
            float spriteWidth = spriteRend.bounds.size.x;
            scaledEnemySpeed = enemySpeed * spriteWidth;
        }
        else
        {
            scaledEnemySpeed = enemySpeed;
        }
    }

    void Update()
    {
        // Check ground status
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Patrol movement
        if (currentPoint == pointB.transform)
        {
            if (transform.position.x < pointB.transform.position.x)
            {
                rigidBody.linearVelocity = new Vector2(scaledEnemySpeed, rigidBody.linearVelocity.y);
            }
            else
            {
                flip();
                currentPoint = pointA.transform;
            }
        }
        else
        {
            if (transform.position.x > pointA.transform.position.x)
            {
                rigidBody.linearVelocity = new Vector2(-scaledEnemySpeed, rigidBody.linearVelocity.y);
            }
            else
            {
                flip();
                currentPoint = pointB.transform;
            }
        }

        // Jump only when landing and not touching player
        if (isGrounded && !wasGrounded && !touchingPlayer && Time.time - lastJumpTime > jumpCooldown)
        {
            Jump();
            lastJumpTime = Time.time;
        }

        // Remember last frame’s grounded state
        wasGrounded = isGrounded;
    }

    

    void Jump()
    {
        rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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
        if(GameManagerLV3.enemiesRemaining > 0)
        {
            GameManagerLV3.enemiesRemaining--;
            GameManagerLV3.instance.CheckForLevelCompletion();
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
            Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
            Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        }

        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    // Detect collision with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }
}
