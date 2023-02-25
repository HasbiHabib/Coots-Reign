using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
	public bool aktif = true;
    public float jarakraycast_sisi;
    public float jarakraycast_atas;
    public float jarakraycast_attack;

    public Transform kanansisi,kirisisi;
    public Transform atas;

    public bool jump;

    public LayerMask tolayer;
    public enemyexecution enemy;

    // Update is called once per frame
    void Update()
    {
             Vector2 forwardkanan = transform.TransformDirection(Vector2.right) * jarakraycast_sisi;
             Vector2 forwardkiri = transform.TransformDirection(Vector2.left) * jarakraycast_sisi;
             Vector2 forwardkananattack = transform.TransformDirection(Vector2.right) * jarakraycast_attack;
             Vector2 forwardkiriattack = transform.TransformDirection(Vector2.left) * jarakraycast_attack;
             Vector2 forwardatas = transform.TransformDirection(Vector2.up) * jarakraycast_atas;

            if(aktif == true)
            {
            // jarak enemy to player (movement / jalan)
            if(Physics2D.Raycast(kanansisi.position, forwardkanan, jarakraycast_sisi,tolayer))
            {
        	       Debug.DrawRay(kanansisi.position, forwardkanan, Color.green);
                   enemy.kanan = true;
        	}
        	else
        	{
        		Debug.DrawRay(kanansisi.position, forwardkanan, Color.blue);
                enemy.kanan = false;

        	}
            if(Physics2D.Raycast(kirisisi.position, forwardkiri, jarakraycast_sisi,tolayer))
            {
                   Debug.DrawRay(kirisisi.position, forwardkiri, Color.green);
                   enemy.kiri = true;
            }
            else
            {
                Debug.DrawRay(kirisisi.position, forwardkiri, Color.blue);
                 enemy.kiri = false;
            }




            // jarak tembak enemy to player (tembak/ attack)
            if(Physics2D.Raycast(kanansisi.position, forwardkananattack, jarakraycast_attack,tolayer))
            {
                   Debug.DrawRay(kanansisi.position, forwardkananattack, Color.green);  
                   enemy.serangkanan = true;
            }
            else
            {
                Debug.DrawRay(kanansisi.position, forwardkananattack, Color.blue);
                enemy.serangkanan = false;

            }
            if(Physics2D.Raycast(kirisisi.position, forwardkiriattack, jarakraycast_attack,tolayer))
            {
                   Debug.DrawRay(kirisisi.position, forwardkiriattack, Color.green);  
                   enemy.serangkiri = true;
            }
            else
            {
                Debug.DrawRay(kirisisi.position, forwardkiriattack, Color.blue);
                enemy.serangkiri = false;

            }



            // jarak lompatan musuh (jump)
            if(Physics2D.Raycast(atas.position, forwardatas, jarakraycast_atas,tolayer))
            {
                   Debug.DrawRay(atas.position, forwardatas, Color.green);
                   jump = true;
            }
            }
            else
            {
                enemy.kanan = false;
                enemy.kiri = false;
                enemy.serangkiri = false;
                enemy.serangkanan = false;
                jump = false;
            }
    }

    void FixedUpdate()
    {
        enemy.enemys(jump);
        jump = false;
    }
}
