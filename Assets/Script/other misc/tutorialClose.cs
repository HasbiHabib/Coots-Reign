using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialClose : MonoBehaviour
{
    public GameObject tutorialbar;
    public gamemaster _GM;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            Close();
            FindObjectOfType<soundmanager>().Play("button2");
        }
    }

    public void Close()
    {
        _GM.onthing = false;
        tutorialbar.SetActive(false);
    }
}
