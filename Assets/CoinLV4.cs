using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinLV4 : MonoBehaviour
{
    [SerializeField] private int value;
    private bool hasTriggered;
    
    private CoinManager coinManager;

    private void Start()
    {
        coinManager = CoinManager.instance; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            coinManager.ChangeCoins(value);
            Destroy(gameObject);
            
            GameManagerLV4.instance.CheckForLevelCompletion();
        }
    }
}
