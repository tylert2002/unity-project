using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManagerLV3 : MonoBehaviour
{
    public GameObject gameOverUI; 
    public static int enemiesRemaining; 
    public static GameManagerLV3 instance;
    public bool starCollected = false;

 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    
    void Start()
    {
       enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
      
    }

    /*public void LoadData(GameData data)
    {
        enemiesRemaining = data.enemiesRemaining;
        
    }

    public void SaveData(ref GameData data)
    {
        data.enemiesRemaining = enemiesRemaining;
    }*/


    public bool CheckForLevelCompletion()
    {
        bool enemiesDead = enemiesRemaining <= 0;
        bool coinsCollected = CoinManager.instance.AllCoinsCollected();

        if (enemiesDead && coinsCollected && starCollected)
        {
            Debug.Log("Level complete! Loading next level...");
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
            return true;
        }
        
        return false;
        
    }


    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        enemiesRemaining = -1;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        enemiesRemaining = -1;
    }

    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
