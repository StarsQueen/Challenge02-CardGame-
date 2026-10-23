using System;
using UnityEngine;

// Plain C# class (no MonoBehaviour): runtime health/block state lives here,
// NOT in a ScriptableObject, so the assets are never modified during play.
public class Combatant : ICombatant
{
    public string DisplayName { get; }
    public int MaxHealth { get; }
    public int CurrentHealth { get; private set; }
    public int Block { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    public event Action Changed;
    public event Action Died;

    public Combatant(string displayName, int maxHealth)
    {
        DisplayName = displayName;
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || amount <= 0) return;

        int absorbed = Mathf.Min(Block, amount);
        Block -= absorbed;
        CurrentHealth = Mathf.Max(0, CurrentHealth - (amount - absorbed));

        Changed?.Invoke();
        if (IsDead) Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        Changed?.Invoke();
    }

    public void GainBlock(int amount)
    {
        if (amount <= 0) return;
        Block += amount;
        Changed?.Invoke();
    }

    public void ResetBlock()
    {
        if (Block == 0) return;
        Block = 0;
        Changed?.Invoke();
    }
}
