using System.Collections.Generic;
using UnityEngine;


public class HandView : MonoBehaviour
{
    [SerializeField] CardView cardPrefab;
    [SerializeField] Transform container;                      // Horizontal layout group
    [SerializeField] CardDataEventChannel cardPlayRequested;   // out
    [SerializeField] IntEventChannel energyChanged;            // in

    readonly List<CardView> views = new List<CardView>();
    Deck deck;
    int energy;

    void OnEnable() => energyChanged.Raised += OnEnergyChanged;
    void OnDisable() => energyChanged.Raised -= OnEnergyChanged;

    void OnDestroy()
    {
        if (deck != null) deck.Changed -= Rebuild;
    }

    public void Bind(Deck newDeck)
    {
        if (deck != null) deck.Changed -= Rebuild;
        deck = newDeck;
        deck.Changed += Rebuild;
        Rebuild();
    }

    void Rebuild()
    {
        foreach (var view in views)
        {
            view.gameObject.SetActive(false);   // hidden for now, Destroyed at end of frame
            Destroy(view.gameObject);
        }
        views.Clear();

        foreach (var card in deck.Hand)
        {
            CardView view = Instantiate(cardPrefab, container);
            view.Setup(card, OnCardClicked);
            views.Add(view);
        }
        RefreshPlayable();
    }

    void OnCardClicked(CardData card) => cardPlayRequested.Raise(card);

    void OnEnergyChanged(int value)
    {
        energy = value;
        RefreshPlayable();
    }

    void RefreshPlayable()
    {
        foreach (var view in views)
            view.SetPlayable(view.Card.EnergyCost <= energy);
    }
}
