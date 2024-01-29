using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationPanel : MonoBehaviour
{
    //2h 10분40
    public TMP_Text notificationTMP;

    public void Show(string message)
    {
        notificationTMP.text = message;
        notificationTMP.enabled = true;
        //ScaleOne();
        //Invoke("ScaleZero", 1);
        StartCoroutine(ScaleFollow());
    }

    void Start() => ScaleZero();

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;
    [ContextMenu("ScaleZero")]
    void ScaleZero() => transform.localScale = Vector3.zero;

    IEnumerator ScaleFollow()
    {
        float elapsedTime = 0f;
        float duration = 0.3f;

        // 크기를 0에서 1로 증가
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f; // elapsedTime 초기화
        while(elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // 크기를 1에서 0으로 감소
        elapsedTime = 0f; // elapsedTime 초기화
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration) 
                ScaleZero();
            yield return null;
        }
    }
}
