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

    public void TestTileAttack(Tile myTile, bool right, int damage, int range, bool overTile, int overRange)
    {
       int temp = 0;
       int atkDir = 1;
       if (overTile)
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

    #region �ϴ� �׽�Ʈ
    public void MovePlayer_Right()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
           if(tiles[i].tileOnObjType == 1)
            {
                if(i + 1 < tiles.Length && (tiles[i + 1].tileOnObjType == 0))//�ε����� ���� �� ����ó�� �߰�����
                {
                    
                    tiles[i].MoveObj_t(tiles[i + 1].transform);
                }
                else
                {
                    Debug.Log("���� �ְų� �迭 ������ ����");
                }
           }
        }
    }

    public void MovePlayer_Left()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == playerOnTile)
            {
                if (i - 1 >= 0 && tiles[i - 1].tileOnObjType == 0)//�ε����� ���� �� ����ó�� �߰�����
                {

                    tiles[i].MoveObj_t(tiles[i - 1].transform);
                }
                else
                {
                    Debug.Log("���� �ְų� �迭 ������ ����");
                }
            }
        }
    }

    public void PlayerAttackRight_One(int range)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileOnObjType == 1)
            {
               // if (tiles[i + 1].tileOnObjType == 2)    
               // {
                    tiles[i+range].TakeDamage_t(1);
                //}
            }
        }
    }

    public void PlayerAttackLeft_One(int range)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileOnObjType == 1)
            {
                // if (tiles[i + 1].tileOnObjType == 2)    
                // {
                tiles[i - range].TakeDamage_t(1);
                //}
            }
        }
    }
    #endregion

    void CheckObj()
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            switch(tiles[i].tileOnObjType)
            {
                case 1: 
                    //�÷��̾��϶�
                    break;
                case 2:
                    //���϶�
                    break;
                case 3:
                    //Enviroment�϶�
                    break;
            }
        }
    }

}
