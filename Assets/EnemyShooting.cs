using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooting : MonoBehaviour
{
    public GameObject pellet;
    public Transform pelletPos;
    public float range = 4f;

    public float bulletSpeed = 10f;
    private float timer;
    private GameObject player;

    public int damage = 1; 
    public int maxHealth = 3;
    private int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);     // face left
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);   // face right
        }


        
        timer += Time.deltaTime;
        
        if(timer > 2)
        {
            timer = 0;
            shoot();
        }
        
      
        

       
    }

    void shoot()
    {
        
        GameObject newPellet = Instantiate(pellet, pelletPos.position, Quaternion.identity);
        Vector2 dir = (player.transform.position - pelletPos.position).normalized;
        Rigidbody2D rb = newPellet.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.linearVelocity = dir * bulletSpeed;
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
        if(GameManagerLV3.enemiesRemaining > 0)
        {
            GameManagerLV3.enemiesRemaining--;
            GameManagerLV3.instance.CheckForLevelCompletion();
        }
    }

    
}
