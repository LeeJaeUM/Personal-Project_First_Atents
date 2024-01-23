using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] Tile[] tiles;
    [SerializeField] Transform[] tilesTransforms;

    private void Start()
    {
        tilesTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            tilesTransforms[i] = child.GetComponent<Transform>();
        }
        tiles = GetComponentsInChildren<Tile>();
        
    }    
    
    
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
            if (tiles[i].tileOnObjType == 1)
            {
                if (tiles[i - 1].tileOnObjType == 0)
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
