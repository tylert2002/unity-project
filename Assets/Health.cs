using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDataPersistence
{
    public float health;
    public float maxHealth; 
    public Image healthBar;
    private bool isDead;
    public GameObject player;

    public GameManagerScript gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
    }

    public void LoadData(GameData data)
    {
        this.health = data.health;
        this.maxHealth = data.maxHealth;
    }

    public void SaveData(ref GameData data)
    {
        data.health = this.health;
        data.maxHealth = this.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if(health <= 0 && !isDead)
        {
            isDead = true;
            player.SetActive(false);
            gameManager.gameOver();
            Debug.Log("Dead");
        }
    }
}
