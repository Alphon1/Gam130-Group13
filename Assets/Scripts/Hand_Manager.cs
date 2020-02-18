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
                        Target.Enemy_Manager.Damage(Played_Card.Function_Values[i]);
                    case null:
                        goto End_Play;
                }

            End_Play:;
            Hand.Remove(Played_Card);
            Discard.Add(Played_Card);
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
