using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;  

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI; 
    public static int enemiesRemaining; 
    public static GameManagerScript instance;// Start is called once before the first execution of Update after the MonoBehaviour is created

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
    public void CheckForLevelCompletion()
    {
        if(enemiesRemaining <= 0)
        {
            Debug.Log("All enemies are defeated! Loading next level...");
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
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


 