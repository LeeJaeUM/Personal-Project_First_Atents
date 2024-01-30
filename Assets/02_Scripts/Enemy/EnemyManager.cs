using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    TurnManager turnManager;
    public bool changeTurn = false;
    void Start()
    {
        turnManager = TurnManager.Inst.GetComponent<TurnManager>();
        TurnManager.OnTurnEnd += ChangeTurn;
        ChangeTurn();
    }


    void ChangeTurn()
    {
        StartCoroutine(EnemyPatternCo());
    }

    /// <summary>
    ///  내 턴일때 행동할 것을 정하고 적의 턴일 때 행동을 시작한다
    /// </summary>
    IEnumerator EnemyPatternCo()
    {
        if (turnManager.myTurn)
        {
            // EnemyBase 스크립트를 가진 모든 오브젝트를 찾기
            EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].isAttackStandby = true;
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
            EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
            // 순서에 따라 각각의 EnemyBase에 대해 공격 함수 실행
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].EnemyAttack(enemies[i].enemyType);
                enemies[i].isAttackStandby = false;
                yield return new WaitForSeconds(1.2f);
            }
        }
        yield return null;
    }
}
