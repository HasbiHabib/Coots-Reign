using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("movement setting")]
    public CharacterController2D controller;
    public gamemaster _GM;
    public float normalrun;
    public float lari;
    float horizontalmove;

    [Header("condition_thing")]
    public bool onEvent;
    private bool jump;
    private bool slam;
    private bool dash;


    [Header("other component")]
    public Animator character;
    public Rigidbody2D rigid;
    public Transform kakinya;
    public Transform checkpoint;
    public float waktuspawn;

    [Header("Dash component")]
    public float cooldown;
    public float DashForce;
    public Transform thecamera;

    public Transform cursorcheck;
    private Vector3 target1;
    private Vector3 difference;
    private Vector2 direction;
    private float rotationZ;
    private float distance;
    private bool on_cooldown;

    public Animator players;
    public Camera cam;

    public bool canDashing = false;

    void Awake()
    {
        on_cooldown = false;
    }

    void Update()
    {
        target1 = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -transform.position.z));

        difference = target1 - transform.position;
        cursorcheck.position = difference + thecamera.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        distance = difference.magnitude;
        direction = difference / distance;

        direction.Normalize();

            if (onEvent == false && _GM.onthing == false)
            {
            if (controller.m_Grounded)
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
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && canDashing)
                {
                    tembak();
                }
            }
            }
        else
        {
            horizontalmove = 0;
            character.SetFloat("jalan", 0);
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
    	controller.Move(horizontalmove * Time.fixedDeltaTime , false, jump, slam, false, dash);
    	jump = false;
    	slam = false;
        dash = false;
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


    public void tembak()
    {
        if (on_cooldown == false)
        {
            players.Play("dash");
            StartCoroutine(cooldowns());
            on_cooldown = true;
            GetComponent<CharacterController2D>().onDash = true;
            if (cursorcheck.localPosition.x < 0)
            {
               horizontalmove = -DashForce;
                dash = true;
            }
            else if (cursorcheck.localPosition.x > 0)
            {
               horizontalmove = DashForce;
                dash = true;
            }
        }
    }

    IEnumerator cooldowns()
    {
        yield return new WaitForSeconds(cooldown);
        on_cooldown = false;
    }

    public void EnterTheDoor()
    {
        onEvent = true;
        StartCoroutine(transisionTime());
    }
    IEnumerator transisionTime()
    {
        yield return new WaitForSeconds(1.5f);
        onEvent = false;
    }

    public void EnableDashing() 
    {
        canDashing = true;
    }
}