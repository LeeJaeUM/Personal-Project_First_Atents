using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float currentTime;     // ���� ��� �ð�

    public TextMeshProUGUI timeText;  // TextMeshPro Text UI

    void Update()
    {
        currentTime += Time.deltaTime;  // ��� �ð��� ���մϴ�.
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        // �ؽ�Ʈ ������Ʈ
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        timeText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
