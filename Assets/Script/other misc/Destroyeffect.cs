using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyeffect : MonoBehaviour
{
    public float DestoryTime;
    float timer;

    private void Update()
    {
        if(timer <= DestoryTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
