using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CombatTrigger : MonoBehaviour
{
    public GameObject battleUI;
    private IEnumerator coroutine;

    void Start()
    {
        battleUI = GameObject.Find("Battle_UI_V3");
        coroutine = WaitDisable(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        battleUI.SetActive(false);
    }
}
