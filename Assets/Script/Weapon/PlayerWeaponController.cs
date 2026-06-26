using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerData playerData;

    public void Start()
    {
        playerData = GetComponent<PlayerData>();

        playerData.control.Charcter.Fire.performed+= context=>Shoot();
    
    }

    public void Shoot()
    {
        GetComponentInChildren<Animator>().SetTrigger("Fire");
    }
}
