using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a scriptable object
[CreateAssetMenu]
public class SignalSender : ScriptableObject
{
    //this is made to be placed in the SignalListener component
    //will be raised from different c# scripts for reasons in the future
    public List<SignalListener> listeners = new List<SignalListener>();
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        listeners.Add(listener);
    }
    public void DeRegisterListener(SignalListener listener)
    {
        listeners.Remove(listener);
    }
}