using System;

// Anything that can fight (player, enemy, a future boss...).
// Effects and views only know this interface, never the concrete class.
public interface ICombatant
{
    string DisplayName { get; }
    int CurrentHealth { get; }
    int MaxHealth { get; }
    int Block { get; }
    bool IsDead { get; }

    event Action Changed;

    void TakeDamage(int amount);
    void Heal(int amount);
    void GainBlock(int amount);
}
