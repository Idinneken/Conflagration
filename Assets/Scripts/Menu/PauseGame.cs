using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public Text textPaused;
    [SerializeField] private bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        textPaused.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (!isPaused) 
        {
            textPaused.enabled = true;
            Time.timeScale = 0f;
        }
        else if (isPaused)
        {
            textPaused.enabled = false;
            Time.timeScale += 1f;
        }
    }
}
