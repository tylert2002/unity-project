using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public int damage = 5;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyKnightLV4 enemy = collision.GetComponent<EnemyKnightLV4>();
        EnemyEyeballLV4 enemy2 = collision.GetComponent<EnemyEyeballLV4>();
        EnemyShootingLV4 enemy3 = collision.GetComponent<EnemyShootingLV4>();
        GuardianEyeLV4 enemy4 = collision.GetComponent<GuardianEyeLV4>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if(enemy2 != null)
        {
            enemy2.TakeDamage(damage);
        }
        if(enemy3 != null)
        {
            enemy3.TakeDamage(damage);
        }
        if(enemy4 != null)
        {
            enemy4.TakeDamage(damage);

        }
        
    }
}
