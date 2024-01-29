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

    SpriteRenderer spriteRenderer;

    Vector3 yUpVec = new Vector3(0, 0.5f, 0);
    /*
    public Color playerOn_Blue = new Color(50f,100f,255f,255f);
    public Color enemyOn_Red = new Color(221f,55f,47f,255f);
    public Color attackStandby_Red = new Color(225f,141f,0f, 255f);
    public Color defaultColor = new Color(225f, 194f, 208f, 255f);
    */
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
      /*  playerOn_Blue = new Color(50f / 255f, 100f / 255f, 255f / 255f, 255f / 255f);
        enemyOn_Red = new Color(221f / 255f, 55f / 255f, 47f / 255f, 255f / 255f);
        attackStandby_Red = new Color(225f / 255f, 141f / 255f, 0f / 255f, 255f / 255f);
        defaultColor = new Color(225f / 255f, 194f / 255f, 208f / 255f, 255f / 255f);
      */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tileOnObjType = 1;
            onCharacer = collision.GetComponent<Character>();
            spriteRenderer.color = Color.blue;
            // 이벤트 발생할 때 Tile 정보 전달
            OnTileOnObject?.Invoke(tileOnObjType, this);
        }
        else if (collision.CompareTag("Enemy"))
        {
            tileOnObjType = 2;
            onCharacer = collision.GetComponent<Character>();
            spriteRenderer.color = Color.red;
            // 이벤트 발생할 때 Tile 정보 전달
            OnTileOnObject?.Invoke(tileOnObjType, this);
        }
        else if (collision.CompareTag("Enviroment"))
        {
            tileOnObjType = 3;
            onCharacer = collision.GetComponent<Character>();
            // 이벤트 발생할 때 Tile 정보 전달
            OnTileOnObject?.Invoke(tileOnObjType, this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            tileOnObjType = 0; 
            onCharacer = null;
        spriteRenderer.color = Color.white;
    }

    //public void TakeDamage_t(int damage)
    //{
    //    if(onCharacer != null)
    //    {
    //        onCharacer.TakeDamage(damage);
    //    }
    //    else
    //    {
    //        Debug.Log("캐릭터가 타일 위에 없다");
    //    }

    //}
    public void MoveObj_t(Transform target)
    {
        onCharacer.transform.position = target.position + yUpVec;
    }

    public void TakeDamage_t(int damage)
    {
        if (onCharacer != null)
        {
            onCharacer.TakeDamage(damage);

        }
        else
        {
            Debug.Log("캐릭터가 타일 위에 없다");
        }
    }

}
