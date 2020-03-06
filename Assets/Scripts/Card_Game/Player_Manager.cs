using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Manager : MonoBehaviour
{
    [SerializeField]
    private int Max_Health;
    private int Health;
    [SerializeField]
    private int Starting_Energy;
    private int Energy;
    [SerializeField]
    private TextMeshProUGUI Energy_Display;
    [SerializeField]
    private Slider Health_Display;
    [SerializeField]
    private TextMeshProUGUI Numerical_Display;
    private int Temp_Starting_Energy;

    //when the player first loads, they have max health and energy
    private void Awake()
    {
        Temp_Starting_Energy = Starting_Energy;
        Energy = Starting_Energy;
        Health = Max_Health;
        Health_Display.value = Health;
        Numerical_Display.text = Health.ToString();
        Energy_Display.text = "Energy: " + Energy.ToString();
    }

    //Changes the player's health, without letting their health go over max
    public void Health_Change(int Health_Removed)
    {
        Health -= Health_Removed;
        if (Health > Max_Health)
        {
            Health = Max_Health;
        }
        Health_Display.value = Health;
        Numerical_Display.text = Health.ToString();
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

    public void Max_Health_Change(int Change)
    {
        Max_Health += Change;
    }

    public void Temp_Starting_Energy_Change(int Energy_Added)
    {
        Temp_Starting_Energy += Energy_Added;
    }

    public void Starting_Energy_Change(int Energy_Added)
    {
        Starting_Energy += Energy_Added;
    }

    //changes the player's energy, without letting it go over max
    public void Energy_Change(int Energy_Removed)
    {
        Energy -= Energy_Removed;
        Energy_Display.text = "Energy: " + Energy.ToString();
    }

    //resets the player's energy to full
    public void Reset_Energy()
    {
        Energy = Temp_Starting_Energy;
        Temp_Starting_Energy = Starting_Energy;
        Energy_Display.text = "Energy: " + Energy.ToString();
    }
}
