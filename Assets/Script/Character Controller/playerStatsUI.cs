using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStatsUI : MonoBehaviour
{
    public GameObject life1,life2,life3,life4,life5,life6;
    public playerstats player;

    void Start()
    {
        Lifecheck();
    }

    public void Lifecheck()
    {
        life1.SetActive(false);
        life2.SetActive(false);
        life3.SetActive(false);
        life4.SetActive(false);
        life5.SetActive(false);
        life6.SetActive(false);
        if (player.HP >= 1)
        {
            life1.SetActive(true);
        }
        if (player.HP >= 2)
        {
            life2.SetActive(true);
        }
        if (player.HP >= 3)
        {
            life3.SetActive(true);
        }
        if (player.HP >= 4)
        {
            life4.SetActive(true);
        }
        if (player.HP >= 5)
        {
            life5.SetActive(true);
        }
        if (player.HP >= 6)
        {
            life6.SetActive(true);
        }
    }
}
