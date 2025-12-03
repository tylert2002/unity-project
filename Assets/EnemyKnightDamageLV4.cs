using UnityEngine;

public class EnemyKnightDamageLV4 : MonoBehaviour
{
    public HealthLV4 playerHealth;
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
           obj.gameObject.GetComponent<HealthLV4>().health -= damage;
        }
    }
}
