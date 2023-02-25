using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransitionEvent : MonoBehaviour
{
    public Animator transisi;
    public UnityEvent afterTransition;

    public void Becomedark(float timer) 
    {
        StartCoroutine(WaitEvent(timer));
        transisi.SetTrigger("out2");
    }
    IEnumerator WaitEvent(float timer) 
    {
        yield return new WaitForSeconds(timer);
        afterTransition.Invoke();
        transisi.SetTrigger("in2");
    }
}
