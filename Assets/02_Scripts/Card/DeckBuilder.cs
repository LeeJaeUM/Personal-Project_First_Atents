using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckBuilder : MonoBehaviour
{
    public List<CardBase> availableCards;
    public List<CardBase> playerDeck;

    public TMP_Text deckSizeText; // TMPro의 Text를 사용

    private void Start()
    {
        // BuildInitialDeck();
        DeckSetting();
        UpdateDeckSizeText();
    }

    private void DeckSetting()
    {
        playerDeck = availableCards;
        for(int i=0; i< playerDeck.Count; i++)
        {
            CardBase selectedCard = playerDeck[i];
            CardBase newCard = Instantiate(selectedCard, transform);
        }
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
        deckSizeText.text = $"덱 크기: {playerDeck.Count}";
    }

    //public void OnCardClick(CardBase clickedCard)
    //{
    //    if (playerDeck.Contains(clickedCard))
    //    {
    //        clickedCard.ApplyCardEffect();
    //        playerDeck.Remove(clickedCard);
    //        UpdateDeckSizeText();
    //    }
    //}
    public int OnCardClick(CardBase clickedCard)
    {
        if (playerDeck.Contains(clickedCard))
        {
            clickedCard.ApplyCardEffect();
            playerDeck.Remove(clickedCard);
            UpdateDeckSizeText();
        }
        return clickedCard.cardID;
    }
}