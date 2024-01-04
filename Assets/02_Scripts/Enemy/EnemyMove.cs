using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1.0f; // 이동 속도

    private Vector2 targetPosition; // 목표 위치
    private bool isMoving = false;      //이동중인지 확인
    private float oneMovingTime = 1.0f; //몇 초마다 움직일건지 

    private void Start()
    {
        StartCoroutine(OneMove());
    }
    private void Update()
    {
        // 목표 위치를 현재 위치보다 x축으로 -2만큼 설정합니다.
        Vector2 targetPosition = new Vector2(transform.position.x - 2, transform.position.y);
        if (isMoving)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    private void Jump()
    {

    }

    IEnumerator OneMove()
    {
        while (true)
        {
            isMoving = true;
            yield return new WaitForSeconds(oneMovingTime);
            isMoving = false;
            yield return new WaitForSeconds(oneMovingTime);
        }
    }
}
