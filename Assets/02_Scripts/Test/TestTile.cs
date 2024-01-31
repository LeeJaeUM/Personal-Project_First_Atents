using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestTile : TestBase
{
    //public Tile[] tiles;
    public TileManager tileManager;
    public CostManager costManager;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        tileManager.PlayerMove(true, 1);
    }
    protected override void OnTest2(InputAction.CallbackContext context)
    {
        tileManager.PlayerMove(false, 1);
    }
    protected override void OnTest3(InputAction.CallbackContext context)
    {
        costManager.CostChange(1);
    }
    protected override void OnTest4(InputAction.CallbackContext context)
    {
        costManager.CostChange(4);
    }
    protected override void OnTest5(InputAction.CallbackContext context)
    {
        
    }
}
