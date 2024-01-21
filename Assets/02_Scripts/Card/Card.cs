using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text attackTMP;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    public Item item;
    bool isFront;           //앞면인지 뒷면인지 판단
    public PRS originPRS;   //원본위치 : 마우스 이벤트로 확대/축소로 위치가 바뀌어도 돌아올 위치

    public void Setup(Item _item, bool _isFront)
    {
        this.item = _item;
        this.isFront = _isFront;

        if (this.isFront) ///앞면일때
        {
            character.sprite = this.item.sprite;
            nameTMP.text = this.item.name;
            attackTMP.text = this.item.attack.ToString();
            healthTMP.text = this.item.health.ToString();
        }
        else
        {
            card.sprite = cardBack;
            nameTMP.text = "";
            attackTMP.text = "";
            healthTMP.text = "";
        }
    }

    /// <summary>
    /// 두트윈 에셋 사용 함수 = 부드럽게 움직이게 만들어주는 에셋
    /// 카드매니저에서 사용
    /// </summary>
    /// <param name="prs">원본위치</param>
    /// <param name="useDotween">사용할건지</param>
    /// <param name="dotweenTime">걸리는 시간</param>
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
