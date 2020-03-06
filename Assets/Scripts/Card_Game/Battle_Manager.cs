using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Manager : MonoBehaviour
{
    [SerializeField]
    private bool Is_Player_Turn;
    private GameObject Player;
    private GameObject Hand;

    //when the battle manager is loaded, it finds the Player and Hand and declares it is the player's turn
    void Start()
    {
        Is_Player_Turn = true;
        Player = GameObject.FindWithTag("Player");
        Hand = GameObject.FindWithTag("Hand");
    }

    //checks whos turn it is, then changes the turn. If it changes to the enemy's turn it tells the enemy to do something,
    // if it changes to the player's turn it resets their hand and energy
    public void Turn_Switch()
    {
        if (Is_Player_Turn == true)
        {
            Hand.GetComponent<Hand_Manager>().End_Of_Turn();
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

    //checks if the player can end their turn, if they can then it changes turn
    public void End_Turn()
    {
        if (Is_Player_Turn)
        {        
            Turn_Switch();
        }
    }

    //checks if its the player's turn
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
