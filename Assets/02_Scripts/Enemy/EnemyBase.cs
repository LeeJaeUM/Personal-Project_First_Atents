using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Tile tileOn;
    TileManager tileManager;
    public int attackDamage = 1;
    public int enemyType = 0;
    public bool playerDir = false;  //false�� ���� true�� ������ ����

    public bool isAttackStandby = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))   //���� �� �̵����� Ÿ�Ͽ� ������ �ڽ��� ��ġ�� ����
        {
            tileOn = collision.GetComponent<Tile>();
        }
    }
    private void Start()
    {
        tileManager = TileManager.Inst.GetComponent<TileManager>();
    }
    void Update()
    {
#if UNITY_EDITOR
        InpuCheckKey();
#endif
    }

    void InpuCheckKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Attack_Range1();
        if (Input.GetKeyDown(KeyCode.S))
            Attack_Range2();
    }

    public void EnemyAttack(int attackCode)
    {
        switch (attackCode)
        {
            case 0: break; 
            case 1: Attack_Range1(); break; 
            case 2: Attack_Range2(); break;
            case 3: Attack_Over1_Range1(); break;
        }
    }

    //TileManager.Inst.TestTileAttack(tileOn, true=����/false����, ������, ����, ���ĭ���ִ°�?, ��ĭ �����);
    void Attack_Range1() // �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 1, false, 0);
    }
    void Attack_Range2() // �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 2, false, 0);
    }
    void Attack_Over1_Range1() // �� ĭ ��� �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 1, true, 1);
    }
    void Attack_Over1_Range2() // �� ĭ ��� �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 2, true, 1);
    }



}
