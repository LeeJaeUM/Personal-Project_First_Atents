using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyHit : MonoBehaviour
{
    Animator anim;
    public float animLength;

    public Vector3 deathPosition;

    public float lifeTime = 10.0f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            //Debug.Log("Hited Enemy!!");
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("Die", true);
        StopAllCoroutines();
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
