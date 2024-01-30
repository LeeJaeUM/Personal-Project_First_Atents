using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] Tile[] tiles;
    [SerializeField] Transform[] tilesTransforms;

    public Tile playerOnTile;
    CardManager cardManager;

    private int right = 1;
    private int left = -1;

    private void Start()
    {
        tilesTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            tilesTransforms[i] = child.GetComponent<Transform>();
        }
        tiles = GetComponentsInChildren<Tile>();
        // �� Tile�� ���� �̺�Ʈ ������ ���
        foreach (var tile in tiles)
        {
            tile.OnTileOnObject += ObjectOnTile;
        }
        cardManager = CardManager.Inst.GetComponent<CardManager>();
        cardManager.OnCardStandBy += PlayerCardStandby;

    }
    private void ObjectOnTile(int objType, Tile tile)
    {
        playerOnTile = null;
        // tile ������ ����Ͽ� � Tile���� �̺�Ʈ�� �߻��ߴ��� Ȯ��
        Debug.Log($"������Ʈ Ÿ�� {objType} on Tile: {tile.gameObject.name}");
        if(objType == 1 )
        {
            playerOnTile = tile;
        }
    }

    #region �ϴ� �׽�Ʈ
    private void PlayerCardStandby(int itemCode)
    {
        switch(itemCode)
        {
            case 0:
                PlayerMove(right);
                break;
            case 1:
                break;
            case 2:
                PlayerMove(left);
                break;   
            case 3: break;
            case 4:
                PlayerAttack_One(1, right);
                break;
            case 5: break;
            case 6:
                PlayerAttack_One(1, left);
                break;
            case 7: break; 
            case 8: break;
            case 9: break;
            case 10: break;
            default:
                Debug.Log("���� ���� ī�� �ڵ��Դϴ�");
                break;
        }
    }

    public void PlayerMove(int attackDirection)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileOnObjType == 1)
            {
                if (i + 1 < tiles.Length && i - 1 >= 0 && (tiles[i + attackDirection].tileOnObjType == 0))
                {

                    tiles[i].MoveObj_t(tiles[i + attackDirection].transform);
                }
                else
                {
                    Debug.Log("���� �ְų� �迭 ������ ����");
                }
            }
        }
    }

    public void PlayerAttack_One(int range, int attackDirection)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileOnObjType == 1)
            {
                if(i + 1 < tiles.Length && i - 1 >= 0)
                    tiles[i + range * attackDirection].TakeDamage_t(1);
            }
        }
    }

    #endregion
    /// <summary>
    /// Enemy�� ����ϴ� ���� �Լ�_TileManager
    /// </summary>
    /// <param name="myTile">���� ��ġ�� Ÿ��</param>
    /// <param name="right">true=����/false����</param>
    /// <param name="damage">������</param>
    /// <param name="range">����</param>
    /// <param name="isOverTile">���ĭ���ִ°�?</param>
    /// <param name="overRange">��ĭ �����</param>
    public void EnemyTileAttack(Tile myTile, bool right, int damage, int range, bool isOverTile, int overRange)
    {
       int temp = 0;
       int atkDir = 1;
       if (isOverTile)
       {
           temp += overRange; //�ǳʶ� �Ÿ�
       }
       if (!right) //fasle��� ���� ����
       {
           atkDir = -1;
       }
       for (int i = 0; i < tiles.Length; i++)
       {
           if (tiles[i] == myTile) // �迭���� ȣ���� Ÿ���� ã��
           {
                switch (range)
                {
                    case 1:
                        tiles[i + (range*atkDir) + (temp*atkDir)].TakeDamage_t(damage);
                        Debug.Log($"���� {range}�� ���� - �پ���� �Ÿ� = {temp}");
                        break;
                    default:
                        for(int j = 1; j < range+1; j++)
                        {
                            tiles[i + (j * atkDir) + (temp * atkDir)].TakeDamage_t(damage);

                            Debug.Log($"���� {range}�� ���� - �پ���� �Ÿ� = {temp}");
                            Debug.Log($"------{j}��° ���� ");
                        }
                        break;
                }
           }
       }
    }
    public void EnemyTileMove(Tile myTile, bool right, int range)
    {
        int moveDir = 1;
        if (!right) //fasle��� ���� �̵�
        {
            moveDir = -1;
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == myTile) // �迭���� ȣ���� Ÿ���� ã��
            {
                if (i + 1 < tiles.Length && i - 1 >= 0 && (tiles[i + (range * moveDir)].tileOnObjType == 0))
                {

                    tiles[i].MoveObj_t(tiles[i + (range * moveDir)].transform);
                }
                else
                {
                    Debug.Log("�÷��̾ �ְų� �迭 ������ ����");
                }
            }
        }
    }

    public bool EnemyCheckPlayerPos(Tile myTile, bool playerDir, int attackRange)
    {
        int temp = attackRange;
        if (!playerDir)
        {
            temp *= -1;
        }
        bool isPlayerOnRange = true;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == myTile) // �迭���� ȣ���� Ÿ���� ã��
            {
                if (tiles[i+(1*temp)].tileOnObjType == 1)
                {
                    isPlayerOnRange = true;
                }
                else
                {
                    isPlayerOnRange = false;
                }
            }
        }
        return isPlayerOnRange;
    }
}
