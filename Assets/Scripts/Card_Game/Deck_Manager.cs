using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck_Manager : MonoBehaviour
{
    [SerializeField]
    protected List<Card> Deck;
    protected List<Card> Discard;
    protected List<Card> Exhaust;
    private int Random_Position;
    private Card Temp_Card;
    [SerializeField]
    private Text Discard_Text;

    public void Awake()
    {
        Display_Deck_Count();
    }
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

    public void Display_Deck_Count()
    {
        gameObject.GetComponent<Text>().text = Deck.Count.ToString();
    }

    public void Display_Discard_Count()
    {
        Discard_Text.text = Discard.Count.ToString();
    }

    public void Deck_Out()
    {
        Deck = Discard;
        Display_Deck_Count();
        Discard = null;
        Display_Discard_Count();
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
