using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Manager : Deck_Manager
{
    [SerializeField]
    private List<Card> Hand;
    private GameObject Target;

    public void Draw_Card(int cards_drawn)
    {
        for (int i = 0; i < cards_drawn; i++)
        {
            if (Hand.Count < 11)
            {
                Hand.Add(Deck[0]);
                Deck.RemoveAt(0);
            }
            if (Deck.Count == 0)
            {
                Deck_Out();
            }
        }      
    }

    public void Play_Card(Card Played_Card)
    {

        if (Player_Manager.Can_Play(Played_Card.Cost))
        {
            for (int i = 0; i < Played_Card.Functions.Count; i++)
                switch (Played_Card.Functions[i])
                {
                    case "Draw":
                        Draw_Card(Played_Card.Function_Values[i]);
                        break;
                    case "Damage":
                        Target_Select();
                        if (Target.tag == "Enemy")
                        {
                            Target.Enemy_Manager.Damage(Played_Card.Function_Values[i]);
                            break;
                        }
                        else
                        {
                            goto Not_Played;
                        }
                    case null:
                        break;
                }
            Hand.Remove(Played_Card);
            Discard.Add(Played_Card);
            Player_Manager.Energy_Loss(Played_Card.Cost);
        Not_Played:;
        }
    }

    IEnumerator Target_Select()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit Hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit))
                {
                    Target = Hit.transform.gameObject;
                }
                yield break;
            }
            yield return null;
        }
    }


    public void Reset_Hand()
    {
       for (int i = 0; i < Hand.Count; i++)
        {
            Discard.Add(Hand[i]);
            Hand.RemoveAt(i);
        }
        Draw_Card(5);
    }
}
