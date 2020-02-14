using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Manager : Deck_Manager
{
    [SerializeField]
    private List<Card> Hand;

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
            switch (Played_Card.Function1)
            {
                    case "Draw":
                        Draw_Card(Played_Card.Function1_value);
                        break;
                    case null:
                    goto End_Play;
            }
            switch (Played_Card.Function2)
            {
                case "Draw":
                    Draw_Card(Played_Card.Function2_value);
                    break;
                case null:              
                goto End_Play;
        }
            switch (Played_Card.Function3)
            {
                case "Draw":
                    Draw_Card(Played_Card.Function3_value);
                    break;
                case null:   
                goto End_Play;
        }
            switch (Played_Card.Function4)
            {
                case "Draw":
                    Draw_Card(Played_Card.Function4_value);
                    break;
                case null:
                goto End_Play;
            }
            switch (Played_Card.Function5)
            {
                case "Draw":
                    Draw_Card(Played_Card.Function5_value);
                    break;
                case null:
                goto End_Play;
            }
    End_Play:;
        Hand.Remove(Played_Card);
        Discard.Add(Played_Card);
        
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
