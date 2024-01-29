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
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다.")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("카드 배분이 매우 빨라집니다.")] bool fastMode;
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다.")] int startCardCount;

    [Header("Properties")]
    public bool isLoading; // 게임 끝나면 isLoading을 ture로 하면 카드와 엔티티 클릭 방지
    public bool myTurn;

    enum ETurnMode { Random, My, Other}
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;   //Cardmanager에서 반응
    public static Action<int> OnTurnEnd;   //CostManager, TurnText에서 반응

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

    public IEnumerator StartGameCo()    //게임 시작시 실행할 코루틴
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
        StartCoroutine(StartTurnCo()); // 바로 턴 시작
    }
    IEnumerator StartTurnCo()   //턴 시작 시 실행할 코루틴
    {
        isLoading = true;
        if (myTurn) { GameManager.Inst.Notification("나의 턴일까?"); }
            
        yield return delay07;
        OnAddCard?.Invoke(myTurn);
        yield return delay07;
        isLoading = false;
    }

    public void EndTurn()
    {
        myTurn = !myTurn;
        OnTurnEnd?.Invoke(1);
        StartCoroutine(StartTurnCo());
    }
}
