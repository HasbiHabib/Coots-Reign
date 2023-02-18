using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Talktrigger : MonoBehaviour
{
    private bool cantalk;
    public bool autoopen;

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
                cantalk = false;
                FindObjectOfType<doornotice>().untalks();
                GM_.onthing = true;
                talk.Invoke();
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
                        cantalk = false;
                        FindObjectOfType<doornotice>().untalks();
                        GM_.onthing = true;
                        talk.Invoke();
                    }
                }
            }
        }
    }
}
