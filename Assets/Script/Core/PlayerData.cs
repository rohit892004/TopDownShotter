using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Player control;
    
    private void OnEnable()
    {
    if (control == null)
        control = new Player();

    control.Enable();
    }

    private void OnDisable()
    {
    if (control != null)
        control.Disable();
    }
}

