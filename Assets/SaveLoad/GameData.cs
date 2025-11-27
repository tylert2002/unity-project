using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class GameData
{
    public float health;
    public float maxHealth;
    public Vector3 playerPosition;
    public string currentSceneName;
    public int enemiesRemaining;

    public GameData()
    {
        this.health = 100;
        playerPosition = new Vector3(-85.8f, -5.4f, -0.009915f);
    } 
}
