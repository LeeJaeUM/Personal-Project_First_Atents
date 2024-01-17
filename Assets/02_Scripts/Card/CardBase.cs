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
    public int cardID; // ī���� ���� �ĺ���ȣ
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
        // ī���� Ư���� ȿ���� ����
        Debug.Log($"ī�� ID: {cardID}, �Ӽ�: {attribute}, ���ݷ�: {attack}, �ڽ�Ʈ: {cost}");
    }


}