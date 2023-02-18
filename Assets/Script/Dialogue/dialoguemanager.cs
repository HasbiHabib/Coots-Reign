using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dialoguemanager : MonoBehaviour
{
    // script untuk NPC special
    // info : (dialogue) ( dialogue object )

    // script untuk customize dialogue

    public dialoguemanager dialogmanager;
    public bool aftereffect;
    public string[] nama;
    public Sprite[] wajah;
    public string[] thesound;

    [Header("English")]
    [TextArea(3,10)]
    public string[] sentences;
    [Header("Indonesia")]
    [TextArea(3,10)]
    public string[] kalimat;

     [Header("Events")]
    [Space]
    public UnityEvent dialogselesai;

    void Start()
    {
        dialogmanager = this;
    }

    public void startTheDialogue()
    {
        StartCoroutine(mulaidialogue());
    }

    IEnumerator mulaidialogue()
    {
        yield return new WaitForSeconds(0.001f);
        FindObjectOfType<dialogue>().StartDialogue(this);
    }
}
