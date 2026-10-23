using System.Collections.Generic;
using UnityEngine;

// Tweak balance (HP, energy, hand size, starting deck) without touching code.
[CreateAssetMenu(menuName = "Cards/Game Config", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField, Min(1)] int playerMaxHealth = 40;
    [SerializeField, Min(1)] int energyPerTurn = 3;
    [SerializeField, Min(1)] int cardsPerTurn = 5;
    [SerializeField] List<CardData> startingDeck = new List<CardData>();

    public int PlayerMaxHealth => playerMaxHealth;
    public int EnergyPerTurn => energyPerTurn;
    public int CardsPerTurn => cardsPerTurn;
    public IReadOnlyList<CardData> StartingDeck => startingDeck;
}
