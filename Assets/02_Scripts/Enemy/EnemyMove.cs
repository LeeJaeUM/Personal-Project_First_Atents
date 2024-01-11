using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //이동중인지 확인
    public float oneMovingTime = 1.0f; //몇 초마다 움직일건지 
    public float moveSpeed = 2;         //이동속도
    public float moveLength = 1f;       //이동거리
    private float elapsedTime = 0f;     //Time.deltaTime용 변수

    Animator anim;
    readonly int Move_String = Animator.StringToHash("Move");
    readonly int Die_String = Animator.StringToHash("Die");

    public bool isDie = false;      //사망확인
    public float animLength;        //애니메이션 길이
    Vector2 distanceToPlayer = Vector2.zero;  //이동할  거리
    Vector2 startPosition = Vector2.zero;   //이동을 시작한 위치
    Vector2 targetPosition = Vector2.zero;  //이동 후 도착할 위치
    public Vector3 deathPosition;           //사망애니메이션 중 움직이기위한 위치

    IEnumerator MoveCoroutine;          //이동 코루틴

    GameObject player;

    float absX; //절대값 비교용
    float absY; //절대값 비교용

    private void Awake()
    {
        anim = GetComponent<Animator>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    private void Start()
    {
        MoveCoroutine = OneTimeMove();
        StartCoroutine(MoveCoroutine);
       // Destroy(gameObject, lifeTime);

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
        if(player != null)  //플레이어가 null이 아닐때
        {
            // 플레이어와 Enemy 간의 방향을 계산
            distanceToPlayer = (player.transform.position - transform.position);
        }
        else
        {
            // 플레이어를 찾지 못한 경우에 대한 예외 처리 (예: 로그 출력)
            Debug.LogError("Player not found!");
            yield return new WaitForSeconds(oneMovingTime);
        }

        // 이동 애니메이션
        anim.SetTrigger(Move_String);

        // x와 y의 절대값 비교
        absX = Mathf.Abs(distanceToPlayer.x);
        absY = Mathf.Abs(distanceToPlayer.y);


        // x의 절대값이 더 크면 x축으로 이동, 그렇지 않으면 y축으로 이동
        if (absX > absY)
        {
            // 플레이어가 오른쪽에 있으면 x축으로 이동
            if (distanceToPlayer.x > 0)
                targetPosition = new Vector2(transform.position.x + moveLength, transform.position.y);
            // 플레이어가 왼쪽에 있으면 x축으로 이동
            else
                targetPosition = new Vector2(transform.position.x - moveLength, transform.position.y);
        }
        else
        {
            // 플레이어가 위에 있으면 y축으로 이동
            if (distanceToPlayer.y > 0)
                targetPosition = new Vector2(transform.position.x, transform.position.y + moveLength);
            // 플레이어가 아래에 있으면 y축으로 이동
            else
                targetPosition = new Vector2(transform.position.x, transform.position.y - moveLength);
        }

        startPosition = transform.position;
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
