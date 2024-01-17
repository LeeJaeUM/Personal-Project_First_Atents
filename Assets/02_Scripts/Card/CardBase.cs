using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CardAttribute
{
    Physical,
    Fire,
    Ice,
    Lightning,
    Cosmic
}


public class CardBase : MonoBehaviour
{
    public int cardID; // 카드의 고유 식별번호
    public int attack;
    public CardAttribute attribute;
    public int cost;

    public void InitializeCard(int uniqueID, int attackValue, CardAttribute cardAttribute, int cardCost)
    {
        cardID = uniqueID;
        attack = attackValue;
        attribute = cardAttribute;
        cost = cardCost;
    }

    public void ApplyCardEffect()
    {
        // 카드의 특별한 효과를 구현
        Debug.Log($"카드 ID: {cardID}, 속성: {attribute}, 공격력: {attack}, 코스트: {cost}");
    }


}