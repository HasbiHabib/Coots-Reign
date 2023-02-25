using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Events;

public class savedataplayer : MonoBehaviour
{
    public bool canLoad;
    public int proggress = 0;
    public QuestManager questGM;

    public UnityEvent skipintro;
    public bool pernahLogin;

    void Start()
    {
        if(canLoad == true)
        {
            LoadOptions();
        }
    }

    public void saverLevel()
    {
        proggress = questGM.Gameproggres;
        Saveoptions();
    }
    public void StartBEgining() 
    {
        proggress = 0;
        pernahLogin = false;
        Saveoptions();
    }



    public void Saveoptions()
    {
        pernahLogin = true;
        SaveSystem.saveoption(this);
    }
    public void LoadOptions()
    {
    	saveproggresoption data2 = SaveSystem.Loadoption();
        pernahLogin = data2.pernahlogin;
        questGM.Gameproggres = data2.proggress;
        questGM.Load();
        Debug.Log("LOAD OPTION");
    }
}
