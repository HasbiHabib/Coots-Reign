using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    public savedataplayer save;
    public int Gameproggres;
    public UnityEvent Event1;
    public UnityEvent Event2;
    public UnityEvent Event3;
    public UnityEvent Event4;

    public Transform player;
    public Transform placement0;
    public Transform placement1;
    public Transform placement2;

    public GameObject placement0Room;
    public GameObject placement1Room;
    public GameObject placement2Room;

    public bool load;

    private void Start()
    {
        if (!load)
        {
            Load();
            SpawnPlayer();
        }
    }

    public void CHeckUpdate()
    {
        if(Gameproggres >= 1)
        {
            Event1.Invoke();
        }
        if(Gameproggres >= 2)
        {
            Event2.Invoke();
        }
        if (Gameproggres >= 3)
        {
            Event3.Invoke();
        }
        if (Gameproggres >= 4)
        {
            Event4.Invoke();
        }
    }

    public void Load() 
    {
        CHeckUpdate();
        SpawnPlayer();
    }

    public void SpawnPlayer() 
    {
        if(Gameproggres == 0) 
        {
            placement0Room.SetActive(true);
            player.transform.position = placement0.transform.position;
        }
        if (Gameproggres == 1)
        {
            placement1Room.SetActive(true);
            player.transform.position = placement1.transform.position;
        }
        if (Gameproggres == 2)
        {
            placement2Room.SetActive(true);
            player.transform.position = placement2.transform.position;
        }
        if (Gameproggres == 3)
        {
            placement2Room.SetActive(true);
            player.transform.position = placement2.transform.position;
        }
        if (Gameproggres == 4)
        {
            placement2Room.SetActive(true);
            player.transform.position = placement2.transform.position;
        }
    }

    public void Gamepgres(int JumpTo)
    {
        Gameproggres = JumpTo;
        save.saverLevel();
        CHeckUpdate();
    }
}
