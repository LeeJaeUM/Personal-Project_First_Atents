using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    public static CostManager Inst { get; private set; }
    private void Awake() => Inst = this;

    public int maxCost = 6;
    public int minCost = 0;
    public int curCost = 0;

    public bool isCostZero = false;
    public bool isCostFull = false;

    public TestTile test;

    private void Start()
    {
        curCost = maxCost;
        test.OnCostChange += CostChange;
        curCost = Mathf.Clamp(curCost, minCost, maxCost); //코스트 최대 최소값 제한
    }
    private void Update()
    {
        if(curCost <= minCost)
        {
            isCostZero = true;
        }
        if(curCost >= maxCost)
        {

        }
    }

    void CostChange(int cost)
    {
        curCost += cost;
    }
}
