using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/Heal", fileName = "HealEffect")]
public class HealEffect : CardEffect
{
    [SerializeField, Min(1)] int amount = 4;

    public override void Apply(CombatContext context) => context.Source.Heal(amount);
    public override string Describe() => $"Heal {amount} HP.";
}
