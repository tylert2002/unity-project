using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3Scene");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4Scene");
    }
}
