using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player_Manager : MonoBehaviour
{
    [SerializeField]
    private int Max_Health;
    public int Health;
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
    private int Armour;
    private int HOT_Duration;
    private int HOT_Healing;
    [SerializeField]
    private TextMeshProUGUI Armour_Display;
    private float Life_Stolen_Percentage;
    private float Damage_Reduction;
    private float Damage_Deflected;

    //when the player first loads, they have max health and energy
    private void Awake()
    {
        Starting_Energy = 7;
        Temp_Starting_Energy = Starting_Energy;
        Energy = Starting_Energy;
        Health = Max_Health;
        Display_Values();
    }

    public void Set_Deflection(int Percentage_Damage_Deflected)
    {
        Set_Damage_Reduction(Percentage_Damage_Deflected);
        Damage_Deflected = 1 - (Percentage_Damage_Deflected * 0.01f);
    }

    public float Check_Deflection()
    {
        return Damage_Deflected;
    }

    public void Add_Armour(int Armour_Added)
    {
        Armour += Armour_Added;
        Armour_Display.text = Armour.ToString();
    }

    public void Set_Damage_Reduction(int Percentage_Damage_Reduced)
    {
        Damage_Reduction = 1- (Percentage_Damage_Reduced * 0.01f); 
    }

    //Changes the player's health, without letting their health go over max
    public void Health_Change(int Health_Removed)
    {
        if (Health_Removed > 0)
        {
            if (Damage_Reduction > 0)
            {
                Health_Removed = Mathf.RoundToInt(Health_Removed * Damage_Reduction);
            }
            if (Armour - Health_Removed < 0)
            {
                Health = Health - (Health_Removed - Armour);
                Armour = 0;
            }
            else
            {
                Armour -= Health_Removed;
            }
        }
        else
        {
            Health -= Health_Removed;
        }
        if (Health > Max_Health)
        {
            Health = Max_Health;
        }
        Display_Values();
    }

    public void Display_Values()
    {
        Health_Display.value = Health;
        Numerical_Display.text = Health.ToString();
        Armour_Display.text = Armour.ToString();
        Energy_Display.text = "Energy: " + Energy.ToString();
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
        Display_Values();
    }

    //resets the player's energy to full
    public void Reset_Energy()
    {
        Energy = Temp_Starting_Energy;
        Temp_Starting_Energy = Starting_Energy;
        Display_Values();
    }

    public void Set_HOT(int Healing)
    {
        HOT_Duration = 3;
        HOT_Healing = Healing;
    }

    public void HOT_Tick()
    {
        if (HOT_Duration > 0)
        {
            HOT_Duration -= 1;
            Health_Change(-HOT_Healing);
        }
    }

    public void Set_Life_Steal(int Percentage_Stolen)
    {
        Life_Stolen_Percentage = Percentage_Stolen * 0.01f;
    }

    public float Check_Life_Steal()
    {
        return Life_Stolen_Percentage;
    }
    public void Double_Armour()
    {
        Add_Armour(Armour);
    }
}
