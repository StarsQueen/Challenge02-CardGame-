using System;
using System.Collections.Generic;

// Draw pile / hand / discard pile logic. Plain C#, no scene objects needed,
// so it is easy to test and completely separate from how cards are displayed.
public class Deck
{
    readonly List<CardData> drawPile = new List<CardData>();
    readonly List<CardData> discardPile = new List<CardData>();
    readonly List<CardData> hand = new List<CardData>();

    public IReadOnlyList<CardData> Hand => hand;
    public int DrawPileCount => drawPile.Count;
    public int DiscardPileCount => discardPile.Count;

    public event Action Changed;

    public Deck(IEnumerable<CardData> cards)
    {
        drawPile.AddRange(cards);
        Shuffle(drawPile);
    }

    public void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (drawPile.Count == 0)
            {
                if (discardPile.Count == 0) break;   // nothing left anywhere
                Reshuffle();
            }

            int last = drawPile.Count - 1;
            hand.Add(drawPile[last]);
            drawPile.RemoveAt(last);
        }
        Changed?.Invoke();
    }

    // Moves a card from hand to discard. Returns false if it was not in the hand.
    public bool Discard(CardData card)
    {
        if (!hand.Remove(card)) return false;
        discardPile.Add(card);
        Changed?.Invoke();
        return true;
    }

    public void DiscardHand()
    {
        discardPile.AddRange(hand);
        hand.Clear();
        Changed?.Invoke();
    }

    void Reshuffle()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        Shuffle(drawPile);
    }

    // Fisher-Yates shuffle
    static void Shuffle(List<CardData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
