using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class rooms : MonoBehaviour
{
    private bool canenter;

    public UnityEvent masuks;
    public rooms pintu_yang_dituju;
    public Transform thisdoor;
    public GameObject thisroom;

    [Header("put_it_empty")]
    public Animator transition;
    public gamemaster GM_;
    public Transform players;
    public float waktutransisi = 1f;

    void Start()
    {
        waktutransisi = 1f;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "player")
        {
            FindObjectOfType<doornotice>().bisa();
            canenter = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "player")
        {
            FindObjectOfType<doornotice>().gagal();
            canenter = false;
        }
    }

    void Update()
    {
        if(GM_.onthing == false)
        {
        if(GM_.ontransition == false)
        {
    	if(canenter == true)
    	{
    		if(Input.GetKeyDown(KeyCode.E))
    		{
    			transition.SetTrigger("out");
                GM_.ontransition = true;
    			StartCoroutine(masuk());
                FindObjectOfType<soundmanager>().Play("door");
                canenter = false;

    		}
    	}
        }
        }
    }

    IEnumerator masuk()
    {
    	yield return new WaitForSecondsRealtime(waktutransisi);
    	masuks.Invoke();
        pintu_yang_dituju.thisroom.SetActive(true);
        thisroom.SetActive(false);
        players.transform.position = pintu_yang_dituju.thisdoor.transform.position;
    	transition.SetTrigger("in");
        FindObjectOfType<movement>().EnterTheDoor();
        GM_.StartTheFalse(waktutransisi);
    }
}
