using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SignalListener : MonoBehaviour
{
    //you attach this scipt to an object 
    //create a signal with the SignalSender scriptable object
    //when attached give this a child object from which you can call functions 
    //look to heart manager for an example

    public SignalSender signal;
    public UnityEvent signalEvent;
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }
}
