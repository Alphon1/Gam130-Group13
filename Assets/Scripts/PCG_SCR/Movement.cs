using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject currentRoom;
    public GameObject player;
    public bool canForward;
    public bool canBackward;

    private IEnumerator coroutine;

    void Start()
    {
        canForward = false;
        canBackward = false;
        coroutine = WaitDisable(3.0f);
        StartCoroutine(coroutine);
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
        }
    }

    public void Forward()
    {
        if (canForward == true)
        {
            currentRoom = currentRoom.transform.parent.GetChild(currentRoom.transform.GetSiblingIndex() + 1).gameObject;
            canBackward = true;
            player.transform.position = currentRoom.transform.position;
        } 
    }

    public void Backward()
    {
        if (canBackward == true)
        {
            currentRoom = currentRoom.transform.parent.GetChild(currentRoom.transform.GetSiblingIndex() - 1).gameObject;
            canForward = true;
            player.transform.position = currentRoom.transform.position;
        }
    }

    private IEnumerator WaitDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentRoom = GameObject.FindGameObjectWithTag("StartRoom");
        player = GameObject.FindGameObjectWithTag("Player");
        canForward = true;
        canBackward = false;
    }
}
