using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerRotate : MonoBehaviour
{
    public float randomnumber;
    public float batas, batas2;
    void Start()
    {
        randomnumber = Random.Range(batas, batas2);
        this.transform.rotation = Quaternion.Euler(0, 0, randomnumber);
    }
}
