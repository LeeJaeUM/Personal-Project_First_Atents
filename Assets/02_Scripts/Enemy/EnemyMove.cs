using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //�̵������� Ȯ��
    public float oneMovingTime = 1.0f; //�� �ʸ��� �����ϰ��� 

    private void Start()
    {
        StartCoroutine(OneMove()); 
    }
    private void Update()
    {
        // ��ǥ ��ġ�� ���� ��ġ���� x������ -2��ŭ �����մϴ�.
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
