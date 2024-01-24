using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Tile tileOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            tileOn = collision.GetComponent<Tile>();
        }
    }
    void Update()
    {
#if UNITY_EDITOR
        InpuCheckKey();
#endif
    }

    void InpuCheckKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
            TileManager.Inst.TestTileAttack(tileOn, false, 1, 1, false, 0);
        if (Input.GetKeyDown(KeyCode.S))
            TileManager.Inst.TestTileAttack(tileOn, false, 2, 2, false, 0);
    }
}
