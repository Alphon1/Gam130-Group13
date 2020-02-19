using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Manager : MonoBehaviour
{
    public bool Is_Player_Turn;
    private GameObject Player;

    void Start()
    {
        Is_Player_Turn = true;
        Player = GameObject.FindWithTag("Player");
    }

    public void Turn_Switch()
    {
        if (Is_Player_Turn == true)
        {
            Is_Player_Turn = false;
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<Enemy_Manager>().Enemy_Turn();
            }
            Turn_Switch();
            Player.GetComponent<Hand_Manager>().Reset_Hand();
        }
        else
        {
            Is_Player_Turn = true;
        }
    }

    public void End_Turn()
    {
        if (Is_Player_Turn)
        {
            Turn_Switch();
        }
    }
}
