using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Extensions;

public class Trigger : MonoBehaviour
{
    public string triggerID;

    public List<string> requiredKeys = new();

    [Header("Trigger conditions")]
    public bool triggerOnEnter;
    public bool triggerOnStay;
    public bool triggerOnExit;    
    public bool disabled;

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

        if (objectInvokesTriggers && keysSatisfied && triggerOnEnter && !disabled)
        {
            if (callsEvent) { CallEvent(); }

            if (invokesBehaviours) { InvokeBehaviours(); }
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        bool objectInvokesTriggers = other.GetComponent<InvokesTriggers>() ?? false;
        bool keysSatisfied = other.GetComponent<InvokesTriggers>().keys.ContainsList(requiredKeys);

        if (objectInvokesTriggers && keysSatisfied && triggerOnStay && !disabled)
        {
            if (callsEvent) { CallEvent(); }

            if (invokesBehaviours) { InvokeBehaviours(); }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bool objectInvokesTriggers = other.GetComponent<InvokesTriggers>() ?? false;
        bool keysSatisfied = other.GetComponent<InvokesTriggers>().keys.ContainsList(requiredKeys);

        if (objectInvokesTriggers && keysSatisfied && triggerOnExit && !disabled)
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