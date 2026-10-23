using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This is for displaying card Data BARK!
[RequireComponent(typeof(Button))]
public class CardView : MonoBehaviour
{
    [SerializeField] Image artwork;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text descriptionText;

    Button button;

    public CardData Card { get; private set; }

    public void Setup(CardData card, Action<CardData> onClick)
    {
        Card = card;
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke(card));

        if (artwork != null) artwork.sprite = card.Artwork;
        if (nameText != null) nameText.text = card.CardName;
        if (costText != null) costText.text = card.EnergyCost.ToString();
        if (descriptionText != null) descriptionText.text = card.Description;
    }

    public void SetPlayable(bool playable)
    {
        if (button != null) button.interactable = playable;
    }
}
