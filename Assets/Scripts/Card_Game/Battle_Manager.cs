﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Manager : MonoBehaviour
{
    private bool Is_Player_Turn;
    private GameObject Player;
    private GameObject Hand;

    void Start()
    {
        Is_Player_Turn = true;
        Player = GameObject.FindWithTag("Player");
        Hand = GameObject.FindWithTag("Hand");
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
        }
        else
        {
            Is_Player_Turn = true;
            Hand.GetComponent<Hand_Manager>().Reset_Hand();
            Player.GetComponent<Player_Manager>().Reset_Energy();
        }
    }

    public void End_Turn()
    {
        if (Is_Player_Turn)
        {
            Turn_Switch();
        }
    }

    public bool Player_Control()
    {
        if (Is_Player_Turn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
