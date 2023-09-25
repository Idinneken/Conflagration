using UnityEngine;

public class GameEventListenerExample : MonoBehaviour
{
    //Here's how you add a method to an event
    void Start()
    {
        GameEvents.instance.Event1 += SomeMethod;
    }

    void SomeMethod()
    {
        Debug.Log("This message was called from an In-code listener :)");
    }

    //Remember to remove it if the GameObject is destroyed
    void OnDestroy()
    {
        GameEvents.instance.Event1 -= SomeMethod;
    }
}
