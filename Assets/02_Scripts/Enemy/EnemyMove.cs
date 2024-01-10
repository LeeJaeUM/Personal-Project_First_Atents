using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //이동중인지 확인
    public float oneMovingTime = 1.0f; //몇 초마다 움직일건지 
    public float moveSpeed = 2;
    public float moveLength = 2f;
    private float elapsedTime = 0f;

    Animator anim;
    readonly int Move_String = Animator.StringToHash("Move");
    readonly int Die_String = Animator.StringToHash("Die");

    public bool isDie = false;
    public float animLength;
    Vector2 targetDistance = Vector2.zero;
    Vector2 startPosition = Vector2.zero;
    Vector2 targetPosition = Vector2.zero;
    public Vector3 deathPosition;

    public float lifeTime = 10.0f;

    IEnumerator MoveCoroutine;

    GameObject player;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    private void Start()
    {
        MoveCoroutine = OneTimeMove();
        StartCoroutine(MoveCoroutine);
        Destroy(gameObject, lifeTime);

        // 플레이어를 찾음 (가정: 플레이어는 "Player" 태그를 가지고 있음)
        player = GameObject.FindGameObjectWithTag("Player");
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

    IEnumerator OneTimeMove()
    {
        isMoving = true;
        if(player != null)
        {
            // 플레이어와 Enemy 간의 방향을 계산
            targetDistance = (player.transform.position - transform.position).normalized;
            // 플레이어의 위치에서 x값이 1 또는 -1이면 y값을 0으로, y값이 1 또는 -1이면 x값을 0으로 설정
            if (Mathf.Abs(targetDistance.x) == 1f)
            {
                targetDistance.y = 0f;
            }
            else if (Mathf.Abs(targetDistance.y) == 1f)
            {
                targetDistance.x = 0f;
            }
            // 타겟 위치 계산
            targetPosition = (Vector2)transform.position + targetDistance * moveLength;
        }
        else
        {
            // 플레이어를 찾지 못한 경우에 대한 예외 처리 (예: 로그 출력)
            Debug.LogError("Player not found!");
            yield return new WaitForSeconds(oneMovingTime);
        }

        startPosition = transform.position;
        targetPosition = startPosition * targetDistance;

        // 이동 애니메이션
        anim.SetTrigger(Move_String);

        elapsedTime = 0f;
        while (elapsedTime < 1.1f)
        {
                // 보간을 사용하여 부드럽게 이동
                transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime);

                // 시간 업데이트
                elapsedTime += Time.fixedDeltaTime * moveSpeed;

                yield return null; // 다음 프레임 대기
        }
        isMoving = false;
    }

    private void Die()
    {
        isDie = true;
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
