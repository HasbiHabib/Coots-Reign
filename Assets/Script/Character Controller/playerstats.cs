using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerstats : MonoBehaviour
{
    [Header("TheStats")]
    public int HP = 6;
    public int HPmax = 6;
    [Header("otherComponent")]
    public movement move;
    private bool onCooldown = false;
    public float cooldownTime;

    [Header("visual component")]
    public Animator lowhealth;
    public Animator anima;
    public GameObject bloodpartikel;
    public Transform tempatkeluardarah;
    public GameObject gameover;
    private bool alive = false;

    // Update is called once per frame

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 9);
        HP = HPmax;
    }

    void Update()
    {
        // healt pengaturan
        if(HP <= 1)
        {
        	alive = true;
        }
        if(HP <= 1)
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
                gameover.SetActive(true);
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
        FindObjectOfType<soundmanager>().Play("cootsGH");
        FindObjectOfType<playerStatsUI>().Lifecheck();
        anima.SetTrigger("gothit");
        lowhealth.SetTrigger("damage");
        onCooldown = true;
        StartCoroutine(cooldowns());
    }

    IEnumerator cooldowns()
    {
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!onCooldown)
        {
            if (other.tag == "enemyattack")
            {
                gothits();
            }
        }
    }
}
