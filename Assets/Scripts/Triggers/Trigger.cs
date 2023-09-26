using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Extensions;

public class Trigger : MonoBehaviour
{
    public List<string> requiredKeys = new();

    [Header("Trigger conditions")]
    public bool triggerOnEnter;
    public bool triggerOnStay;
    public bool triggerOnExit;    

    [Header("Behaviour")]
    public bool callsEvent;
    public bool invokesBehaviours;

    [Space]
    public string eventToCall;
    [Space]
    public UnityEvent behaviours;

    private void OnTriggerEnter(Collider other) 
    {
        bool objectInvokesTriggers = other.GetComponent<InvokesTriggers>() ?? false;
        bool keysSatisfied = other.GetComponent<InvokesTriggers>().keys.ContainsList(requiredKeys);

        if (objectInvokesTriggers && keysSatisfied && triggerOnEnter)
        {
            if (callsEvent) { CallEvent(); }

            if (invokesBehaviours) { InvokeBehaviours(); }
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        bool objectInvokesTriggers = other.GetComponent<InvokesTriggers>() ?? false;
        bool keysSatisfied = other.GetComponent<InvokesTriggers>().keys.ContainsList(requiredKeys);

        if (objectInvokesTriggers && keysSatisfied && triggerOnStay)
        {
            if (callsEvent) { CallEvent(); }

            if (invokesBehaviours) { InvokeBehaviours(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool objectInvokesTriggers = other.GetComponent<InvokesTriggers>() ?? false;
        bool keysSatisfied = other.GetComponent<InvokesTriggers>().keys.ContainsList(requiredKeys);

        if (objectInvokesTriggers && keysSatisfied && triggerOnExit)
        {
            if (callsEvent) { CallEvent(); }

            if (invokesBehaviours) { InvokeBehaviours(); }
        }
    }

    private void CallEvent()
    {
        Debug.Log(eventToCall);
        GameEvents.instance.CallEvent(eventToCall);
    }

    private void InvokeBehaviours()
    {
        behaviours.Invoke();
    }

}