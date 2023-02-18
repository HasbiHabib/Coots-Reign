using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("movement setting")]
    public CharacterController2D controller;
    public float normalrun;
    public float lari;
    [Range(0, 1f)][SerializeField] public float jalanspeed;
    float horizontalmove;

    [Header("condition_thing")]
    public bool onEvent;
    private bool jump;
    private bool slam;


    [Header("other component")]
    public Animator character;
    public Rigidbody2D rigid;
    public Transform kakinya;
    public Transform checkpoint;
    public float waktuspawn;

    void Update()
    {
                if (onEvent == false)
                {

            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                horizontalmove = Input.GetAxis("Horizontal") * lari;
                character.SetFloat("jalan", Mathf.Abs(horizontalmove));
            }
            else 
            {
                horizontalmove = Input.GetAxis("Horizontal") * normalrun;
                character.SetFloat("jalan", Mathf.Abs(horizontalmove));
            }
                            if (controller.m_midair == false)
                            {
                                if (Input.GetButtonDown("Jump"))
                                {
                                    jump = true;
                                }
                            }
             
            
                }




        if (rigid.velocity.y <= -1)
        {
            character.SetBool("fall",true);
        }
        else
        {
        	character.SetBool("fall",false);
        }
        if(rigid.velocity.y >= 1)
        {
        	character.SetBool("jump",true);
        }
        else
        {
        	character.SetBool("jump",false);
        }
        
        if(controller.m_midair == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                slam = true;
            }
        }
        
    }

    void FixedUpdate()
    {
    	controller.Move(horizontalmove * Time.fixedDeltaTime , false, jump, slam, false);
    	jump = false;
    	slam = false;
    }

    void  OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "fall")
      {
          character.SetTrigger("respawn");
          StartCoroutine(despawn());
      }
    }

    public void respawn()
    {
        this.transform.position = checkpoint.transform.position;
        FindObjectOfType<playerstats>().gothits(2,0,0);	
    }

    IEnumerator despawn()
    {
    	yield return new WaitForSeconds(waktuspawn);
    	respawn();
    }


    public void AfterEvent()
    {
        onEvent = false;
    }

}