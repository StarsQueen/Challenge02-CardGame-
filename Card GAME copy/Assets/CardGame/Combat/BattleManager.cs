using System.Collections;
using UnityEngine;

// The "conductor": owns the rules of a battle (turns, energy, playing cards).
// It is also the composition root: it creates the runtime objects and hands them to the views once.
// After that, everything talks through events.
public class BattleManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameConfig config;
    [SerializeField] EnemyData enemyData;

    [Header("Event channels")]
    [SerializeField] TurnStateEventChannel turnChanged;       // out
    [SerializeField] IntEventChannel energyChanged;           // out
    [SerializeField] BoolEventChannel battleEnded;            // out
    [SerializeField] CardDataEventChannel cardPlayRequested;  // in

    [Header("Views to bind")]
    [SerializeField] CombatantView playerView;
    [SerializeField] CombatantView enemyView;
    [SerializeField] HandView handView;

    [Header("Pacing")]
    [SerializeField] float enemyTurnDelay = 1f;

    Combatant player;
    Combatant enemy;
    Deck deck;
    int energy;
    TurnState state;

    bool IsOver => state == TurnState.Victory || state == TurnState.Defeat;

    void OnEnable() => cardPlayRequested.Raised += HandleCardPlayRequested;
    void OnDisable() => cardPlayRequested.Raised -= HandleCardPlayRequested;

    void Start()
    {
        player = new Combatant("You", config.PlayerMaxHealth);
        enemy = new Combatant(enemyData.EnemyName, enemyData.MaxHealth);
        deck = new Deck(config.StartingDeck);

        player.Died += () => EndBattle(false);
        enemy.Died += () => EndBattle(true);

        playerView.Bind(player);
        enemyView.Bind(enemy, enemyData.Portrait);
        handView.Bind(deck);

        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        SetState(TurnState.PlayerTurn);
        player.ResetBlock();
        SetEnergy(config.EnergyPerTurn);   // before drawing, so the hand knows what is affordable
        deck.Draw(config.CardsPerTurn);
    }

    // Called by the End Turn button (UnityEvent in the Inspector).
    public void EndPlayerTurn()
    {
        if (state != TurnState.PlayerTurn) return;
        deck.DiscardHand();
        StartCoroutine(EnemyTurnRoutine());
    }

    void HandleCardPlayRequested(CardData card)
    {
        if (state != TurnState.PlayerTurn) return;
        if (card.EnergyCost > energy) return;
        if (!deck.Discard(card)) return;   // card must really be in the hand

        SetEnergy(energy - card.EnergyCost);
        card.Play(new CombatContext(player, enemy, deck));
    }

    IEnumerator EnemyTurnRoutine()
    {
        SetState(TurnState.EnemyTurn);
        enemy.ResetBlock();
        yield return new WaitForSeconds(enemyTurnDelay);
        if (IsOver) yield break;

        CardData move = enemyData.PickMove();
        if (move != null) move.Play(new CombatContext(enemy, player, null));

        yield return new WaitForSeconds(enemyTurnDelay);
        if (!IsOver) StartPlayerTurn();
    }

    void EndBattle(bool playerWon)
    {
        if (IsOver) return;
        SetState(playerWon ? TurnState.Victory : TurnState.Defeat);
        battleEnded.Raise(playerWon);
    }

    void SetState(TurnState newState)
    {
        state = newState;
        turnChanged.Raise(newState);
    }

    void SetEnergy(int value)
    {
        energy = value;
        energyChanged.Raise(value);
    }
}
