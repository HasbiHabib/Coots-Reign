using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class doornotice : MonoBehaviour
{
    public UnityEvent onNotice;
    public UnityEvent unNotice;

    public void bisa()
    {
    	onNotice.Invoke();
    }
    public void gagal()
    {
    	unNotice.Invoke();
    }


    public UnityEvent saving;
    public UnityEvent unsaving;

    public void save()
    {
        saving.Invoke();
    }
    public void unsave()
    {
        unsaving.Invoke();
    }

    public UnityEvent talk;
    public UnityEvent untalk;

    public void talks()
    {
        talk.Invoke();
    }
    public void untalks()
    {
        untalk.Invoke();
    }
}
