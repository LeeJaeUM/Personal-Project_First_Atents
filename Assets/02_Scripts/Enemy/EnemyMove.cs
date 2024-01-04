using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1.0f; // �̵� �ӵ�

    private Vector2 targetPosition; // ��ǥ ��ġ
    private bool isMoving = false;      //�̵������� Ȯ��
    private float oneMovingTime = 1.0f; //�� �ʸ��� �����ϰ��� 

    private void Start()
    {
        StartCoroutine(OneMove());
    }
    private void Update()
    {
        // ��ǥ ��ġ�� ���� ��ġ���� x������ -2��ŭ �����մϴ�.
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
