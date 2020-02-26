using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    [SerializeField]
    private int Max_Health;
    private int Health;
    [SerializeField]
    private int Max_Energy;
    private int Energy;

    //when the player first loads, they have max health and energy
    private void Awake()
    {
        Energy = Max_Energy;
        Health = Max_Health;
    }

    //Changes the player's health, without letting their health go over max
    public void Health_Change(int Health_Removed)
    {
        Health -= Health_Removed;
        if (Health > Max_Health)
        {
            Health = Max_Health;
        }
    }

    //checks if the player has enough energy for that action
    public bool Can_Play(int Energy_Cost)
    {
        if (Energy - Energy_Cost >= 0)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    //changes the player's energy, without letting it go over max
    public void Energy_Change(int Energy_Removed)
    {
        Energy -= Energy_Removed;
        if (Energy > Max_Energy)
        {
            Reset_Energy();
        }
    }

    //resets the player's energy to full
    public void Reset_Energy()
    {
        Energy = Max_Energy;
    }
}
