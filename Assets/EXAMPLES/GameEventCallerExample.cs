using UnityEngine;

public class GameEventCallerExample : MonoBehaviour
{
    //Here's how you call an event
    void Start()
    {
        GameEvents.instance.CallEvent1();
    }
}
