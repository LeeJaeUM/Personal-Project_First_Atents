using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Enemy_MoveEffect : MonoBehaviour
{
    Animator anim;
    float animLength = 0f;
    SpriteRenderer moveSprite;

    public bool isPlaying = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        moveSprite = GetComponent<SpriteRenderer>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        StartCoroutine(EffectOnOff());
    }

    IEnumerator EffectOnOff()
    {
        while (true)
        {
            // �ִϸ��̼��� ��� ���� �ƴϸ� ���
            if (!isPlaying)
            {
                isPlaying = true;
                moveSprite.enabled = true;
                anim.SetTrigger("PlayAnimation");
            }
            yield return new WaitForSeconds(animLength);
            moveSprite.enabled = false;
            // 1�� ���
            yield return new WaitForSeconds(1.0f - animLength);

            // �ִϸ��̼��� �����ϰ� �÷��� �ʱ�ȭ
            anim.ResetTrigger("PlayAnimation");
            isPlaying = false;
        }

    }
}
