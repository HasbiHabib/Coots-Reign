using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerstats : MonoBehaviour
{
    [Header("TheStats")]
    public float HP = 6;
    public float HPmax = 6;
    [Header("otherComponent")]
    public movement move;

    [Header("visual component")]
    public Animator lowhealth;
    public Animator anima;
    public Animator cameras;
    public GameObject bloodpartikel;
    public Transform tempatkeluardarah;
    public Animator gameover;
    private bool alive = false;

    // Update is called once per frame

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 9);
    }

    void Update()
    {
        // healt pengaturan
        if(HP <= 1)
        {
        	alive = true;
        }
        if(HP <= 3)
        {
            lowhealth.SetBool("danger",true);
        }
        else
        {
            lowhealth.SetBool("danger",false);
        }

        if(HP <= 0)
        {
        	if(alive == true)
        	{
        		gameover.SetTrigger("mulai");
        		alive = false;
        	}
        }
        if(HP >= HPmax)
        {
        	HP = HPmax;
        }
    }

    public void gothits()
    {
        HP = HP - 1;
    }
}
