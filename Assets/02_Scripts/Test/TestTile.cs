using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestTile : TestBase
{
    public Tile[] tiles;
    public TileManager tileManager;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        tileManager.MovePlayer_Right();
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        tileManager.PlayerAttackRight_One(1);
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        tiles[2].TakeDamage_t(1);
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        tiles[3].TakeDamage_t(1);
    }
}
