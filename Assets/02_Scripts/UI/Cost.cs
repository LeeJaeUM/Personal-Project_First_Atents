using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cost : MonoBehaviour
{
    TextMeshProUGUI cost;
    int drawCostValue = 6;

    private void Start()
    {
        cost = GetComponent<TextMeshProUGUI>();
        CostManager costManager = CostManager.Inst.GetComponent<CostManager>();
        costManager.OnCostChange += RefreshCost;

        cost.text = "6 / 6";
    }

    private void Update()
    {
        cost.text = $"{drawCostValue} / {CostManager.Inst.maxCost}";
    }

    void RefreshCost(int cost)
    {
        drawCostValue = cost;
    }
}
