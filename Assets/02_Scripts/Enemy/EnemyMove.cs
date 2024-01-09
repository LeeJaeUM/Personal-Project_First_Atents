using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //이동중인지 확인
    public float oneMovingTime = 1.0f; //몇 초마다 움직일건지 
    public float moveLength = 2f;
    private float elapsedTime = 0f;

    Animator anim;
    readonly int Move_String = Animator.StringToHash("Move");
    readonly int Die_String = Animator.StringToHash("Die");

    public bool isDie = false;
    public float animLength;
    Vector2 direction;
    Vector2 targetPosition;
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
        MoveCoroutine = OneMove();
        StartCoroutine(MoveCoroutine);
        Destroy(gameObject, lifeTime);

        // 플레이어를 찾음 (가정: 플레이어는 "Player" 태그를 가지고 있음)
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!isDie)
        {
            // 보간을 사용하여 부드럽게 이동
            transform.position = Vector2.Lerp(transform.position, targetPosition, elapsedTime / 10);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;
        }
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
        ///while (true)
        ///{
        ///    Vector2 targetPosition = new Vector2(transform.position.x - moveLength, transform.position.y);
        ///    isMoving = true;
        ///    yield return new WaitForSeconds(oneMovingTime);
        ///    anim.SetTrigger(Move_String);
        ///    transform.position = targetPosition;
        ///    isMoving = false;
        ///}
        while (true)
        {

            if (player != null)
            {
                // 플레이어와 Enemy 간의 방향을 계산
                direction = (player.transform.position - transform.position).normalized;

                // 타겟 위치 계산
                targetPosition = (Vector2)transform.position + direction * moveLength;

                // 이동 가능
                isMoving = true;

                // 이동 애니메이션 및 실제 위치 변경
                anim.SetTrigger(Move_String);
                //transform.position = targetPosition;

                yield return new WaitForSeconds(oneMovingTime);
                isMoving = false;
            }
            else
            {
                // 플레이어를 찾지 못한 경우에 대한 예외 처리 (예: 로그 출력)
                Debug.LogError("Player not found!");
                yield return new WaitForSeconds(oneMovingTime);
            }
        }
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
