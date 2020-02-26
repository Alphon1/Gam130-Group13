using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Values : Hand_Manager
{
    private Card Displayed_Card;
    [SerializeField]
    private Text Name_Text;
    [SerializeField]
    private Text Cost_Text;
    [SerializeField]
    private Text Description_Text;
    [SerializeField]
    private Text Type_Text;
    [SerializeField]
    private Image Artwork;
    [SerializeField]
    private Button Card_Button;
    private GameObject Player;
    private int Card_Number;
    private void Awake()
    {
        switch (this.gameObject.name)
        {
            case "Card 1":
                Card_Number = 0;
                break;
            case "Card 2":
                Card_Number = 1;
                break;
            case "Card 3":
                Card_Number = 2;
                break;
            case "Card 4":
                Card_Number = 3;
                break;
            case "Card 5":
                Card_Number = 4;
                break;
            case "Card 6":
                Card_Number = 5;
                break;
            case "Card 7":
                Card_Number = 6;
                break;
            case "Card 8":
                Card_Number = 7;
                break;
            case "Card 9":
                Card_Number = 8;
                break;
            case "Card 10":
                Card_Number = 9;
                break;
        }
        Player = GameObject.FindWithTag("Player");
        Update_Display();
    }

    public void Update_Display()
    {
        if (Hand.Count > Card_Number)
        {
            Displayed_Card = Hand[Card_Number];
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            Displayed_Card = null;
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        Name_Text.text = Displayed_Card.Name;
        Cost_Text.text = Displayed_Card.Cost.ToString();
        Description_Text.text = Displayed_Card.Description;
        Type_Text.text = Displayed_Card.Type;
        Artwork.sprite = Displayed_Card.Art;
        Set_Card_Function(Displayed_Card);
    }

    public void Set_Card_Function(Card Reset_Card)
    {
        Card_Button.onClick.RemoveAllListeners();
        Card_Button.onClick.AddListener(delegate { Play_Card(Displayed_Card); });
    }
}
