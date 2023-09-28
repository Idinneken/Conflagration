using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    public TextMeshProUGUI textPaused;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool isMainMenu;


    // Start is called before the first frame update
    void Start()
    {
        textPaused.enabled = false;

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            isMainMenu = false;
        } else if (currentScene.name == "MainMenu")
        {
            isMainMenu = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) ) 
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!isPaused)
        {
            print("Is Paused");
            textPaused.enabled = true;
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if (isPaused)
        {
            print("Not Paused");
            textPaused.enabled = false;
            Time.timeScale += 1f;
            isPaused = false;
        }
    }
}
