using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField]
    private int Min_Possible_Health;
    [SerializeField]
    private int Max_Possible_Health;
    private int Max_Health;
    private int Health;
    [SerializeField]
    private int Min_Damage;
    [SerializeField]
    private int Max_Damage;
    private int Damage_Dealt;
    [SerializeField]
    private int Min_Healing;
    [SerializeField]
    private int Max_Healing;
    private int Healing;
    private int Decision;
    private GameObject Player;
    [SerializeField]
    private Text Health_Display;

    //when an enemy is first loaded it has max health
    private void Awake()
    {
        Max_Health = Random.Range(Min_Possible_Health, Max_Possible_Health);
        Health = Max_Health;
        Player = GameObject.FindWithTag("Player");
        Health_Display.text = Health.ToString();
    }

    // if it's the enemy's turn, they randomly decide to attack the player for their damage, or heal themselves for their healing
    public void Enemy_Turn()
    {
        Damage_Dealt = Random.Range(Min_Damage, Max_Damage);
        if (Health < Max_Health)
        {
            Decision = Random.Range(0, 2);
            switch (Decision)
            {
                case 0:
                    Player.GetComponent<Player_Manager>().Health_Change(Damage_Dealt);
                    break;
                case 1:
                    Healing = Random.Range(Min_Healing, Max_Healing);
                    Health += Healing;
                    if (Health > Max_Health)
                    {
                        Health = Max_Health;
                    }
                    Health_Display.text = Health.ToString();
                    break;
            }
        }
        else
        {
            Player.GetComponent<Player_Manager>().Health_Change(Damage_Dealt);
        }
    }

    //takes health away from the enemy
    public void Damage(int Damage_Taking)
    {
        Health -= Damage_Taking;
        Health_Display.text = Health.ToString();
        Death_Check();
    }
    public void Death_Check()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    //takes health away from the enemy, and says if the damage killed it or not
    public bool Lethal_Damage(int Damage_Taking)
    {
        Damage(Damage_Taking);
        if (Health <= 0)
        {
            Death_Check();
            return true;
        }
        else
        {
            return false;
        }
    }
}
