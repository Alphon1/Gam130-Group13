using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queued_Function
{
    public Card Source_Card;
    public int Function_Start_Point;
}
public class Hand_Manager : MonoBehaviour
{
    [SerializeField]
    private List<Card> Hand;
    private List<Card> Exhaust;
    private GameObject Player;
    private GameObject Turn_Order;
    [SerializeField]
    private GameObject Deck_Object;
    private int Starting_Function;
    private bool Freeze_Player_Control;
    private int On_Draw_Damage = 0;
    private int On_Draw_Healing = 0;
    public List<Queued_Function> Queued_Functions = new List<Queued_Function>();
    private Queued_Function Function_To_Queue;
    private int Random_Target;
    private int Loops = 0;

    //When the hand is first loaded, finds which objects are the deck, the player, and the battle manager,
    //and calls a function to draw 5 cards
    public void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Turn_Order = GameObject.FindWithTag("Battle_Manager");
        Deck_Object = GameObject.FindWithTag("Deck");
        Starting_Function = 0;
        Draw_Card(5);
    }

    public void Reset_Exhaust()
    {
        for (int i = 0; i < Exhaust.Count; i++)
        {
            Deck_Object.GetComponent<Deck_Manager>().Deck.Add(Exhaust[i]);
        }
        Exhaust.Clear();
    }
    public void End_Of_Turn()
    {
        On_Draw_Damage = 0;
        On_Draw_Healing = 0;
        Player.GetComponent<Player_Manager>().Set_Life_Steal(0);
    }

    public void Start_Of_Turn()
    {
        Reset_Hand();
        Player.GetComponent<Player_Manager>().HOT_Tick();
        Player.GetComponent<Player_Manager>().AOT_Tick();
        Player.GetComponent<Player_Manager>().Set_Damage_Reduction(0);
        Player.GetComponent<Player_Manager>().Set_Deflection(0);
        for (int i = 0; i < Queued_Functions.Count; i++)
        {
            Starting_Function = Queued_Functions[i].Function_Start_Point;
            Play_Card(Queued_Functions[i].Source_Card);
        }
    }
    //Goes through each card in the scene by tag, and if they are needed to display a card in the hand it enables them
    //and tells them to update their displayed values. If they aren't needed it disables them
    public void Enable_Cards()
    {
        for (int i = 0; i < 10; i++)
        {
            if (Hand.Count > GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Card_Values>().Get_Card_Number())
            {
                GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Card_Values>().Enable_Display(Hand);
            }
            else
            {
                GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Card_Values>().Disable_Display();
            }
        }
    }

    //moves a set number of cards from the deck to the hand, 
    //if the deck runs out of card during this it tells the deck to swap with the discard pile
    public void Draw_Card(int Cards_Drawn)
    {
        for (int i = 0; i < Cards_Drawn; i++)
        {
            if (Hand.Count < 11)
            {
                Hand.Add(Deck_Object.GetComponent<Deck_Manager>().Deck[0]);
                Deck_Object.GetComponent<Deck_Manager>().Deck.RemoveAt(0);
                if (Hand[Hand.Count - 1].Functions[0] == "When Drawn")
                {
                    Starting_Function = 0;
                    Play_Card(Hand[Hand.Count - 1]);
                }
                if (On_Draw_Damage > 0)
                {
                    GameObject.FindGameObjectsWithTag("Enemy")[Random.Range(0, GameObject.FindGameObjectsWithTag("Enemy").Length)].GetComponent<Enemy_Manager>().Damage(On_Draw_Damage);
                }
                if (On_Draw_Healing > 0)
                {
                    Player.GetComponent<Player_Manager>().Health_Change(-On_Draw_Healing);
                }
            }
            if (Deck_Object.GetComponent<Deck_Manager>().Deck.Count == 0)
            {
                Deck_Object.GetComponent<Deck_Manager>().Deck_Out();
            }
        }
        Deck_Object.GetComponent<Deck_Manager>().Display_Deck_Count();
        Enable_Cards();
    }

    //This is called when the card is played, and tells the rest of the game what the card does based on its functions
    public void Play_Card(Card Played_Card)
    {
        if (Turn_Order.GetComponent<Battle_Manager>().Player_Control() == true && Freeze_Player_Control == false)
        {
            if (Player.GetComponent<Player_Manager>().Can_Play(Played_Card.Cost))
            {
                for (int i = Starting_Function; i < Played_Card.Functions.Count; i++)
                {
                    switch (Played_Card.Functions[i])
                    {
                        case "Draw":
                            Draw_Card(Played_Card.Function_Values[i]);
                            break;
                        case "Damage":
                            StartCoroutine(Target_Select("Damage",Played_Card, i));
                            Freeze_Player_Control = true;
                            Starting_Function = i + 1;
                            goto Stop_Card;
                        case "Heal":
                            Player.GetComponent<Player_Manager>().Health_Change(-(Played_Card.Function_Values[i]));
                            break;
                        case "Add Energy":
                            Player.GetComponent<Player_Manager>().Energy_Change(-(Played_Card.Function_Values[i]));
                            break;
                        case "Lethal Damage":
                            StartCoroutine(Target_Select("Lethal Damage",Played_Card, i));
                            Freeze_Player_Control = true;
                            Starting_Function = i + 1;
                            goto Stop_Card;
                        case "Shuffle Discard":
                            for (int j = 0; j < Deck_Object.GetComponent<Deck_Manager>().Discard.Count; j++)
                            {
                                Deck_Object.GetComponent<Deck_Manager>().Deck.Add(Deck_Object.GetComponent<Deck_Manager>().Discard[j]);
                                Deck_Object.GetComponent<Deck_Manager>().Display_Deck_Count();
                                Deck_Object.GetComponent<Deck_Manager>().Discard.RemoveAt(j);
                                Deck_Object.GetComponent<Deck_Manager>().Display_Discard_Count();
                            }
                            Deck_Object.GetComponent<Deck_Manager>().Shuffle_Deck();
                            break;
                        case "Random Damage":
                            for (int j = 0; j < Played_Card.Function_Values[i]; j++)
                            {

                                GameObject.FindGameObjectsWithTag("Enemy")[Random.Range(0, GameObject.FindGameObjectsWithTag("Enemy").Length)].GetComponent<Enemy_Manager>().Damage(1);
                            }
                            break;
                        case "AOE Damage":
                            for (int j = 0; j < GameObject.FindGameObjectsWithTag("Enemy").Length; j++)
                            {
                                GameObject.FindGameObjectsWithTag("Enemy")[j].GetComponent<Enemy_Manager>().Damage(Played_Card.Function_Values[i]);
                            }
                            break;
                        case "Discard":       
                            StartCoroutine(Target_Select("Discard", Played_Card, Played_Card.Function_Values[i]));
                            Freeze_Player_Control = true;
                            Starting_Function = i+1;
                            goto Stop_Card;
                        case "Exhaust":
                            Starting_Function = 0;
                            Hand.Remove(Played_Card);
                            Enable_Cards();
                            Exhaust.Add(Played_Card);
                            goto Stop_Card;
                        case "Max HP Change":
                            Player.GetComponent<Player_Manager>().Max_Health_Change(Played_Card.Function_Values[i]);
                            break;
                        case "DOT":
                            StartCoroutine(Target_Select("DOT", Played_Card, i));
                            Freeze_Player_Control = true;
                            Starting_Function = i + 1;
                            goto Stop_Card;
                        case "On Draw Damage":
                            On_Draw_Damage = Played_Card.Function_Values[i];
                            break;
                        case "Next Turn":
                            Function_To_Queue = new Queued_Function();
                            Function_To_Queue.Source_Card = Played_Card;
                            Function_To_Queue.Function_Start_Point = i + 1;
                            Queued_Functions.Add(Function_To_Queue);
                            goto End_Card;
                        case "Self Random Damage":
                            for (int j = 0; j < Played_Card.Function_Values[i]; j++)
                            {
                                Random_Target = Random.Range(0, GameObject.FindGameObjectsWithTag("Enemy").Length + 1);
                                if (Random_Target == GameObject.FindGameObjectsWithTag("Enemy").Length + 1)
                                {
                                    Player.GetComponent<Player_Manager>().Health_Change(1);
                                }
                                else
                                {
                                    GameObject.FindGameObjectsWithTag("Enemy")[Random_Target].GetComponent<Enemy_Manager>().Damage(1);
                                }
                            }
                            break;
                        case "Add Temp Max Energy":
                            Player.GetComponent<Player_Manager>().Temp_Starting_Energy_Change(Played_Card.Function_Values[i]);
                        break;
                        case "Add Max Energy":
                            Player.GetComponent<Player_Manager>().Starting_Energy_Change(Played_Card.Function_Values[i]);
                            break;
                        case "Add Armour":
                            Player.GetComponent<Player_Manager>().Add_Armour(Played_Card.Function_Values[i]);
                            break;
                        case "HOT":
                            Player.GetComponent<Player_Manager>().Set_HOT(Played_Card.Function_Values[i]);
                            break;
                        case "On Draw Heal":
                            On_Draw_Healing = Played_Card.Function_Values[i];
                            break;
                        case "Life Steal":
                            Player.GetComponent<Player_Manager>().Set_Life_Steal(Played_Card.Function_Values[i]);
                            break;
                        case "Reduce Damage":
                            Player.GetComponent<Player_Manager>().Set_Damage_Reduction(Played_Card.Function_Values[i]);
                            break;
                        case "Double Armour":
                            Player.GetComponent<Player_Manager>().Double_Armour();
                            break;
                        case "Deflect":
                            Player.GetComponent<Player_Manager>().Set_Deflection(Played_Card.Function_Values[i]);
                            break;
                        case "For Cards in Pile":
                            if (Loops == 0)
                            {
                                switch (Played_Card.Function_Values[i])
                                {
                                    case 0:
                                        Loops = Mathf.FloorToInt(Deck_Object.GetComponent<Deck_Manager>().Discard.Count / 3);
                                        break;
                                    case 1:
                                        Loops = Mathf.FloorToInt(Deck_Object.GetComponent<Deck_Manager>().Deck.Count / 3);
                                        break;
                                }
                            }
                            Loops -= 1;
                            Starting_Function = i;
                            break;
                        case "AOT":
                            Player.GetComponent<Player_Manager>().Set_AOT(Played_Card.Function_Values[i]);
                            break;
                        case null:
                            break;
                    }
                }
            End_Card:;
                if (Loops != 0)
                {
                    Play_Card(Played_Card);
                    goto Stop_Card;
                }
                Starting_Function = 0;        
                Hand.Remove(Played_Card);
                Enable_Cards();
                Deck_Object.GetComponent<Deck_Manager>().Discard.Add(Played_Card);
                Deck_Object.GetComponent<Deck_Manager>().Display_Discard_Count();
                Player.GetComponent<Player_Manager>().Energy_Change(Played_Card.Cost);
            Stop_Card:;
            }
        }
    }

    //lets the user choose what to target by left clicking on them
    IEnumerator Target_Select(string Target_Function, Card Played_Card, int Target_Function_Int)
    {
    Target_Select:;
        while (true)
        {
        Incorrect_Target:;
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit Hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit))
                {
                     if (Hit.transform.gameObject.tag == "Enemy")
                    {
                        Freeze_Player_Control = false;
                        switch (Target_Function)
                        {
                            case "Damage":
                                Hit.transform.gameObject.GetComponent<Enemy_Manager>().Damage(Played_Card.Function_Values[Target_Function_Int]);
                                Play_Card(Played_Card);
                            break;
                            case "Lethal Damage":
                                if (Hit.transform.gameObject.GetComponent<Enemy_Manager>().Lethal_Damage(Played_Card.Function_Values[Target_Function_Int]) == true)
                                {
                                    Play_Card(Played_Card);
                                }
                                else
                                {
                                    Starting_Function = 0;
                                    Hand.Remove(Played_Card);
                                    Enable_Cards();
                                    Deck_Object.GetComponent<Deck_Manager>().Discard.Add(Played_Card);
                                    Deck_Object.GetComponent<Deck_Manager>().Display_Discard_Count();
                                    Player.GetComponent<Player_Manager>().Energy_Change(Played_Card.Cost);
                                }
                            break;
                            case "DOT":
                                Hit.transform.gameObject.GetComponent<Enemy_Manager>().Set_DOT(Played_Card.Function_Values[Target_Function_Int]);
                                Play_Card(Played_Card);
                            break;
                            default:
                                goto Incorrect_Target;
                        }
                        yield break;
                    }
                     else if (Hit.transform.gameObject.tag == "Card")
                    {
                        switch (Target_Function)
                        {
                            case "Discard":
                                if (Hit.transform.gameObject.GetComponent<Card_Values>().Displayed_Card != Played_Card)
                                {
                                    Hand.Remove(Hit.transform.gameObject.GetComponent<Card_Values>().Displayed_Card);
                                    Deck_Object.GetComponent<Deck_Manager>().Discard.Add(Hit.transform.gameObject.GetComponent<Card_Values>().Displayed_Card);
                                    Deck_Object.GetComponent<Deck_Manager>().Display_Discard_Count();
                                    Hit.transform.gameObject.GetComponent<Card_Values>().Disable_Display();
                                    Target_Function_Int -= 1;
                                    if (Target_Function_Int > 0)
                                    {
                                        goto Target_Select;
                                    }
                                }
                            break;
                            default:
                                goto Incorrect_Target;
                        }
                    }
                }           
            }
            yield return null;
        }
    }

    //empties the player's current hand and draws 5 new cards
    public void Reset_Hand()
    {
       for (int i = 0; i < Hand.Count; i++)
        {
            Deck_Object.GetComponent<Deck_Manager>().Discard.Add(Hand[i]);
        }
        Deck_Object.GetComponent<Deck_Manager>().Display_Discard_Count();
        Hand.Clear();
        Draw_Card(5);
        Enable_Cards();
    }
}
