using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyexecution : MonoBehaviour
{
    public float kecepatan;
    public float bulletforce;
    public Rigidbody2D rb2d;
    public Transform theenemyrigid;

    private bool canattack = true;

    public GameObject attack;
    public Transform kanansisi,kirisisi;
    public float waktureload,waktuserang;


    public bool kanan = true,kiri;
    public bool serangkanan,serangkiri;

    // ground thingy
    public bool m_Grounded;
    [SerializeField] public float jumpforce;
    [SerializeField] public Transform m_GroundCheck;
    [SerializeField] public LayerMask m_WhatIsGround;		
    const float k_GroundedRadius = .2f;

    public Animator anima;

    public bool harderBoss;

    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}
	}

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);  
        Physics2D.IgnoreLayerCollision(8, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (canattack == true)
        {
            if (serangkanan == false)
            {
                if (serangkiri == false)
                {
                    if (serangkanan == false)
                    {
                        if (kanan == true)
                        {
                            rb2d.velocity = new Vector2(kecepatan, rb2d.velocity.y);
                            theenemyrigid.eulerAngles = new Vector3(0, 0, 0);
                        }
                        if (kanan == false)
                        {
                            if (kiri == false)
                            {
                                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
                            }
                        }
                        if (kiri == true)
                        {
                            rb2d.velocity = new Vector2(-kecepatan, rb2d.velocity.y);
                            theenemyrigid.eulerAngles = new Vector3(0, 180, 0);
                        }
                    }
                    else
                    {
                        rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                    }
                }
                else
                {
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                }
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

         if(serangkanan == true)
         {
             if(canattack == true)
             {
                StartCoroutine(attackkanan());
                canattack = false;
             }
         }
         if(serangkiri == true)
         {
             if(canattack == true)
             {
                StartCoroutine(attackkiri());
                canattack = false;
             }
         }

        if (kanan == true)
        {
            theenemyrigid.eulerAngles = new Vector3(0, 0, 0);
        }
        if (kiri == true)
        {
            theenemyrigid.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    
    IEnumerator attackkanan()
    {
        yield return new WaitForSeconds(waktuserang);
        GameObject b = Instantiate(attack) as GameObject;
        b.transform.position = kanansisi.transform.position;
        b.transform.rotation = kanansisi.transform.rotation;
        b.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletforce, 0, 0);
        if (harderBoss) 
        {
            GameObject c = Instantiate(attack) as GameObject;
            c.transform.position = kanansisi.transform.position;
            c.transform.rotation = kanansisi.transform.rotation;
            c.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletforce, bulletforce/5, 0);
        }
        theenemyrigid.eulerAngles = new Vector3(0, 0, 0);
        anima.SetTrigger("serang");
        FindObjectOfType<soundmanager>().Play("trowing1");
        StartCoroutine(reloads());
    }
    IEnumerator attackkiri()
    {
        yield return new WaitForSeconds(waktuserang);
        GameObject b = Instantiate(attack) as GameObject;
        b.transform.position = kirisisi.transform.position;
        b.transform.rotation = kirisisi.transform.rotation;
        b.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletforce, 0,0);
        if (harderBoss)
        {
            GameObject c = Instantiate(attack) as GameObject;
            c.transform.position = kirisisi.transform.position;
            c.transform.rotation = kirisisi.transform.rotation;
            c.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletforce, bulletforce /5, 0);
        }
        theenemyrigid.eulerAngles = new Vector3(0, 180, 0);
        FindObjectOfType<soundmanager>().Play("trowing2");
        anima.SetTrigger("serang");
        StartCoroutine(reloads());
    }
    IEnumerator reloads()
    {
    	yield return new WaitForSeconds(waktureload);
    	canattack = true;
    }

    public void enemys(bool jump)
    {
    	if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			rb2d.AddForce(new Vector2(0f, jumpforce));
            anima.SetTrigger("jump");
		}
    }
}
