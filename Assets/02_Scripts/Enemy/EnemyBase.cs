using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] Tile tileOn;
    TileManager tileManager;
    public int enemyType = 0;
    public int attackDamage = 1;
    public int attackRange = 1;

    public Transform playerTransform;
    public bool playerDir = false;  //false�� ���� true�� ������ ����
    public bool playerOnRange = true;
    public bool isActionStandby = false;

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
        // �÷��̾ ã�Ƽ� playerTransform ������ �Ҵ�
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("�÷��̾ ã�� �� �����ϴ�. �̸��̳� �ٸ� �ĺ��ڸ� Ȯ���ϼ���.");
        }
    }
    void Update()
    {
#if UNITY_EDITOR
        InpuCheckKey();
#endif
        //�÷��̾��� ��ġ�� ã�� ���� �� �̵��� ������ ����
        playerDir = playerTransform.position.x > transform.position.x;

    }

    void InpuCheckKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Attack_Range1();
        if (Input.GetKeyDown(KeyCode.S))
            Attack_Range2();
    }

    public void EnemyActions()
    {

    }

    void EnemyAttack(int attackCode)
    {
        switch (attackCode)
        {
            case 0: break; 
            case 1: Attack_Range1(); break; 
            case 2: Attack_Range2(); break;
            case 3: Attack_Over1_Range1(); break;
            case 4: Attack_Over1_Range2(); break;
        }
    }

    //TileManager.Inst.TestTileAttack(tileOn, true=����/false����, ������, ����, ���ĭ���ִ°�?, ��ĭ �����);
    void Attack_Range1() // �� ĭ ����
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 1, false, 0);
    }
    void Attack_Range2() // �� ĭ ����
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 2, false, 0);
    }
    void Attack_Over1_Range1() // �� ĭ ��� �� ĭ ����
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 1, true, 1);
    }
    void Attack_Over1_Range2() // �� ĭ ��� �� ĭ ����
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 2, true, 1);
    }



}
