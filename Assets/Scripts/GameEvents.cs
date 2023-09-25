using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance; 

    void Awake() 
    { 
        instance = this;
    } 

    //Our "Game Events" are here. If you want to add a new one, adding looks like this:
    //public event Action NewEvent;
    //public void CallNewEvent() => NewEvent.Invoke();

    public event Action Event1;
    public void CallEvent1() => Event1?.Invoke();
}
