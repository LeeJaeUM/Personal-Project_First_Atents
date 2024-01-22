using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    enum ETileOn
    {
        Player,
        Enemy,
        Enviroment
    }



    public int tileOnObjType = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tileOnObjType = 1;
        }
        else if (collision.CompareTag("Enemy"))
        {
            tileOnObjType = 2;
        }
        else if (collision.CompareTag("Enviroment"))
        {
            tileOnObjType = 3;
        }
    }

    void TakeDamage()
    {

    }
}
