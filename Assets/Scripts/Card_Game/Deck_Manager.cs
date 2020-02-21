using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck_Manager : MonoBehaviour
{
    [SerializeField]
    protected List<Card> Deck;
    protected List<Card> Discard;
    protected List<Card> Exhaust;
    private int Random_Position;
    private Card Temp_Card;

    public void Shuffle_Deck()
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            Temp_Card = Deck[i];
            Random_Position = Random.Range(i, Deck.Count);
            Deck[i] = Deck[Random_Position];
            Deck[Random_Position] = Temp_Card;
        }
    }

    public void Deck_Out()
    {
        Deck = Discard;
        Discard = null;
        Shuffle_Deck();
    }

    public void Add_Card(Card Added_Card)
    {
        if (Deck.Count < 15)
        {
            Deck.Add(Added_Card);
        }
    }

    public void Remove_Card(Card Removed_Card)
    {
        if (Deck.Count > 0 || Deck.Contains(Removed_Card))
        {
            Deck.Remove(Removed_Card);
        }
    }
}
