using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Enemy_MoveEffect : MonoBehaviour
{
    Animator anim;
    float animLength = 0f;
    SpriteRenderer moveSprite;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        moveSprite = GetComponent<SpriteRenderer>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        StartCoroutine(EffectOnOff());
    }

    IEnumerator EffectOnOff()
    {
        moveSprite.enabled = true;
        yield return new WaitForSeconds(animLength);
        moveSprite.enabled = false;
        yield return new WaitForSeconds(1.0f - animLength);
    }
}
