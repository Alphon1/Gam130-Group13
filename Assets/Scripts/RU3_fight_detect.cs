using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RU3_fight_detect : MonoBehaviour
{
    public GameObject RU3;
    public GameObject PostDialogue;
    public bool fightOver = false;
    private Enemy_Manager Enemyscript;

    void Update()
    {
        if (Enemyscript.Health >= 0)
        {
            //fightOver = true;
            PostDialogue.SetActive(true);
        }
    }
}
