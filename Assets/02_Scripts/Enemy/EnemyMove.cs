using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //이동중인지 확인
    public float oneMovingTime = 1.0f; //몇 초마다 움직일건지 
    public float moveLenght = 3f;

    Animator anim;
    readonly int Move_String = Animator.StringToHash("Move");
    readonly int Die_String = Animator.StringToHash("Die");

    public float animLength;
    public Vector3 deathPosition;

    public float lifeTime = 10.0f;

    IEnumerator MoveCoroutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    private void Start()
    {
        MoveCoroutine = OneMove();
        StartCoroutine(MoveCoroutine);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy touch Player_coL");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            //Debug.Log("Hited Enemy!!");
            Die();
        }
    }


    IEnumerator OneMove()
    {
        while (true)
        {
            Vector2 targetPosition = new Vector2(transform.position.x - moveLenght, transform.position.y);
            isMoving = true;
            yield return new WaitForSeconds(oneMovingTime);
            anim.SetTrigger(Move_String);
            transform.position = targetPosition;
            isMoving = false;

        }
    }

    private void Die()
    {
        StopCoroutine(MoveCoroutine);
        anim.SetBool(Die_String, true);
        StartCoroutine(Die_Move());
        Destroy(this.gameObject, animLength);
    }

    IEnumerator Die_Move()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < animLength)
        {
            // 현재 시간에 따른 보간 비율 계산
            float t = elapsedTime / animLength;

            // SmoothStep 함수를 사용하여 처음에는 빠르게, 나중에는 느리게 이동
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // SmoothStep 함수로 계산된 보간 비율을 사용하여 부드럽게 이동
            transform.position = Vector3.Lerp(startPosition, deathPosition, smoothT);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 한 프레임 대기
            yield return null;
        }
        // 애니메이션이 끝난 후 필요한 처리 추가
        // 예를 들어 오브젝트 비활성화, 파괴 등
    }

}
