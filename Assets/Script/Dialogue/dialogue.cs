using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class dialogue : MonoBehaviour
{
    // script utama dalam sistem dialogue

    // info : ( dialogue ) ( dialogueobject ) ( dialogue manager ) ( NPCTrigger )

    // komponen komponen display
    public Text dialogtext;                           // text dialogue yang di display
    public Image wajahini;                            // image dialogue yang di display
    public Text namaini;

    public Sprite empty;

    // efektor script
    public bool npcface;                              // memilih NPC yang berbicara
    
    // float script
    public float textspeed;                           // kecepatan mengisi text

    // script ekstender / scdript pihak kedua
   
    private Queue<string> sentences;                  // text yang dibicarakan NPC
    private Queue<string> kalimat;                    // dlm bahasa indonesia
    private Queue<Sprite> wajah;
    private Queue<string> nama;
    private Queue<string> sound;

    private string kalimah;
    private string sentence;

    public dialoguemanager dialogmanager;

    public gamemaster _GM;
    public GameObject thedialogbar;

    public bool ondialogue;

    public bool english = true;
    public bool indonesia;

    public Animator thedialogbars;

    void Start()
    {
        // menentukan text si NPC
        sentences = new Queue<string>();
        wajah = new Queue<Sprite>();
        nama = new Queue<string>();
        kalimat = new Queue<string>();
        sound = new Queue<string>();
    }


    public void StartDialogue(dialoguemanager dialog)
    {
        thedialogbar.SetActive(true);
        _GM.onthing = true;
        ondialogue = true;
        
        // bahasa ingris 
        if(english == true)
        {
           sentences.Clear();
           foreach (string sentence in dialog.sentences)
           {
               sentences.Enqueue(sentence);
           }
        }
        
        // bahasa indonesia
        if(indonesia == true)
        {
           kalimat.Clear();
           foreach (string kalimah in dialog.kalimat)
           {
               kalimat.Enqueue(kalimah);
           }
        }


        wajah.Clear();
        foreach (Sprite wajahi in dialog.wajah)
        {
            wajah.Enqueue(wajahi);
        }
        sound.Clear();
        foreach(string suaras in dialog.thesound)
        {
            sound.Enqueue(suaras);
        }

        nama.Clear();
        foreach(string namas in dialog.nama)
        {
            nama.Enqueue(namas);
        }
        dialogmanager = dialog.dialogmanager;

        DisplayNextSentence();
        // jika NPC yang berbicara
    }

    public void DisplayNextSentence()
    {
        thedialogbars.SetTrigger("pop");
        // text dialogue selanjutnya
        if (english == true)
        {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        }

        if(indonesia == true)
        {
        if(kalimat.Count == 0)
        {
            EndDialogue();
            return;
        }
        }
        
        // bahasa ingris
        if(english == true)
        {
        sentence = sentences.Dequeue();
        dialogtext.text = sentence;
        }

        if(indonesia == true)
        {
        kalimah = kalimat.Dequeue();
        dialogtext.text = kalimah;
        }

        Sprite wajahi = wajah.Dequeue();
        wajahini.sprite = wajahi;

        string sounder = sound.Dequeue();
        //FindObjectOfType<soundmanager>().Play(sounder);

        string namas = nama.Dequeue();
        namaini.text = namas;


        StopAllCoroutines();

        if(english == true)
        {
            StartCoroutine(Typesentence(sentence)); 
        }     
        if(indonesia == true)
        {
            StartCoroutine(menuliskalimah(kalimah)); 
        }  
    }

    IEnumerator Typesentence (string sentence)
    {
        // agar text diisi perlahan
        dialogtext.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogtext.text += letter;
            yield return new WaitForSecondsRealtime(textspeed);
        }
    }

    IEnumerator menuliskalimah (string kalimah)
    {
        // agar text diisi perlahan
        dialogtext.text = "";
        foreach (char letter in kalimah.ToCharArray())
        {
            dialogtext.text += letter;
            yield return new WaitForSecondsRealtime(textspeed);
        }
    }

    void EndDialogue()
    {
        // dialogue berakhir
        nama.Clear();
        sentences.Clear();
        kalimat.Clear();
        wajah.Clear();
        dialogtext.text = "";
        namaini.text = "";
        wajahini.sprite = empty;
        
        thedialogbar.SetActive(false);
        _GM.onthing = false;
        ondialogue = false;
        dialogmanager.dialogselesai.Invoke();
    }

    void Update()
    {
    	if(ondialogue == true)
    	{
            if (Input.GetKeyDown(KeyCode.E))
            {
            DisplayNextSentence();
    	}
        }
    }
   
}
