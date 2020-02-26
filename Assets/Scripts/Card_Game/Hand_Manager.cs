using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Manager : MonoBehaviour
{
    [SerializeField]
    private List<Card> Hand;
    private GameObject Target;
    private GameObject Player;
    private GameObject Turn_Order;
    private bool Card_Enabled;
    private GameObject Deck_Object;
    

    public void Enable_Cards()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Card").Length; i++)
        {
             if ( Hand.Count > GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Card_Values>().Get_Card_Number())
            {
                GameObject.FindGameObjectsWithTag("Card")[i].SetActive(true);
                Card_Enabled = true;
            }
             else
            {
                GameObject.FindGameObjectsWithTag("Card")[i].SetActive(false);
                Card_Enabled = false;
            }
            GameObject.FindGameObjectsWithTag("Card")[i].GetComponent<Card_Values>().Update_Display(Card_Enabled, Hand);
        }
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Turn_Order = GameObject.FindWithTag("Battle_Manager");
        Deck_Object = GameObject.FindWithTag("Deck");
        Draw_Card(5);
    }

    public void Draw_Card(int Cards_Drawn)
    {
        for (int i = 0; i < Cards_Drawn; i++)
        {
            if (Hand.Count < 11)
            {
                Hand.Add(Deck_Object.GetComponent<Deck_Manager>().Deck[0]);
                Deck_Object.GetComponent<Deck_Manager>().Deck.RemoveAt(0);
                Deck_Object.GetComponent<Deck_Manager>().Display_Deck_Count();
                Enable_Cards();
            }
            if (Deck_Object.GetComponent<Deck_Manager>().Deck.Count == 0)
            {
                Deck_Object.GetComponent<Deck_Manager>().Deck_Out();
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
                        case null:
                            break;
                    }
                }
                End_Card:;
                Hand.Remove(Played_Card);
                Enable_Cards();
                Deck_Object.GetComponent<Deck_Manager>().Discard.Add(Played_Card);
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
            Deck_Object.GetComponent<Deck_Manager>().Discard.Add(Hand[i]);
            Deck_Object.GetComponent<Deck_Manager>().Display_Discard_Count();
            Hand.RemoveAt(i);
            Enable_Cards();
        }
        Draw_Card(5);
    }
}
