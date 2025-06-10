using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardCollection", menuName = "Card Game/Card Collection")]
public class CardCollection : ScriptableObject
{
    public List<CardData> cards;
}
