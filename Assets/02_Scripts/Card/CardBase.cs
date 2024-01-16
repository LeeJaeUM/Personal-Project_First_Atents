using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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

    PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.CardClick.performed += OnCardClick;
    }

    private void OnCardClick(InputAction.CallbackContext context)
    {
        Debug.Log("������ ���콺");
    }

    private void OnDisable()
    {
        inputAction.Player.CardClick.performed -= OnCardClick;
        inputAction.Player.Disable();
    }
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