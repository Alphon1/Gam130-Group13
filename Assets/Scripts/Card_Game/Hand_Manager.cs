using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Manager : Deck_Manager
{
    [SerializeField]
    protected List<Card> Hand;
    private GameObject Target;
    private GameObject Player;
    private GameObject Turn_Order;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Turn_Order = GameObject.FindWithTag("Battle_Manager");
    }

    public void Draw_Card(int Cards_Drawn)
    {
        for (int i = 0; i < Cards_Drawn; i++)
        {
            if (Hand.Count < 11)
            {
                Hand.Add(Deck[0]);
                Deck.RemoveAt(0);
                Display_Deck_Count();
                for (int j = 0; j < GameObject.FindGameObjectsWithTag("Card").Length; j++)
                {
                    GameObject.FindGameObjectsWithTag("Card")[j].GetComponent<Card_Values>().Update_Display();
                }
            }
            if (Deck.Count == 0)
            {
                Deck_Out();
            }
        }      
    }
    public void Play_Card(Card Played_Card)
    {
        if (Turn_Order.GetComponent<Battle_Manager>().Player_Control() == true)
        {
            if (Player.GetComponent<Player_Manager>().Can_Play(Played_Card.Cost))
            {
                for (int i = 0; i < Played_Card.Functions.Count; i++)
                {
                    switch (Played_Card.Functions[i])
                    {
                        case "Draw":
                            Draw_Card(Played_Card.Function_Values[i]);
                            break;
                        case "Damage":
                            Target_Select();
                            if (Target.tag == "Enemy")
                            {
                                Target.GetComponent<Enemy_Manager>().Damage(Played_Card.Function_Values[i]);
                                break;
                            }
                            else
                            {
                                goto Not_Played;
                            }
                        case "Heal":
                            Player.GetComponent<Player_Manager>().Health_Change(-(Played_Card.Function_Values[i]));
                            break;
                        case "Add Energy":
                            Player.GetComponent<Player_Manager>().Energy_Change(-(Played_Card.Function_Values[i]));
                            break;
                        case "Lethal Damage":
                            Target_Select();
                            if (Target.tag == "Enemy")
                            {
                                if (Target.GetComponent<Enemy_Manager>().Lethal_Damage(Played_Card.Function_Values[i]) == false)
                                {
                                    goto End_Card;
                                }
                                break;
                            }
                            else
                            {
                                goto Not_Played;
                            }
                        case "Shuffle Discard":
                            for (int j = 0; j < Discard.Count; j++)
                            {
                                Deck.Add(Discard[j]);
                                Display_Deck_Count();
                                Discard.RemoveAt(j);
                                Display_Discard_Count();
                            }
                            Shuffle_Deck();
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
                        case null:
                            break;
                    }
                }
                End_Card:;
                Hand.Remove(Played_Card);
                for (int j = 0; j < GameObject.FindGameObjectsWithTag("Card").Length; j++)
                {
                    GameObject.FindGameObjectsWithTag("Card")[j].GetComponent<Card_Values>().Update_Display();
                }
                Discard.Add(Played_Card);
                Player.GetComponent<Player_Manager>().Energy_Change(Played_Card.Cost);
                Not_Played:;
            }
        }
    }

    IEnumerator Target_Select()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit Hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out Hit))
                {
                    Target = Hit.transform.gameObject;
                }
                yield break;
            }
            yield return null;
        }
    }


    public void Reset_Hand()
    {
       for (int i = 0; i < Hand.Count; i++)
        {
            Discard.Add(Hand[i]);
            Display_Discard_Count();
            Hand.RemoveAt(i);
            for (int j = 0; j < GameObject.FindGameObjectsWithTag("Card").Length; j++)
            {
                GameObject.FindGameObjectsWithTag("Card")[j].GetComponent<Card_Values>().Update_Display();
            }
        }
        Draw_Card(5);
    }
}
