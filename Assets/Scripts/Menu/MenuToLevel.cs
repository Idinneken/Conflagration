using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToLevel : MonoBehaviour
{
    
    public void TransitionToLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
