using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemaster : MonoBehaviour
{
	public bool paused = false;
    public bool onthing = false;
    public bool overload = false;
    public bool ontransition = false;
    
     public void resume()
     {
        Time.timeScale = 1;
        paused = false;
     }

     public void pause()
     {
        Time.timeScale = 0;
        paused = true;
     }
    public void StartTheFalse(float waktutransisi) 
    {
        StartCoroutine(TransitionControl(waktutransisi));
    }

    IEnumerator TransitionControl(float waktutransisi) 
    {
        yield return new WaitForSecondsRealtime(waktutransisi);
        ontransition = false;
    }
}
