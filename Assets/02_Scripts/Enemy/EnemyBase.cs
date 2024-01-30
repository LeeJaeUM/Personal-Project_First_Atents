using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Tile tileOn;
    TileManager tileManager;
    public int attackDamage = 1;
    public int enemyType = 0;
    public bool playerDir = false;  //false면 왼쪽 true면 오른쪽 공격

    public bool isAttackStandby = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))   //생성 및 이동으로 타일에 닿으면 자신의 위치를 지정
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

    //TileManager.Inst.TestTileAttack(tileOn, true=우측/false좌측, 데미지, 범위, 띄는칸이있는가?, 몇칸 띌건지);
    void Attack_Range1() // 한 칸 공격
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 1, false, 0);
    }
    void Attack_Range2() // 두 칸 공격
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 2, false, 0);
    }
    void Attack_Over1_Range1() // 한 칸 띄고 한 칸 공격
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 1, true, 1);
    }
    void Attack_Over1_Range2() // 한 칸 띄고 두 칸 공격
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 2, true, 1);
    }



}
