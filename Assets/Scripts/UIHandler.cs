using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public SignalSender weights;
    // Start is called before the first frame update
    public void UpdateStuff(){
        weights.Raise();
    }
}
