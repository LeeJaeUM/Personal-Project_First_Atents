using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public DeckBuilder deckBuilder;
    //public Button selectCardButton;
    public TMP_Text buttonText; // TMPro�� Text�� ���

    private void Start()
    {
       // selectCardButton.onClick.AddListener(SelectRandomCard);
    }

    //void SelectRandomCard()
    //{
    //    if (deckBuilder.playerDeck.Count > 0)
    //    {
    //        int randomIndex = Random.Range(0, deckBuilder.playerDeck.Count);
    //        deckBuilder.OnCardClick(deckBuilder.playerDeck[randomIndex]);
    //    }
    //}    

    void SelectCard()
    {

    }
}
