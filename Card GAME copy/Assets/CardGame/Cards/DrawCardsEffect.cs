using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/Draw Cards", fileName = "DrawCardsEffect")]
public class DrawCardsEffect : CardEffect
{
    [SerializeField, Min(1)] int count = 2;

    // SourceDeck is null for the enemy (it has no deck), so we use ?.
    public override void Apply(CombatContext context) => context.SourceDeck?.Draw(count);

    public override string Describe()
    {
        string noun = count == 1 ? "card" : "cards";
        return $"Draw {count} {noun}.";
    }
}
