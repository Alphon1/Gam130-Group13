using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck_Manager : MonoBehaviour
{
    [SerializeField]
    public List<Card> Deck;
    private List<Card> Temp_Deck;
    private List<Card> Discard;
    private int Random_Position;

    public void Shuffle_Deck()
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            Random_Position = Random.Range(0, Deck.Count);
            Temp_Deck[i] = Deck[Random_Position];
            Deck.RemoveAt(Random_Position);
        }
        Deck = Temp_Deck;
    }

    public void Deck_Out()
    {
        Deck = Discard;
        Shuffle_Deck();
    }
}
