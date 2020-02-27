using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField]
    private int Max_Health;
    [SerializeField]
    private int Health;
    [SerializeField]
    private int Damage_Dealt;
    [SerializeField]
    private int Healing;
    private int Decision;
    private GameObject Player;

    //when an enemy is first loaded it has max health
    private void Awake()
    {
        Health = Max_Health;
        Player = GameObject.FindWithTag("Player");
    }

    // if it's the enemy's turn, they randomly decide to attack the player for their damage, or heal themselves for their healing
    public void Enemy_Turn()
    {
        Decision = Random.Range(0, 2);
        switch (Decision)
        {
            case 0:
                Player.GetComponent<Player_Manager>().Health_Change(Damage_Dealt);
                break;
            case 1:
                Health += Healing;
                if (Health > Max_Health)
                {
                    Health = Max_Health;
                }
                break;
        }
    }

    //takes health away from the enemy
    public void Damage(int Damage_Taking)
    {
        Health -= Damage_Taking;
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
