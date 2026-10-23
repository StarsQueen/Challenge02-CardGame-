using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Effects/Block", fileName = "BlockEffect")]
public class BlockEffect : CardEffect
{
    [SerializeField, Min(1)] int amount = 5;

    public override void Apply(CombatContext context) => context.Source.GainBlock(amount);
    public override string Describe() => $"Gain {amount} block.";
}
