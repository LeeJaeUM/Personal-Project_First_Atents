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

    void TestTileAttack(Tile myTile, bool left, int range, bool overTile, int overRange)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == myTile)
            {
                if (left)
                {
                    range = -range;
                }
                if (tiles[i +1].tileOnObjType == 0)// ��ǥ��ġ�� ��� obj�� �����Ѵٸ�
                {
                    tiles[i].MoveObj_t(tiles[i - 1].transform);
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
                if(tiles[i+1].tileOnObjType == 0)//�ε����� ���� �� ����ó�� �߰�����
                {
                    
                    tiles[i].MoveObj_t(tiles[i + 1].transform);
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
                if (tiles[i - 1].tileOnObjType == 0)//�ε����� ���� �� ����ó�� �߰�����
                {

                    tiles[i].MoveObj_t(tiles[i - 1].transform);
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
