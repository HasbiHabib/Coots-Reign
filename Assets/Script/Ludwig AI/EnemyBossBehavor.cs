using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBossBehavor : MonoBehaviour
{
    public UnityEvent afterdefeat;
    public int HP;
    public enemymove enemymove;

    public Animator LudwigAnim;

    public float Shieldcooldown;
    float ShieldTimer;
    bool ShieldON = false;
    float ShieldActivatedTimer;

    public float LaserCooldwon;
    float LaserTimer;

    public bool activated = true;

    public GameObject slicer;
    public Transform slicercome;

    public float randomsceam;

    void Start()
    {
        ShieldCooldowns();
        randomsceam = 1;
    }
    void Update()
    {
        if (activated)
        {
            if (ShieldTimer >= 0)
            {
                ShieldTimer -= Time.deltaTime;
            }
            else
            {
                if (ShieldON)
                {
                    ShieldCooldowns();
                }
            }
            if (ShieldActivatedTimer >= 0)
            {
                ShieldActivatedTimer -= Time.deltaTime;
            }
            else
            {
                if (!ShieldON)
                {
                    GetShield();
                }
            }
        }
    }

    public void GetShield()
    {
        ShieldTimer = Shieldcooldown;
        LudwigAnim.SetBool("SHielded", true);
        ShieldON = true;
    }

    public void ShieldCooldowns() 
    {
        LudwigAnim.SetBool("SHielded", false);
        ShieldActivatedTimer = Shieldcooldown * 2;
        ShieldON = false;
    }

    public void gothit() 
    {
        if(ShieldTimer <= 0 && HP >= 1) 
        {
            HP -= 1;
            randomsceam += 1;
            LudwigAnim.Play("gothit",1,2);
            CheckHealth();
            Instantiate(slicer, slicercome.position, Quaternion.identity);
            ludscream();
        }
    }

    private void CheckHealth() 
    {
        if (HP <= 0)
        {
            GotDefeat();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerattack")
        {
            gothit();
            FindObjectOfType<soundmanager>().Play("slice");
        }
    }

    public void GotDefeat() 
    {
        enemymove.aktif = false;
        LudwigAnim.SetTrigger("defeat");
        activated = false;
        afterdefeat.Invoke();
    }

    public void ludscream() 
    {
            if (randomsceam == 1)
            {

            }
            if (randomsceam == 2)
            {
                FindObjectOfType<soundmanager>().Play("ludGH2");
            }
            if (randomsceam == 3)
            {
                FindObjectOfType<soundmanager>().Play("ludGH3");
                randomsceam = 1;
            }

    }





    }
