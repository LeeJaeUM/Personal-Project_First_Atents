using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] Tile tileOn;
    TileManager tileManager;
    public int enemyType = 1;
    public int attackDamage = 1;
    public int attackRange = 1;

    public Transform playerTransform;
    public bool playerDir = false;  //false면 왼쪽 true면 오른쪽 공격
    //public bool playerOnRange = true;
    public bool isActionStandby = false;

    public bool CheckFind = false;

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
        // 플레이어를 찾아서 playerTransform 변수에 할당
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다. 이름이나 다른 식별자를 확인하세요.");
        }
    }
    void Update()
    {
#if UNITY_EDITOR
        InpuCheckKey();
#endif
        //플레이어의 위치를 찾아 공격 및 이동의 방향을 정함
        if(playerTransform != null)
        {
            playerDir = playerTransform.position.x > transform.position.x;
        }
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
        CheckFind = tileManager.EnemyCheckPlayerPos(tileOn, playerDir, attackRange);
        if (tileManager.EnemyCheckPlayerPos(tileOn, playerDir, attackRange))
        {
            EnemyAttack(enemyType);
        }
        else
        {
            tileManager.EnemyTileMove(tileOn, playerDir, 1);
        }
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

    //TileManager.Inst.TestTileAttack(tileOn, true=우측/false좌측, 데미지, 범위, 띄는칸이있는가?, 몇칸 띌건지);
    void Attack_Range1() // 한 칸 공격
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 1, false, 0);
    }
    void Attack_Range2() // 두 칸 공격
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 2, false, 0);
    }
    void Attack_Over1_Range1() // 한 칸 띄고 한 칸 공격
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 1, true, 1);
    }
    void Attack_Over1_Range2() // 한 칸 띄고 두 칸 공격
    {
        tileManager.EnemyTileAttack(tileOn, playerDir, attackDamage, 2, true, 1);
    }

    void Move_Range1()
    {
        tileManager.EnemyTileMove(tileOn, playerDir, 1);
    }
    void Move_Range2()
    {
        tileManager.EnemyTileMove(tileOn, playerDir, 2);
    }
}
