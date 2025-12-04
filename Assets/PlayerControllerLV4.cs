using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerControllerLV4 : MonoBehaviour
{
    public float jumpForce = 18f;
    public bool isGrounded = false;
    public float currentMoveSpeed;
    public float moveSpeed = 30f;
    public float horizontalMovement;
    bool isFacingRight = false;
    bool isPaused;
    bool doubleJump;
    private SpriteRenderer spriteRend;
    public GameObject player; 
    public GameManagerLV4 gameManager;
    public Transform Aim;

    public Rigidbody2D rigidBody;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        AdjustCharacterSpeed();
    }

    void AdjustCharacterSpeed()
    {
        if(spriteRend != null)
        {
            float spriteWidth = spriteRend.bounds.size.x;
            currentMoveSpeed = moveSpeed * 5 * spriteWidth; 
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        TurnAround();

        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded)
            {
                
                
                rigidBody.linearVelocity = Vector2.up * jumpForce * transform.localScale.y;
                SoundEffectManager.Play("Jump");
                isGrounded = false;
                doubleJump = true; 
                animator.SetTrigger("jump");
                
            }
            else if (doubleJump)  
            {
                
                rigidBody.linearVelocity = Vector2.up * jumpForce * transform.localScale.y;
                SoundEffectManager.Play("Jump");
                doubleJump = false;  
                animator.SetTrigger("jump");
            }
        
        }

        animator.SetFloat("yVelocity", rigidBody.linearVelocity.y);
        animator.SetFloat("magnitude", rigidBody.linearVelocity.magnitude);
        animator.SetFloat("yVelocity", rigidBody.linearVelocity.y);
        
        rigidBody.gravityScale = 7f;

        if(transform.position.y < -55)
        {
            player.SetActive(false);
            gameManager.gameOver();
        }

       
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rigidBody.linearVelocity.y);
        // animator.SetFloat("xVelocity", Mathf.Abs(rigidBody.linearVelocity.x));
        // animator.SetFloat("yVelocity", rigidBody.linearVelocity.y);
    
    }

    void TurnAround()
    {
        if(isFacingRight && horizontalMovement > 0f || !isFacingRight && horizontalMovement < 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;

            // Flip sword/Aim
            Vector3 aimScale = Aim.localScale; 
            aimScale.x *= -1f;
            Aim.localScale = aimScale;

            // Flip sword position left/right
            Vector3 aimPos = Aim.localPosition;
            aimPos.x *= -1f;
            Aim.localPosition = aimPos;

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            // animator.SetBool("isJumping", false);
        }

        if(collision.gameObject.tag.Equals("Spike"))
        {
            player.SetActive(false);
            gameManager.gameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        // animator.SetBool("isJumping", !isGrounded);


        if (collision.CompareTag("Health"))
        {
            HealthLV4 healthScript = GetComponent<HealthLV4>();
            if (healthScript != null)
            {
                healthScript.Heal(20); 
            }

            Destroy(collision.gameObject);
            Debug.Log("Shield collected — health restored!");
        }
    }
}
