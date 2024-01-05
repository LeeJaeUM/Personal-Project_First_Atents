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

        //애니메이터에서 클립 길이 받아오기
        //GetCurrentAnimatorClipInfo(0) : 애니메이터의 첫 번째 레이어의 클립 정보 받아오기
        //GetCurrentAnimatorClipInfo(0)[0] : 애니메이터의 첫 번째 레이어에 있는 애니메이션 클립 중 첫 번째 클립의 정보
        
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
