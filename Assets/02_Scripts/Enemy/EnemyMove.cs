using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //이동중인지 확인
    public float oneMovingTime = 1.0f; //몇 초마다 움직일건지 

    private void Start()
    {
        StartCoroutine(OneMove()); 
    }
    private void Update()
    {
        // 목표 위치를 현재 위치보다 x축으로 -2만큼 설정합니다.
        //if (isMoving)
        //{
        //    transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        //}
        if(isMoving)
        {
        }
    }

    private void Jump()
    {

    }

    IEnumerator OneMove()
    {
        while (true)
        {
            Vector2 targetPosition = new Vector2(transform.position.x - 3, transform.position.y);
            isMoving = true;
            transform.position = targetPosition;
            yield return new WaitForSeconds(oneMovingTime);
            isMoving = false;
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Enemt touch_coL");
    }

}
