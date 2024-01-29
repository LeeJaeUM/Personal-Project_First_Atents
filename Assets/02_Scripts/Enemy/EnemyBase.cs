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
            Attack_1Range();
        if (Input.GetKeyDown(KeyCode.S))
            Attack_2Range();
    }

    //TileManager.Inst.TestTileAttack(tileOn, true=����/false����, ������, ����, ���ĭ���ִ°�?, ��ĭ �����);
    void Attack_1Range() // �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 1, false, 0);
    }
    void Attack_2Range() // �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 2, false, 0);
    }
    void Attack_1Over_1Range() // �� ĭ ��� �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 1, true, 1);
    }
    void Attack_1Over_2Range() // �� ĭ ��� �� ĭ ����
    {
        tileManager.TestTileAttack(tileOn, playerDir, attackDamage, 2, true, 1);
    }



}
