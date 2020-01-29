using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Card : MonoBehaviour
{
    public Card Card;
    public Text Name;
    public Text Cost;
    public Text Description;
    public Image Art;
    // Start is called before the first frame update
    void Start()
    {
        Name.text = Card.Name;
        Cost.text = Card.Cost.ToString();
        Description.text = Card.Description;
        Art.sprite = Card.Art;
    }

}
