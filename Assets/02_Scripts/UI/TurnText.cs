using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnText : MonoBehaviour
{
    public TMP_Text turnTMP;
    private int turnText = 1;

    private void Start()
    {
        turnTMP = GetComponent<TMP_Text>();
        TurnManager.OnTurnEnd += Show;
    }

    public void Show()
    {
        turnText++;
        turnTMP.text = turnText.ToString();
    }
}
