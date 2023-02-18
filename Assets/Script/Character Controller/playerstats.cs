using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerstats : MonoBehaviour
{
    [Header("TheStats")]
    public float HP = 100;
    public float HPmax;
    public float MP,MPmax;
    public float SP,SPmax;

    public float manaregen;
    public float turnregen;
    public float turnadder;
    public int armorlevel;

    public float turn,turnMax;

    [Header("otherComponent")]
    public movement move;

    [Header("visual component")]
    public Animator lowhealth;
    public GameObject chargee;
    public bool charges;
    public Animator anima;
    public Animator cameras;
    public GameObject bloodstain1,bloodstain2,bloodstain3,bloodstain4;
    public GameObject bloodpartikel;
    public Transform tempatkeluardarah;
    public int idle;
    public Animator gameover;
    private bool alive = false;

    // Update is called once per frame

    void Start()
    {
        lowhealth = GameObject.FindGameObjectWithTag("healthlow").GetComponent<Animator>();
        cameras = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        gameover = GameObject.FindGameObjectWithTag("gameover").GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(10, 9);
        Physics2D.IgnoreLayerCollision(9, 9);
        Physics2D.IgnoreLayerCollision(11, 8);  
        Physics2D.IgnoreLayerCollision(10, 10);  
        Physics2D.IgnoreLayerCollision(11, 11);
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
        if(SP >= SPmax)
        {
        	SP = SPmax;
        }

        if(manaregen >= 0.01f)
        {
            if(MP <= MPmax)
            {
                MP = MP + manaregen;
            }
            else
            {
                MP = MPmax;
            }
        }


        if(charges == true)
        {
            turn = turn + turnadder;
            anima.SetBool("charge",true);
            chargee.SetActive(true);
        }
        if(turn >= turnMax)
        {
            turn = turnMax;
        }

        if(turnregen >= 0.01f)
        {
            if(turn <= turnMax)
            {
                turn = turn + turnregen;
            }
            else
            {
                turn = turnMax;
            }
        }
    }

    public void gothits(float jumlahhp,float jumlahmp,float jumlahsp)
    {
        float seranganhp = jumlahhp - armorlevel;
        if(seranganhp <= 0)
        {
            seranganhp = 1;
        }

        HP = HP - seranganhp;
        MP = MP - jumlahmp;
        SP = SP - jumlahsp;
        cameras.SetTrigger("hit");
        idle = Random.Range(1,5);
        darahstain();
    }


    public void darahstain()
    {
        Instantiate(bloodpartikel,tempatkeluardarah.position,tempatkeluardarah.rotation);
        anima.SetTrigger("hit");
        
        if(idle == 1)
        {
            Instantiate(bloodstain1,tempatkeluardarah.position,tempatkeluardarah.rotation);
        }
        if(idle == 2)
        {
            Instantiate(bloodstain2,tempatkeluardarah.position,tempatkeluardarah.rotation);
        }
        if(idle == 3)
        {
            Instantiate(bloodstain3,tempatkeluardarah.position,tempatkeluardarah.rotation);
        }
        if(idle == 4)
        {
            Instantiate(bloodstain4,tempatkeluardarah.position,tempatkeluardarah.rotation);
        }
        if(idle == 5)
        {
            Instantiate(bloodstain1,tempatkeluardarah.position,tempatkeluardarah.rotation);
        }
    }
}
