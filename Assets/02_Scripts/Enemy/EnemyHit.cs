using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyHit : MonoBehaviour
{
    Animator anim;
    public float animLength;

    public Vector3 deathPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        //�ִϸ����Ϳ��� Ŭ�� ���� �޾ƿ���
        //GetCurrentAnimatorClipInfo(0) : �ִϸ������� ù ��° ���̾��� Ŭ�� ���� �޾ƿ���
        //GetCurrentAnimatorClipInfo(0)[0] : �ִϸ������� ù ��° ���̾ �ִ� �ִϸ��̼� Ŭ�� �� ù ��° Ŭ���� ����
        
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("Hited Enemy!!");
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("Die", true);
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