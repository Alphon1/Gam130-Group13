using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DRMove : MonoBehaviour
{
    public GameObject player;
    public GameObject room;
    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public void Move()
    {
        player = GameObject.Find("Player");
        room = GameObject.FindGameObjectWithTag("DialogueRoom");
        Debug.Log("Player Move Dialogue/other room");
        player.transform.position = room.transform.position;
        player.transform.position = player.transform.position + new Vector3(offsetX, offsetY, offsetZ);
    }
}
