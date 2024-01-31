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
    private bool isRight = true;
    private bool isLeft = false;

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

    void OnDestroy()
    {
        // 스크립트가 파괴될 때 이벤트 핸들러를 제거
        cardManager.OnCardStandBy -= PlayerCardStandby;
        foreach (var tile in tiles)
        {
            tile.OnTileOnObject -= ObjectOnTile;
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

    #region 플레이어 행동
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
                PlayerTileAttack(isRight, 2, 1, false, 0);
                break;
            case 5: break;
            case 6:
                PlayerTileAttack(isLeft, 2, 1, false, 0);
                break;
            case 7: break; 
            case 8:
                PlayerTileAttack(isRight, 1, 2, false, 0);
                break;
            case 9: break;
            case 10:
                PlayerMove(right+1);
                break;
            default:
                Debug.Log("현재 없는 카드 코드입니다");
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
                    Debug.Log("적이 있거나 배열 범위를 넘음");
                }
            }
        }
    }

    public void PlayerTileAttack(bool right, int damage, int range, bool isOverTile, int overRange)
    {
        int temp = 0;
        int atkDir = 1;
        if (isOverTile)
        {
            temp += overRange; //건너뛸 거리
        }
        if (!right) //fasle라면 왼쪽 공격
        {
            atkDir = -1;
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].tileOnObjType == 1) // 배열에서 플레이어가 위치한 타일을 찾기
            {
                switch (range)
                {
                    case 0:
                        tiles[i].TakeDamage_t(damage); 
                        break;
                    case 1:
                        tiles[i + (range * atkDir) + (temp * atkDir)].TakeDamage_t(damage);
                        Debug.Log($"범위 {range}의 공격 - 뛰어넘은 거리 = {temp}");
                        break;
                    default:
                        for (int j = 1; j < range + 1; j++)
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

    #endregion
    /// <summary>
    /// Enemy가 사용하는 공격 함수_TileManager
    /// </summary>
    /// <param name="myTile">현재 위치한 타일</param>
    /// <param name="right">true=우측/false좌측</param>
    /// <param name="damage">데미지</param>
    /// <param name="range">범위</param>
    /// <param name="isOverTile">띄는칸이있는가?</param>
    /// <param name="overRange">몇칸 띌건지</param>
    public void EnemyTileAttack(Tile myTile, bool right, int damage, int range, bool isOverTile, int overRange)
    {
       int temp = 0;
       int atkDir = 1;
       if (isOverTile)
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
    public void EnemyTileMove(Tile myTile, bool right, int range)
    {
        int moveDir = 1;
        if (!right) //fasle라면 왼쪽 이동
        {
            moveDir = -1;
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] == myTile) // 배열에서 호출한 타일을 찾기
            {
                if (tiles[i + (range * moveDir)].tileOnObjType == 0)
                {

                    tiles[i].MoveObj_t(tiles[i + (range * moveDir)].transform);
                }
                else
                {
                    Debug.Log("플레이어가 있거나 배열 범위를 넘음");
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
            if (tiles[i] == myTile) // 배열에서 호출한 타일을 찾기
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
