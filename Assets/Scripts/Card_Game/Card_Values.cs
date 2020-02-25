using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Values : MonoBehaviour
{
    private Card Displayed_Card;
    public Text Name_Text;
    public Text Cost_Text;
    public Text Description_Text;
    public Text Type_Text;
    public Image Artwork;
    public Button Card_Button;
    private GameObject Player;
    private bool Card_Active;
    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Name_Text.text = Displayed_Card.Name;
        Cost_Text.text = Displayed_Card.Cost.ToString();
        Description_Text.text = Displayed_Card.Description;
        Type_Text.text = Displayed_Card.Type;
        Artwork.sprite = Displayed_Card.Art;
    }

    public void Update_Display(List<Card> Hand)
    {
        switch (this.gameObject.name)
        {
            case "Card 1":
                if (Hand.Count > 0)
                {
                    Displayed_Card = Hand[0];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 2":
                if (Hand.Count > 1)
                {
                    Displayed_Card = Hand[1];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 3":
                if (Hand.Count > 2)
                {
                    Displayed_Card = Hand[2];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 4":
                if (Hand.Count > 3)
                {
                    Displayed_Card = Hand[3];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 5":
                if (Hand.Count > 4)
                {
                    Displayed_Card = Hand[4];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 6":
                if (Hand.Count > 5)
                {
                    Displayed_Card = Hand[5];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 7":
                if (Hand.Count > 6)
                {
                    Displayed_Card = Hand[6];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 8":
                if (Hand.Count > 7)
                {
                    Displayed_Card = Hand[7];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 9":
                if (Hand.Count > 8)
                {
                    Displayed_Card = Hand[8];
                }
                else
                {
                    Card_Active = false;
                }
                break;
            case "Card 10":
                if (Hand.Count > 0)
                {
                    Displayed_Card = Hand[9];
                }
                else
                {
                    Card_Active = false;
                }
                break;
        }
        Name_Text.text = Displayed_Card.Name;
        Cost_Text.text = Displayed_Card.Cost.ToString();
        Description_Text.text = Displayed_Card.Description;
        Type_Text.text = Displayed_Card.Type;
        Artwork.sprite = Displayed_Card.Art;
        if (!Card_Active)
        {
            Displayed_Card = null;
            gameObject.GetComponent <Renderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
