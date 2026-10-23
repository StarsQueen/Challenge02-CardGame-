using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/Damage", fileName = "DamageEffect")]
public class DamageEffect : CardEffect
{
    [SerializeField, Min(1)] int amount = 6;

    public override void Apply(CombatContext context) => context.Target.TakeDamage(amount);
    public override string Describe() => $"Deal {amount} damage.";
}
