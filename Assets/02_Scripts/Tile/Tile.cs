using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //enum ETileOn
    //{
    //    Player,
    //    Enemy,
    //    Enviroment
    //}

    [SerializeField]Character onCharacer;

    public int tileOnObjType = 0;

    public event System.Action<int, Tile> OnTileOnObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tileOnObjType = 1;
            onCharacer = collision.GetComponent<Character>();
            // �̺�Ʈ �߻��� �� Tile ���� ����
            OnTileOnObject?.Invoke(tileOnObjType, this);
        }
        else if (collision.CompareTag("Enemy"))
        {
            tileOnObjType = 2;
            onCharacer = collision.GetComponent<Character>();
            // �̺�Ʈ �߻��� �� Tile ���� ����
            OnTileOnObject?.Invoke(tileOnObjType, this);
        }
        else if (collision.CompareTag("Enviroment"))
        {
            tileOnObjType = 3;
            onCharacer = collision.GetComponent<Character>();
            // �̺�Ʈ �߻��� �� Tile ���� ����
            OnTileOnObject?.Invoke(tileOnObjType, this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            tileOnObjType = 0;
            onCharacer = null;
    }

    //public void TakeDamage_t(int damage)
    //{
    //    if(onCharacer != null)
    //    {
    //        onCharacer.TakeDamage(damage);
    //    }
    //    else
    //    {
    //        Debug.Log("ĳ���Ͱ� Ÿ�� ���� ����");
    //    }
        
    //}

    public void MoveObj_t(Transform target)
    {
        onCharacer.transform.position = target.position;
    }

    public void TakeDamage_t(int damage)
    {
        if (onCharacer != null)
        {
            onCharacer.TakeDamage(damage);

        }
        else
        {
            Debug.Log("ĳ���Ͱ� Ÿ�� ���� ����");
        }
    }
}
