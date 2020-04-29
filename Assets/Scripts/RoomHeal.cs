using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomHeal : MonoBehaviour
{
    private Player_Manager playerScript;
    public GameObject Player;
    public GameObject battleUI;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player_UI");
        playerScript = Player.GetComponent<Player_Manager>();
        battleUI = GameObject.Find("Battle_UI_V3");
    }

    private void OnCollisionEnter(Collision Player)
    {
        Debug.Log("Enter");
        battleUI.SetActive(true);
        playerScript.Health = 50;
        battleUI.SetActive(false);
    }
}
