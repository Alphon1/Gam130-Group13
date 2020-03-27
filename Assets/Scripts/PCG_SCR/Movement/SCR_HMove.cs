using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HMove : MonoBehaviour
{
    public GameObject player;
    public GameObject room;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public GameObject battleUI;

    public void Start()
    {
        player = GameObject.Find("Player");
        room = GameObject.FindGameObjectWithTag("HealthRoom");
        battleUI = GameObject.Find("Battle_UI_V3");
    }

    public void Move()
    {
        Debug.Log("Player Move Health");
        player.transform.position = room.transform.position;
        player.transform.position = player.transform.position + new Vector3(offsetX, offsetY, offsetZ);
        battleUI.SetActive(false);
    }
}
