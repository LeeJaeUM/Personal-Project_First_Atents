using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContorller : MonoBehaviour
{
    PlayerInputAction inputAction;
    Animator anim;
    Rigidbody2D rigid2d;

    [Header("공격관련")]
    readonly int Attack_String = Animator.StringToHash("Attack");
    public BoxCollider2D attackCollider;
    Transform attackTransform;
    public float attackTime = 0.1f;

    [Header("이동관련")]
    private Vector2 moveInput;
    public float moveSpeed = 2f; // 이동 속도 조절
    public float targetDistance = 1f; // 목표 거리
    private bool isMoving = false;
    private float elapsedTime = 0f;
    Vector2 startPosition;
    Vector2 targetPosition;
    private void Awake()
    {
        inputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        attackTransform = transform.GetChild(0);
        attackCollider = attackTransform.GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        inputAction.Player.Enable();
        inputAction.Player.Move.performed += OnMove;
        inputAction.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputAction.Player.Attack.performed -= OnAttack;
        inputAction.Player.Move.performed -= OnMove;
        inputAction.Player.Disable();
    }
    private void OnMove(InputAction.CallbackContext context)
    {
        // Up, Down, Right, Left 확인
        if (moveInput.y > 0)
        {
            // Up 키가 눌렸을 때
            Debug.Log("Up key pressed");
        }
        else if (moveInput.y < 0)
        {
            // Down 키가 눌렸을 때
            Debug.Log("Down key pressed");
        }

        if (moveInput.x > 0)
        {
            // Right 키가 눌렸을 때
            Debug.Log("Right key pressed");
        }
        else if (moveInput.x < 0)
        {
            // Left 키가 눌렸을 때
            Debug.Log("Left key pressed");
        }
        // Move 입력 값 얻기
        moveInput = context.ReadValue<Vector2>();
        // 대각선 이동 벡터를 수정하여 길이가 1 이하로 제한
        if (Mathf.Abs(moveInput.x) == 1f)
        {
            moveInput.y = 0f;
        }
        else if (Mathf.Abs(moveInput.y) == 1f)
        {
            moveInput.x = 0f;
        }
        
        
        if (!isMoving)  //이동 중이 아닐 떄만 이동 가능
        {
            // 이동 코루틴 시작
            StartCoroutine(MoveToTarget());
        }
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnAttack 눌림");
            anim.SetTrigger(Attack_String);
            StartCoroutine(Attack());
        }
    }

    //private void FixedUpdate()
    //{
    //    //rigid2d.MovePosition(rigid2d.position + (Vector2)(Time.fixedDeltaTime * moveSpeed * moveInput));
    //}

    IEnumerator Attack()
    {
        attackCollider.enabled = false;
        attackCollider.enabled = true; 
        yield return new WaitForSeconds(attackTime);
        attackCollider.enabled = false;
    }
    IEnumerator MoveToTarget()
    {
        isMoving = true; //이동중 플래그
        startPosition = transform.position;
        targetPosition = startPosition + moveInput * targetDistance;

        elapsedTime = 0f;

        while (elapsedTime < 1.1f)
        {
            // 보간을 사용하여 부드럽게 이동
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime);

            // 시간 업데이트
            elapsedTime += Time.fixedDeltaTime * moveSpeed;

            yield return null; // 다음 프레임 대기
        }
        isMoving = false; //이동 종료 플래그

        // 이동이 끝난 후 추가 작업을 수행하거나 초기화
        // 플레이어의 위치를 반올림하여 소수점 제거
         //transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }
}
