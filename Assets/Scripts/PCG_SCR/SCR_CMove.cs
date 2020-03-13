using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CMove : MonoBehaviour
{
    public GameObject player;
    public GameObject room;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public GameObject battleUI;
    public void Move()
    {
        player = GameObject.Find("Player");
        room = GameObject.FindGameObjectWithTag("Room 3");
        //battleUI = GameObject.Find("Temp_Battle_UI");
        Debug.Log("Player Move");
        player.transform.position = room.transform.position;
        player.transform.position = player.transform.position + new Vector3(offsetX, offsetY, offsetZ);
        //battleUI.SetActive(true);
    }
}
