using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyKnightDamageLV3 : MonoBehaviour
{

    public HealthLV3 playerHealth;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.CompareTag("Player"))
        {
           obj.gameObject.GetComponent<HealthLV3>().health -= damage;
        }
    }
}

