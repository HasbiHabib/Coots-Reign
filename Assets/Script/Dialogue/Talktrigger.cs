using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Talktrigger : MonoBehaviour
{
    private bool cantalk;
    public bool autoopen;
    public bool cannotopentwice;
    private bool first = true;

    public UnityEvent talk;
    public gamemaster GM_;

    void Start()
    {
        GM_ = FindObjectOfType<gamemaster>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GM_.onthing == false)
        {
            if (!autoopen)
            {
                if (other.tag == "player")
                {
                    FindObjectOfType<doornotice>().talks();
                    cantalk = true;
                }
            }
            else 
            {
                if (!cannotopentwice)
                {
                    cantalk = false;
                    FindObjectOfType<doornotice>().untalks();
                    GM_.onthing = true;
                    talk.Invoke();
                }
                else 
                {
                    if (first) 
                    {
                        cantalk = false;
                        FindObjectOfType<doornotice>().untalks();
                        GM_.onthing = true;
                        talk.Invoke();
                        first = false;
                    }
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            FindObjectOfType<doornotice>().untalks();
            cantalk = false;
        }
    }

    void Update()
    {
        if (GM_.onthing == false)
        {
            if (GM_.ontransition == false)
            {
                if (cantalk == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!cannotopentwice)
                        {
                            cantalk = false;
                            FindObjectOfType<doornotice>().untalks();
                            GM_.onthing = true;
                            talk.Invoke();
                        }
                        else 
                        {
                            if (first)
                            {
                                cantalk = false;
                                FindObjectOfType<doornotice>().untalks();
                                GM_.onthing = true;
                                talk.Invoke();
                                first = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
