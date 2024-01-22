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
    // Ÿ�� ���� �ִ��� ���θ� �Ǵ��ϴ� �Լ�

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
    private bool IsOnTile(Vector2 position)
    {
        foreach (Tile tile in tiles)
        {
            // Ÿ���� Collider�� �浹 ������ ����
            if (tile.GetComponent<Collider2D>().OverlapPoint(position))
            {
                return true; // Ÿ�� ���� �ִٸ� true ��ȯ
            }
        }
        return false; // Ÿ�� ���� ���ٸ� false ��ȯ
    }
}
