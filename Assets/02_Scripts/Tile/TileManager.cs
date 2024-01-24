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

    public void TestTileAttack(Tile myTile, bool right, int damage, int range, bool overTile, int overRange)
    {
       int temp = 0;
       int atkDir = 1;
       if (overTile)
       {
           temp += overRange; //건너뛸 거리
       }
       if (!right) //fasle라면 왼쪽 공격
       {
           atkDir = -1;
       }
       for (int i = 0; i < tiles.Length; i++)
       {
           if (tiles[i] == myTile) // 배열에서 호출한 타일을 찾기
           {
                switch (range)
                {
                    case 1:
                        tiles[i + (range*atkDir) + (temp*atkDir)].TakeDamage_t(damage);
                        Debug.Log($"범위 {range}의 공격 - 뛰어넘은 거리 = {temp}");
                        break;
                    default:
                        for(int j = 1; j < range+1; j++)
                        {
                            tiles[i + (j * atkDir) + (temp * atkDir)].TakeDamage_t(damage);

                            Debug.Log($"범위 {range}의 공격 - 뛰어넘은 거리 = {temp}");
                            Debug.Log($"------{j}번째 공격 ");
                        }
                        break;
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
                if(i + 1 < tiles.Length && (tiles[i + 1].tileOnObjType == 0))//인덱스가 없을 때 예외처리 추가예정
                {
                    
                    tiles[i].MoveObj_t(tiles[i + 1].transform);
                }
                else
                {
                    Debug.Log("적이 있거나 배열 범위를 넘음");
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
                if (i - 1 >= 0 && tiles[i - 1].tileOnObjType == 0)//인덱스가 없을 때 예외처리 추가예정
                {

                    tiles[i].MoveObj_t(tiles[i - 1].transform);
                }
                else
                {
                    Debug.Log("적이 있거나 배열 범위를 넘음");
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
