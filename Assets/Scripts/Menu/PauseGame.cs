using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseGame : MonoBehaviour
{
    private TextMeshProUGUI textPaused;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool isMainMenu;


    void Start()
    {

        textPaused = GetComponentInChildren<TextMeshProUGUI>();

        textPaused.enabled = false; //Ensures UI isn't showing


        //Checks if the current scene is the main menu
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            isMainMenu = false;
        } else if (currentScene.name == "MainMenu")
        {
            isMainMenu = true;
        }
    }

    void Update()
    {
        //whenever p is pressed other than on the main menu run pause function
        if(Input.GetKeyDown(KeyCode.P)  && isMainMenu == false) 
        {
            Pause();
        }
    }

    private void Pause() // pauses the game using the Time.timescale
    {
        if (!isPaused)
        {
            textPaused.enabled = true;
            Time.timeScale = 0f;
            isPaused = true;
        }
        else if (isPaused)
        {
            textPaused.enabled = false;
            Time.timeScale += 1f;
            isPaused = false;
        }
    }
}
