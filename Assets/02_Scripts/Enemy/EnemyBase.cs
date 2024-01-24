using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Tile tileOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))   //���� �� �̵����� Ÿ�Ͽ� ������ �ڽ��� ��ġ�� ����
        {
            tileOn = collision.GetComponent<Tile>();
        }
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
        TileManager.Inst.TestTileAttack(tileOn, false, 1, 1, false, 0);
    }
    void Attack_2Range() // �� ĭ ����
    {
        TileManager.Inst.TestTileAttack(tileOn, false, 2, 2, false, 0);
    }
    void Attack_1Over_1Range() // �� ĭ ��� �� ĭ ����
    {
        TileManager.Inst.TestTileAttack(tileOn, false, 1, 1, true, 1);
    }
    void Attack_1Over_2Range() // �� ĭ ��� �� ĭ ����
    {
        TileManager.Inst.TestTileAttack(tileOn, false, 2, 2, true, 1);
    }



}
