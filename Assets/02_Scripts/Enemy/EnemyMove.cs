using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //�̵������� Ȯ��
    public float oneMovingTime = 1.0f; //�� �ʸ��� �����ϰ��� 
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

        // �÷��̾ ã�� (����: �÷��̾�� "Player" �±׸� ������ ����)
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
            // �÷��̾�� Enemy ���� ������ ���
            targetDistance = (player.transform.position - transform.position).normalized;
            // �÷��̾��� ��ġ���� x���� 1 �Ǵ� -1�̸� y���� 0����, y���� 1 �Ǵ� -1�̸� x���� 0���� ����
            if (Mathf.Abs(targetDistance.x) == 1f)
            {
                targetDistance.y = 0f;
            }
            else if (Mathf.Abs(targetDistance.y) == 1f)
            {
                targetDistance.x = 0f;
            }
            // Ÿ�� ��ġ ���
            targetPosition = (Vector2)transform.position + targetDistance * moveLength;
        }
        else
        {
            // �÷��̾ ã�� ���� ��쿡 ���� ���� ó�� (��: �α� ���)
            Debug.LogError("Player not found!");
            yield return new WaitForSeconds(oneMovingTime);
        }

        startPosition = transform.position;
        targetPosition = startPosition * targetDistance;

        // �̵� �ִϸ��̼�
        anim.SetTrigger(Move_String);

        elapsedTime = 0f;
        while (elapsedTime < 1.1f)
        {
                // ������ ����Ͽ� �ε巴�� �̵�
                transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime);

                // �ð� ������Ʈ
                elapsedTime += Time.fixedDeltaTime * moveSpeed;

                yield return null; // ���� ������ ���
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
            // ���� �ð��� ���� ���� ���� ���
            float t = elapsedTime / animLength;

            // SmoothStep �Լ��� ����Ͽ� ó������ ������, ���߿��� ������ �̵�
            float smoothT = Mathf.SmoothStep(0f, 1f, t);

            // SmoothStep �Լ��� ���� ���� ������ ����Ͽ� �ε巴�� �̵�
            transform.position = Vector3.Lerp(startPosition, deathPosition, smoothT);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            // �� ������ ���
            yield return null;
        }
        // �ִϸ��̼��� ���� �� �ʿ��� ó�� �߰�
        // ���� ��� ������Ʈ ��Ȱ��ȭ, �ı� ��
    }

}
