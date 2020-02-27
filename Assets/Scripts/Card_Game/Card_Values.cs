using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Values : MonoBehaviour
{ 
    public Card Displayed_Card;
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
    [SerializeField]
    private int Card_Number;
    private GameObject Hand_Object;
    [SerializeField]
    private GameObject Card_Visuals;
   
    //When the card loads it find the Hand and which number card it is
    private void Awake()
    {
       
        Hand_Object = GameObject.FindWithTag("Hand");
        Card_Button.onClick.AddListener(delegate { Hand_Object.GetComponent<Hand_Manager>().Play_Card(Displayed_Card); });
        Card_Number = Get_Card_Number();
    }


    //finds which card this is based on it's name
    public int Get_Card_Number()
    {
        switch (gameObject.name)
        {
            case "Card 1":
                return 0;
            case "Card 2":
                return 1;
            case "Card 3":
                return 2;
            case "Card 4":
                return 3;
            case "Card 5":
                return 4;
            case "Card 6":
                return 5;
            case "Card 7":
                return 6;
            case "Card 8":
                return 7;
            case "Card 9":
                return 8;
            case "Card 10":
                return 9;
            default:
                return 11;    
        }
    }

    //Changes what's written on the card to the proper values of what it's meant to represent
    public void Enable_Display(List<Card> Hand)
    {
        Card_Visuals.SetActive(true);
        Displayed_Card = Hand[Card_Number];
        Name_Text.text = Displayed_Card.Name;
        Cost_Text.text = Displayed_Card.Cost.ToString();
        Description_Text.text = Displayed_Card.Description;
        Type_Text.text = Displayed_Card.Type;
        Artwork.sprite = Displayed_Card.Art;
    }

    public void Disable_Display()
    {
        Card_Visuals.SetActive(false);
    }
}
