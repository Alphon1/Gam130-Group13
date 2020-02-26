using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card_Values : MonoBehaviour
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
    private int Card_Number;
    private GameObject Hand_Object;
   
    private void Awake()
    {
        Hand_Object = GameObject.FindWithTag("Hand");
        Card_Number = Get_Card_Number();
    }

    public int Get_Card_Number()
    {
        switch (this.gameObject.name)
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

    public void Update_Display(bool Card_Enabled, List<Card> Hand)
    {
        if (Card_Enabled)
        {
            Displayed_Card = Hand[Card_Number];
        }
        else
        {
            Displayed_Card = null;
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
        Card_Button.onClick.AddListener(delegate { Hand_Object.GetComponent<Hand_Manager>().Play_Card(Displayed_Card); });
    }
}
