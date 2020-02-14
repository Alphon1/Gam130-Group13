using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Art;
    public int Cost;
    public string Function1;
    public int Function1_value;
    public string Function2;
    public int Function2_value;
    public string Function3;
    public int Function3_value;
    public string Function4;
    public int Function4_value;
    public string Function5;
    public int Function5_value;
}
