using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnChecker : MonoBehaviour
{
    public TextMeshProUGUI turnText; // TextMeshPro UI Text 오브젝트를 연결하세요.
    private PlayerContorller player;

    void Start()
    {
        // YourScriptName이 연결된 GameObject에 Player 스크립트가 있다고 가정합니다.
        player = FindAnyObjectByType<PlayerContorller>();

        if (player == null)
        {
            Debug.LogError("Player script not found!");
        }

        UpdateTurnText(); // 초기화 시 텍스트 업데이트
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && player != null)
        {
            player.IncrementTurn();
            UpdateTurnText();
        }
    }

    void UpdateTurnText()
    {
        if (turnText != null && player != null)
        {
            turnText.text = "Turn: " + player.turn.ToString();
        }
    }

}
