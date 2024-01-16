using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckBuilder : MonoBehaviour
{
    public List<CardBase> availableCards;
    public List<CardBase> playerDeck;

    public TMP_Text deckSizeText; // TMPro�� Text�� ���

    private void Start()
    {
        BuildInitialDeck();
        UpdateDeckSizeText();
    }

    void BuildInitialDeck()
    {
        for (int i = 0; i < 5; i++)
        {
            CardBase selectedCard = availableCards[Random.Range(0, availableCards.Count)];
            CardBase newCard = Instantiate(selectedCard, transform);
            playerDeck.Add(newCard);
        }
    }

    void UpdateDeckSizeText()
    {
        deckSizeText.text = $"�� ũ��: {playerDeck.Count}";
    }

    public void OnCardClick(CardBase clickedCard)
    {
        if (playerDeck.Contains(clickedCard))
        {
            clickedCard.ApplyCardEffect();
            playerDeck.Remove(clickedCard);
            UpdateDeckSizeText();
        }
    }
}