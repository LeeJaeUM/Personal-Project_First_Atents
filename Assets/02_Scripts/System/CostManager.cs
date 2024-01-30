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

    //private int turnCount = 0;
    public int upCost_EndTurn = 2;
    WaitForSeconds delay01 = new WaitForSeconds(0.1f);

    public event System.Action<int> OnCostChange;

    private void Start()
    {
        curCost = maxCost;
        TurnManager.OnTurnEnd += TurnEndCostUp;
    }
    private void Update()
    {
        isCostZero = curCost <= minCost;    //�ڽ�Ʈ�� 0���� ũ�� false �۰ų� ������ true
        isCostFull = curCost >= maxCost;    //�ڽ�Ʈ�� maxCost���� ũ�ų� ������ true ������ false
    }

    public void CostChange(int cost)
    {
        curCost += cost;
        curCost = Mathf.Clamp(curCost, minCost, maxCost); //�ڽ�Ʈ �ִ� �ּҰ� ����
        OnCostChange?.Invoke(curCost);
    }

    public void TurnEndCostUp()
    {
        if (isCostFull) return;
        CostChange(upCost_EndTurn);
    }


}
