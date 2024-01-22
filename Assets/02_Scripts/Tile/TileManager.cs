using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{


    [SerializeField] Tile[] tiles;

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
    }    
    // 타일 위에 있는지 여부를 판단하는 함수

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
    private bool IsOnTile(Vector2 position)
    {
        foreach (Tile tile in tiles)
        {
            // 타일의 Collider로 충돌 판정을 진행
            if (tile.GetComponent<Collider2D>().OverlapPoint(position))
            {
                return true; // 타일 위에 있다면 true 반환
            }
        }
        return false; // 타일 위에 없다면 false 반환
    }
}
