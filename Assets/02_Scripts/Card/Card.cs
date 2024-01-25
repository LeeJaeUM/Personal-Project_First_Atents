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
    [SerializeField] TMP_Text costTMP;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    public Item item;
    bool isFront;           //�ո����� �޸����� �Ǵ�
    public PRS originPRS;   //������ġ : ���콺 �̺�Ʈ�� Ȯ��/��ҷ� ��ġ�� �ٲ� ���ƿ� ��ġ

    public void Setup(Item _item, bool _isFront)
    {
        this.item = _item;
        this.isFront = _isFront;

        if (this.isFront) ///�ո��϶�
        {
            character.sprite = this.item.sprite;
            nameTMP.text = this.item.name;
            attackTMP.text = this.item.attack.ToString();
            costTMP.text = this.item.cost.ToString();
        }
        else
        {
            card.sprite = cardBack;
            nameTMP.text = "";
            attackTMP.text = "";
            costTMP.text = "";
        }
    }

    /// <summary>
    /// ��Ʈ�� ���� ��� �Լ� = �ε巴�� �����̰� ������ִ� ����
    /// ī��Ŵ������� ���
    /// </summary>
    /// <param name="prs">������ġ</param>
    /// <param name="useDotween">����Ұ���</param>
    /// <param name="dotweenTime">�ɸ��� �ð�</param>
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

    private void OnMouseOver()
    {
        if (isFront)
            CardManager.Inst.CardMouseOver(this);
    }

    private void OnMouseExit()
    {
        if (isFront)
            CardManager.Inst.CardMouseExit(this);
    }
    private void OnMouseDown()
    {
        if (isFront)
            CardManager.Inst.CardMouseDown();
    }
    private void OnMouseUp()
    {
        if (isFront)
            CardManager.Inst.CardMouseUp(this);
    }
}
