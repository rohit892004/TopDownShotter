using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerVisual : MonoBehaviour
{
    [SerializeField] private Transform[] gunTransform;


    public void Update()
    {
        GunSwitch();
    }

    public void GunSwitch()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchOn(gunTransform[0]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchOn(gunTransform[1]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchOn(gunTransform[2]);
        }
    }
  
    public void SwitchOn( Transform gunTransform)
    {   
        SwitchOffGuns();
        gunTransform.gameObject.SetActive(true);
    }
    public void SwitchOffGuns()
    {
        for(int i =0; i<gunTransform.Length; i++)
        {
            gunTransform[i].gameObject.SetActive(false);
        }
    }

}


