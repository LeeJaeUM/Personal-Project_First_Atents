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

    [Header("���ݰ���")]
    readonly int Attack_String = Animator.StringToHash("Attack");
    public BoxCollider2D attackCollider;
    Transform attackTransform;
    public float attackTime = 0.1f;

    [Header("�̵�����")]
    private Vector2 moveInput;
    public float moveSpeed = 2f; // �̵� �ӵ� ����
    public float targetDistance = 1f; // ��ǥ �Ÿ�
    private void Awake()
    {
        inputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
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
        // Up, Down, Right, Left Ȯ��
        if (moveInput.y > 0)
        {
            // Up Ű�� ������ ��
            Debug.Log("Up key pressed");
        }
        else if (moveInput.y < 0)
        {
            // Down Ű�� ������ ��
            Debug.Log("Down key pressed");
        }

        if (moveInput.x > 0)
        {
            // Right Ű�� ������ ��
            Debug.Log("Right key pressed");
        }
        else if (moveInput.x < 0)
        {
            // Left Ű�� ������ ��
            Debug.Log("Left key pressed");
        }
        // Move �Է� �� ���
        moveInput = context.ReadValue<Vector2>();

        // �̵� �ڷ�ƾ ����
        StartCoroutine(MoveToTarget());
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnAttack ����");
            anim.SetTrigger(Attack_String);
            StartCoroutine(Attack());
        }
    }


    private void Update()
    {
        
    }

    IEnumerator Attack()
    {
        attackCollider.enabled = false;
        attackCollider.enabled = true; 
        yield return new WaitForSeconds(attackTime);
        attackCollider.enabled = false;
    }
    IEnumerator MoveToTarget()
    {
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = startPosition + moveInput * targetDistance;

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            // ������ ����Ͽ� �ε巴�� �̵�
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime * moveSpeed;

            yield return null; // ���� ������ ���
        }

        // �̵��� ���� �� �߰� �۾��� �����ϰų� �ʱ�ȭ
    }
}
