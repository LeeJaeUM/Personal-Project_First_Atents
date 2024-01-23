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
        // 각 Tile에 대해 이벤트 리스너 등록
        foreach (var tile in tiles)
        {
            tile.OnTileOnObject += ObjectOnTile;
        }
    }
    private void ObjectOnTile(int objType, Tile tile)
    {
        playerOnTile = null;
        // tile 정보를 사용하여 어떤 Tile에서 이벤트가 발생했는지 확인
        Debug.Log($"오브젝트 타입 {objType} on Tile: {tile.gameObject.name}");
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
                if (tiles[i +1].tileOnObjType == 0)// 목표위치에 어던 obj라도 존재한다면
                {
                    tiles[i].MoveObj_t(tiles[i - 1].transform);
                }
            }
        }
    }

    #region 일단 테스트
    public void MovePlayer_Right()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
           if(tiles[i].tileOnObjType == 1)
           {
                if(tiles[i+1].tileOnObjType == 0)//인덱스가 없을 때 예외처리 추가예정
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
                if (tiles[i - 1].tileOnObjType == 0)//인덱스가 없을 때 예외처리 추가예정
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
                    //플레이어일때
                    break;
                case 2:
                    //적일때
                    break;
                case 3:
                    //Enviroment일때
                    break;
            }
        }
    }

}
