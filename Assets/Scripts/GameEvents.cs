using System;
using System.Reflection;
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
    //Always have the void start with "Call"

    public event Action Event1;
    public void CallEvent1() => Event1?.Invoke();


    public EventInfo GetEventInfo(string eventName)
    {
        if (GetType().GetEvent(eventName) != null)
        {
            return GetType().GetEvent(eventName);
        }

        Debug.LogError($"Event named {eventName} not found");
        return null;
    }

    public void CallEvent(string eventName)
    {
        this.GetType().GetMethod($"Call{eventName}").Invoke(instance, null);
    }

}
