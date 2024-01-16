using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnChecker : MonoBehaviour
{
    public TextMeshProUGUI turnText; // TextMeshPro UI Text ������Ʈ�� �����ϼ���.
    private PlayerContorller player;

    void Start()
    {
        // YourScriptName�� ����� GameObject�� Player ��ũ��Ʈ�� �ִٰ� �����մϴ�.
        player = FindAnyObjectByType<PlayerContorller>();

        if (player == null)
        {
            Debug.LogError("Player script not found!");
        }

        UpdateTurnText(); // �ʱ�ȭ �� �ؽ�Ʈ ������Ʈ
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
