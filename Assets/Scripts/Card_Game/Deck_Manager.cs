using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Deck_Manager : MonoBehaviour
{
    [SerializeField]
    public List<Card> Deck;
    public List<Card> Discard;
    private List<Card> Exhaust;
    private int Random_Position;
    private Card Temp_Card;
    [SerializeField]
    private TextMeshProUGUI Discard_Text;
    [SerializeField]
    private TextMeshProUGUI Deck_Text;
    private int Random_Int;
    private GameObject Hand;

    //when the deck is loaded, displays how many cards are in it and the discard pile
    public void Awake()
    {
        Hand = GameObject.FindWithTag("Hand");
        Display_Deck_Count();
        Display_Discard_Count();
        Shuffle_Deck();
        Hand.GetComponent<Hand_Manager>().Draw_Card(5);
    }

    //moves the cards in the deck around randomly
    public void Shuffle_Deck()
    {
        System.Random New_Random = new System.Random();
        for (int i = 0;  i < Deck.Count; i++)
        {
            Random_Int = i + (int)(New_Random.NextDouble() * (Deck.Count - i));
            Temp_Card = Deck[Random_Int];
            Deck[Random_Int] = Deck[i];
            Deck[i] = Temp_Card;
        }
    }

    //Displays how many cards are in the deck
    public void Display_Deck_Count()
    {
        Deck_Text.text = "Deck: " + Deck.Count.ToString();
    }

    //displays how many cards are in the discard pile
    public void Display_Discard_Count()
    {
        Discard_Text.text = "Discard: " + Discard.Count.ToString();
    }

    //The deck becomes the discard pile, and the discard pile gets emptied. Then it shuffles the deck
    public void Deck_Out()
    {
        for (int i = 0; i < Discard.Count; i++)
        {
            Deck.Add(Discard[i]);
        }
        Discard.Clear();
        Display_Deck_Count();
        Display_Discard_Count();
        Shuffle_Deck();
    }

    //if the deck isn't full, add a card to the deck
    public void Add_Card(Card Added_Card)
    {
        if (Deck.Count < 15)
        {
            Deck.Add(Added_Card);
        }
    }

    //if the deck isn't empty and if the deck contains the card you want to remove, removes the card
    public void Remove_Card(Card Removed_Card)
    {
        if (Deck.Count > 0 || Deck.Contains(Removed_Card))
        {
            Deck.Remove(Removed_Card);
        }
    }
}
