using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Manager : Deck_Manager
{
    [SerializeField]
    private List<Card> Hand;
    private string card_function;

    public void Draw_Card(int cards_drawn)
    {
        for (int i = 0; i < cards_drawn; i++)
        {
            if (Hand.Count < 11)
            {
                Hand.Add(Deck[0]);
                Deck.RemoveAt(0);
            }
        }
        
    }
    public void Play_Card(Card Played_Card)
    {
        switch (Played_Card.Function)
        {
            case "Draw":
                Draw_Card(1);
            break;
        }
    }

    public void Reset_Hand()
    {

    }
}
