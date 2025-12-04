using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class projectile : MonoBehaviour
{
    public int projectileDamage = 1; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyKnight enemy = collision.GetComponent<EnemyKnight>();
        EnemyEyeball enemy2 = collision.GetComponent<EnemyEyeball>();
        EnemyShooting enemy3 = collision.GetComponent<EnemyShooting>();
        EnemyKnightLV3 enemy4 = collision.GetComponent<EnemyKnightLV3>();
        EnemyEyeballLV3 enemy5 = collision.GetComponent<EnemyEyeballLV3>();
        EnemyKnightLV4 enemy6 = collision.GetComponent<EnemyKnightLV4>();

        if(enemy)
        {
            enemy.TakeDamage(projectileDamage); 

            Destroy(gameObject);
        }

        if(enemy2)
        {
            enemy2.TakeDamage(projectileDamage);

            Destroy(gameObject);
        }

        if(enemy3)
        {
            enemy3.TakeDamage(projectileDamage);

            Destroy(gameObject);
        }
        
        if(enemy4)
        {
            enemy4.TakeDamage(projectileDamage);

            Destroy(gameObject);
        }
        
        if(enemy5)
        {
            enemy5.TakeDamage(projectileDamage);

            Destroy(gameObject);
        }
        
        if(enemy6)
        {
            enemy6.TakeDamage(projectileDamage);

            Destroy(gameObject);
        }

    }
}
