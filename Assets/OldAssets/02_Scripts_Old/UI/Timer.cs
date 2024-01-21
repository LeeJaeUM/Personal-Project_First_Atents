using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float currentTime;     // 현재 경과 시간

    public TextMeshProUGUI timeText;  // TextMeshPro Text UI

    void Update()
    {
        currentTime += Time.deltaTime;  // 경과 시간을 더합니다.
        UpdateTimeText();
    }

    void UpdateTimeText()
    {
        // 텍스트 업데이트
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((currentTime * 1000) % 1000);

        timeText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
