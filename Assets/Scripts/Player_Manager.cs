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
    public void Damage(int Damage_Taking)
    {
        Health -= Damage_Taking;
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

    public void Energy_Loss(int Energy_Lost)
    {
        Energy -= Energy_Lost;
    }
}
