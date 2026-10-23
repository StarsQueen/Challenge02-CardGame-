using System.Collections.Generic;
using System.Text;
using UnityEngine;

// A card is pure data: name, art, cost and a list of effects.
// It never changes at runtime, so one asset can be shared by every copy of the card.
[CreateAssetMenu(menuName = "Cards/Card", fileName = "NewCard")]
public class CardData : ScriptableObject
{
    [SerializeField] string cardName;
    [SerializeField] Sprite artwork;
    [SerializeField, Min(0)] int energyCost = 1;
    [SerializeField] List<CardEffect> effects = new List<CardEffect>();

    public string CardName => string.IsNullOrEmpty(cardName) ? name : cardName;
    public Sprite Artwork => artwork;
    public int EnergyCost => energyCost;

    // The description is built from the effects, so text and behaviour can never disagree.
    public string Description
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var effect in effects)
                if (effect != null) sb.AppendLine(effect.Describe());
            return sb.ToString().TrimEnd();
        }
    }

    public void Play(CombatContext context)
    {
        foreach (var effect in effects)
            if (effect != null) effect.Apply(context);
    }
}
