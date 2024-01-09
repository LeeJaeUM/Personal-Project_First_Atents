using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //�̵������� Ȯ��
    public float oneMovingTime = 1.0f; //�� �ʸ��� �����ϰ��� 
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

        // �÷��̾ ã�� (����: �÷��̾�� "Player" �±׸� ������ ����)
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!isDie)
        {
            // ������ ����Ͽ� �ε巴�� �̵�
            transform.position = Vector2.Lerp(transform.position, targetPosition, elapsedTime / 10);

            // �ð� ������Ʈ
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
                // �÷��̾�� Enemy ���� ������ ���
                direction = (player.transform.position - transform.position).normalized;

                // Ÿ�� ��ġ ���
                targetPosition = (Vector2)transform.position + direction * moveLength;

                // �̵� ����
                isMoving = true;

                // �̵� �ִϸ��̼� �� ���� ��ġ ����
                anim.SetTrigger(Move_String);
                //transform.position = targetPosition;

                yield return new WaitForSeconds(oneMovingTime);
                isMoving = false;
            }
            else
            {
                // �÷��̾ ã�� ���� ��쿡 ���� ���� ó�� (��: �α� ���)
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
