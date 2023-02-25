using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class saveproggresoption
{
    // option
    //public float brignesslevel;
    public int proggress;
    public bool pernahlogin;

    public saveproggresoption(savedataplayer player)
    {
        //option
        // brignesslevel = player.brignesslevel;
        proggress = player.proggress;
        pernahlogin = player.pernahLogin;
    }

}
