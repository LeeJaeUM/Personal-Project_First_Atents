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

    public event System.Action<int> OnCostChange;

    private void Start()
    {
        curCost = maxCost;
        curCost = Mathf.Clamp(curCost, minCost, maxCost); //코스트 최대 최소값 제한
    }
    private void Update()
    {
        isCostZero = curCost <= minCost;    //코스트가 0보다 크면 false 작거나 같으면 true
        isCostFull = curCost >= maxCost;    //코스트가 maxCost보다 크거나 같으면 true 작으면 false
    }

    public void CostChange(int cost)
    {
        curCost += cost;
        OnCostChange?.Invoke(cost);
    }
}
