using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public string eventToListenFor;
    private EventInfo listenedEventInfo;
    [Space] 
    public UnityEvent behaviours;

    void Start()
    {
        CheckEventExists();
        listenedEventInfo?.AddEventHandler(GameEvents.instance, new Action(() => behaviours.Invoke()));
        //Looks at the GameEvents which exist. If there is an existent event with the name "eventName", it adds the methods in the UnityEvent as an Action
        //E.G. If there's an event called "SunExploded", and eventToListenFor == "SunExploded", it'll do the stuff you list in behaviours
    }

    public void CheckEventExists()
    {        
        foreach (EventInfo eventInfo in GameEvents.instance.GetType().GetEvents())
        {
            if (eventInfo.Name == eventToListenFor) { listenedEventInfo = eventInfo; return; }
        }

        Debug.LogWarning($"An event called '{eventToListenFor}' which is being listened for on '{gameObject.name}' doesn't exist! You may have entered the name incorrectly");        
    }

    private void OnDestroy()
    {
        listenedEventInfo?.RemoveEventHandler(GameEvents.instance, new Action(() => behaviours.Invoke()));
        //Looks at the GameEvents which exist. If there is an existent event with the name "eventName", it removes the methods in the UnityEvent as an Action
    }
}

