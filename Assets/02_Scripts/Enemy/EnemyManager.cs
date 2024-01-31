using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Inst { get; private set; }
    void Awake() => Inst = this;
    TurnManager turnManager;
    public bool changeTurn = false;
    [SerializeField] EnemyBase[] enemies;
    WaitForSeconds delay06 = new WaitForSeconds(0.6f);

    public static Action OnEnemyAllDie; // 스테이지 클리어 확인용 액션 GameManager, PlayerWin 에서 사용
    private bool oneCheck_EnemyDie_Action = false;

    void Start()
    {
        turnManager = TurnManager.Inst.GetComponent<TurnManager>();
        TurnManager.OnTurnEnd += ChangeTurn;
        //ChangeTurn();

        enemies = FindObjectsOfType<EnemyBase>();
    }
    private void Update()
    {
        if(enemies.Length == 0)
        {
            Debug.Log("WWWWWWWAaaakak");
            if (!oneCheck_EnemyDie_Action)
            {
                OnEnemyAllDie?.Invoke();
                oneCheck_EnemyDie_Action = true;
            }
        }
    }

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 이벤트 핸들러를 제거
        TurnManager.OnTurnEnd -= ChangeTurn;
    }
    void ChangeTurn()
    {
        enemies = FindObjectsOfType<EnemyBase>();
        StartCoroutine(EnemyPatternCo());
    }

    /// <summary>
    ///  내 턴일때 행동할 것을 정하고 적의 턴일 때 행동을 시작한다
    /// </summary>
    IEnumerator EnemyPatternCo()
    {
        if (turnManager.myTurn)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].isActionStandby = true;
                enemies[i].EnemyLookPlayer();
                switch (enemies[i].enemyType)
                {
                    case 0:
                        Debug.Log("Enemy타입이 0 입니다.");
                        break;
                    case 1:
                        Debug.Log("Enemy타입이 1 입니다.");
                        break;
                    case 2:
                        Debug.Log("Enemy타입이 2 입니다.");
                        break;
                    case 3:
                        Debug.Log("Enemy타입이 3 입니다.");
                        break;

                }
            }
        }
        else if (!turnManager.myTurn)
        {
            // 순서에 따라 각각의 EnemyBase에 대해 행동 함수 실행
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].EnemyActions();
                enemies[i].isActionStandby = false;
                yield return delay06;
            }
        }
        yield return null;
    }
}
