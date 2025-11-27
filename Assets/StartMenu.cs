using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    public void StartButton()
    {
        // DataPersistence.instance.NewGame();
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif 
        Application.Quit();
    }

    public void LoadGame()
    {
        Debug.Log("Load Game Clicked");

        bool loaded = DataPersistence.instance.LoadGame();

        if (loaded)
        {
            SceneManager.LoadSceneAsync("SampleScene");
        }   
        else
        {
            Debug.Log("No save data found!");
        }
        
    }
}
