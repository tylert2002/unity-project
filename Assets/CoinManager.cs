using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [SerializeField] private TMP_Text coinsText;
    private int coins;
    public int totalCoins;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        
        
    }

    /*private void OnGUI()
    {
        coinsText.text = coins.ToString();
    }*/

    public void ChangeCoins(int amount)
    {
        coins += amount;
        coinsText.text = coins.ToString();
    }

    public bool AllCoinsCollected()
    {
        return coins >= totalCoins;
    }

}
