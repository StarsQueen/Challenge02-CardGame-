using UnityEngine;

// Base class for everything a card can do.
// Every effect is its own asset, so a new card = mix existing effects in the Inspector, no new code.
public abstract class CardEffect : ScriptableObject
{
    public abstract void Apply(CombatContext context);
    public abstract string Describe();
}
