using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyHit : MonoBehaviour
{
    Animator anim;
    float animLength;
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
       // Destroy(this.gameObject, animLength);
    }
}
