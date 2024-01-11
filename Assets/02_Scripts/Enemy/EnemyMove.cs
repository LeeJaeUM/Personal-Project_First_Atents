using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public bool isMoving = false;      //�̵������� Ȯ��
    public float oneMovingTime = 1.0f; //�� �ʸ��� �����ϰ��� 
    public float moveSpeed = 2;         //�̵��ӵ�
    public float moveLength = 1f;       //�̵��Ÿ�
    private float elapsedTime = 0f;     //Time.deltaTime�� ����

    Animator anim;
    readonly int Move_String = Animator.StringToHash("Move");
    readonly int Die_String = Animator.StringToHash("Die");

    public bool isDie = false;      //���Ȯ��
    public float animLength;        //�ִϸ��̼� ����
    Vector2 distanceToPlayer = Vector2.zero;  //�̵���  �Ÿ�
    Vector2 startPosition = Vector2.zero;   //�̵��� ������ ��ġ
    Vector2 targetPosition = Vector2.zero;  //�̵� �� ������ ��ġ
    public Vector3 deathPosition;           //����ִϸ��̼� �� �����̱����� ��ġ

    IEnumerator MoveCoroutine;          //�̵� �ڷ�ƾ

    GameObject player;

    float absX; //���밪 �񱳿�
    float absY; //���밪 �񱳿�

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
        if(player != null)  //�÷��̾ null�� �ƴҶ�
        {
            // �÷��̾�� Enemy ���� ������ ���
            distanceToPlayer = (player.transform.position - transform.position);
        }
        else
        {
            // �÷��̾ ã�� ���� ��쿡 ���� ���� ó�� (��: �α� ���)
            Debug.LogError("Player not found!");
            yield return new WaitForSeconds(oneMovingTime);
        }

        // �̵� �ִϸ��̼�
        anim.SetTrigger(Move_String);

        // x�� y�� ���밪 ��
        absX = Mathf.Abs(distanceToPlayer.x);
        absY = Mathf.Abs(distanceToPlayer.y);


        // x�� ���밪�� �� ũ�� x������ �̵�, �׷��� ������ y������ �̵�
        if (absX > absY)
        {
            // �÷��̾ �����ʿ� ������ x������ �̵�
            if (distanceToPlayer.x > 0)
                targetPosition = new Vector2(transform.position.x + moveLength, transform.position.y);
            // �÷��̾ ���ʿ� ������ x������ �̵�
            else
                targetPosition = new Vector2(transform.position.x - moveLength, transform.position.y);
        }
        else
        {
            // �÷��̾ ���� ������ y������ �̵�
            if (distanceToPlayer.y > 0)
                targetPosition = new Vector2(transform.position.x, transform.position.y + moveLength);
            // �÷��̾ �Ʒ��� ������ y������ �̵�
            else
                targetPosition = new Vector2(transform.position.x, transform.position.y - moveLength);
        }

        startPosition = transform.position;
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
