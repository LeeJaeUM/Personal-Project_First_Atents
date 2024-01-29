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
        // 각 Tile에 대해 이벤트 리스너 등록
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
        // tile 정보를 사용하여 어떤 Tile에서 이벤트가 발생했는지 확인
        Debug.Log($"오브젝트 타입 {objType} on Tile: {tile.gameObject.name}");
        if(objType == 1 )
        {
            playerOnTile = tile;
        }
    }

    #region 일단 테스트
    private void PlayerCardStandby(int itemCode)
    {
        switch(itemCode)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;   
            case 3: break;
            case 4: break;
            case 5: break;
            case 6: break;
            case 7: break; 
            case 8: break;
            case 9: break;
            case 10: break;
            default:
                Debug.Log("현재 없는 카드 코드입니다");
                break;
        }
    }

    public void MovePlayer(int attackDirection)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileOnObjType == 1)
            {
                if (i + 1 < tiles.Length && (tiles[i + attackDirection].tileOnObjType == 0))//인덱스가 없을 때 예외처리 추가예정
                {

                    tiles[i].MoveObj_t(tiles[i + attackDirection].transform);
                }
                else
                {
                    Debug.Log("적이 있거나 배열 범위를 넘음");
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
                tiles[i + range * attackDirection].TakeDamage_t(1);
            }
        }
    }

    #endregion

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
