using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject currentRoom;
    public bool canForward;
    public bool canBackward;

    private IEnumerator coroutine;

    void Start()
    {
        canForward = false;
        canBackward = false;
        coroutine = WaitDisable(1.0f);
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

    void Forward()
    {
        if (canForward == true)
        {
            currentRoom = currentRoom.transform.parent.GetChild(currentRoom.transform.GetSiblingIndex() + 1).gameObject;
            canBackward = true;
        } 
    }

    void Backward()
    {
        if (canBackward == true)
        {
            currentRoom = currentRoom.transform.parent.GetChild(currentRoom.transform.GetSiblingIndex() - 1).gameObject;
            canForward = true;
        }
    }

    private IEnumerator WaitDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        currentRoom = GameObject.FindGameObjectWithTag("StartRoom");
        canForward = true;
        canBackward = true;
    }
}
