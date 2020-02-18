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

    private void Awake()
    {
        Energy = Max_Energy;
        Health = Max_Health;
    }
    public void Health_Change(int Health_Removed)
    {
        Health -= Health_Removed;
        if (Health > Max_Health)
        {
            Health = Max_Health;
        }
    }

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

    public void Energy_Change(int Energy_Removed)
    {
        Energy -= Energy_Removed;
        if (Energy > Max_Energy)
        {
            Energy = Max_Energy;
        }
    }
}
