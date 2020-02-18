using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    [SerializeField]
    private int Max_Health;
    private int Health;
    [SerializeField]
    private int Damage_Dealt;
    [SerializeField]
    private int Healing;
    private int Decision;
    private GameObject Player;

    private void Awake()
    {
        Health = Max_Health;
    }
    public void Enemy_Turn()
    {
        Decision = Random.Range(0, 2);
        switch (Decision)
        {
            case 0:
                Player.GetComponent<Player_Manager>().Damage(Damage_Dealt);
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

    public void Damage(int Damage_Taking)
    {
        Health -= Damage_Taking;
    }
}
