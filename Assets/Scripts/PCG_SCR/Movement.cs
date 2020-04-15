using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject currentRoom;
    public GameObject player;
    public bool canForward;
    public bool canBackward;
    public GameObject battleUI;
    private IEnumerator coroutine;
    public GameObject[] CRooms;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    void Start()
    {
        canForward = false;
        canBackward = false;
        coroutine = WaitDisable(3.0f);
        StartCoroutine(coroutine);
        battleUI = GameObject.Find("Battle_UI_V3");

    }

    void Update()
    {
        if (currentRoom == GameObject.FindGameObjectWithTag("StartRoom"))
        {
            canBackward = false;
        }

        if (currentRoom == GameObject.FindGameObjectWithTag("BossRoom"))
        {
            canForward = false;
            battleUI.SetActive(true);
        }
    }

    public void Forward()
    {
        if (canForward == true)
        {
            currentRoom = currentRoom.transform.parent.GetChild(currentRoom.transform.GetSiblingIndex() + 1).gameObject;
            canBackward = true;
            Move();
            battleUI.SetActive(false);
            foreach (GameObject CRoom in CRooms)
            {
                if (currentRoom == CRoom)
                {
                    battleUI.SetActive(true);
                }
            }
        } 
    }

    public void Backward()
    {
        if (canBackward == true)
        {
            currentRoom = currentRoom.transform.parent.GetChild(currentRoom.transform.GetSiblingIndex() - 1).gameObject;
            canForward = true;
            Move();
            battleUI.SetActive(false);
        }
    }

    public void Move()
    {
        player.transform.position = currentRoom.transform.position;
        player.transform.position = player.transform.position + new Vector3(offsetX, offsetY, offsetZ);
    }

    private IEnumerator WaitDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentRoom = GameObject.FindGameObjectWithTag("StartRoom");
        player = GameObject.FindGameObjectWithTag("Player");
        canForward = true;
        canBackward = false;
        CRooms = GameObject.FindGameObjectsWithTag("CombatRoom");
    }
}
