using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    TurnManager turnManager;
    public bool changeTurn = false;
    [SerializeField] EnemyBase[] enemies;
    WaitForSeconds delay06 = new WaitForSeconds(0.6f);

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
    ///  �� ���϶� �ൿ�� ���� ���ϰ� ���� ���� �� �ൿ�� �����Ѵ�
    /// </summary>
    IEnumerator EnemyPatternCo()
    {
        if (turnManager.myTurn)
        {
            // EnemyBase ��ũ��Ʈ�� ���� ��� ������Ʈ�� ã��
            enemies = FindObjectsOfType<EnemyBase>();
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].isActionStandby = true;
                enemies[i].EnemyLookPlayer();
                switch (enemies[i].enemyType)
                {
                    case 0:
                        Debug.Log("EnemyŸ���� 0 �Դϴ�.");
                        break;
                    case 1:
                        Debug.Log("EnemyŸ���� 1 �Դϴ�.");
                        break;
                    case 2:
                        Debug.Log("EnemyŸ���� 2 �Դϴ�.");
                        break;
                    case 3:
                        Debug.Log("EnemyŸ���� 3 �Դϴ�.");
                        break;

                }
            }
        }
        else if (!turnManager.myTurn)
        {
            enemies = FindObjectsOfType<EnemyBase>();
            // ������ ���� ������ EnemyBase�� ���� �ൿ �Լ� ����
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
