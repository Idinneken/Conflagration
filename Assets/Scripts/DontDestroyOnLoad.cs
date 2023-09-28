using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //Apply to object so it isn't destroyed between scene transitions

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
