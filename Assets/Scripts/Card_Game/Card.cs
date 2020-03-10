using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string Type;
    public string Description;
    public Sprite Art;
    public int Cost;
    public List<string> Functions;
    public List<int> Function_Values;
}
