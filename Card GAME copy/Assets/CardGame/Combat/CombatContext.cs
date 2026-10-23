// Everything an effect needs to know when a card is played.
// Passing one object means adding new info later does not change every effect's signature.
public class CombatContext
{
    public ICombatant Source { get; }
    public ICombatant Target { get; }
    public Deck SourceDeck { get; }   // null when the enemy plays a card

    public CombatContext(ICombatant source, ICombatant target, Deck sourceDeck)
    {
        Source = source;
        Target = target;
        SourceDeck = sourceDeck;
    }
}
