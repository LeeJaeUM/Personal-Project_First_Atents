using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("���� �� ��带 ���մϴ�.")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("ī�� ����� �ſ� �������ϴ�.")] bool fastMode;
    [SerializeField] [Tooltip("���� ī�� ������ ���մϴ�.")] int startCardCount;

    [Header("Properties")]
    public bool isLoading; // ���� ������ isLoading�� ture�� �ϸ� ī��� ��ƼƼ Ŭ�� ����
    public bool myTurn;

    enum ETurnMode { Random, My, Other}
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;   //Cardmanager���� ����
    public static Action OnTurnEnd;   //CostManager, TurnText���� ����

    void GameSetup()
    {
        if (fastMode)
            delay05 = new WaitForSeconds(0.05f);

        switch (eTurnMode)
        {
            case ETurnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;
            case ETurnMode.My:
                myTurn = true;
                break;
            case ETurnMode.Other:
                myTurn = false;
                break;
        }
    }

    public IEnumerator StartGameCo()    //���� ���۽� ������ �ڷ�ƾ
    {
        GameSetup();
        isLoading = true;

        for(int i=0; i< startCardCount; i++)
        {
            yield return delay05;
           // OnAddCard?.Invoke(false);
            yield return delay05;
            OnAddCard?.Invoke(true);
        }
        StartCoroutine(StartTurnCo()); // �ٷ� �� ����
    }
    IEnumerator StartTurnCo()   //�� ���� �� ������ �ڷ�ƾ
    {
        isLoading = true;
        if (myTurn) { GameManager.Inst.Notification("���� ���ϱ�?"); }
            
        yield return delay07;
        OnAddCard?.Invoke(myTurn);
        yield return delay07;
        isLoading = false;
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        OnTurnEnd?.Invoke();
        StartCoroutine(StartTurnCo());
    }
}
