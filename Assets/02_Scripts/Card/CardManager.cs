using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> myCards;        //�� ī�� ����Ʈ
    [SerializeField] List<Card> otherCards;     //�� ī�� ����Ʈ
    [SerializeField] Transform cardSpawnPoint;     //ī�� ���� ����Ʈ
    [SerializeField] Transform myCardLeft;      //ī�� ���� ��ġ
    [SerializeField] Transform myCardRight;     
    [SerializeField] Transform otherCardLeft;    
    [SerializeField] Transform otherCardRight;
    [SerializeField] ECardState eCardState;  

    List<Item> itemBuffer;
    Card selectCard;
    bool isMyCardDrag;
    bool onMyCardArea;
    enum ECardState {  Nothing, CanMouseOver, CanMouseDrag}


    float originScale = 1.0f;
    float mouseOverYpos = -2.2f;
    float mouseOverScale = 1.8f;

    private void Start()        //�����Ҷ� �¾�
    {
        SetUpItemBuffer();
        TurnManager.OnAddCard += AddCard;
    }

    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
    }

    private void Update()       //1, 2������ �׽�Ʈ
    {
        if (isMyCardDrag)
        {
            CardDrag();
        }
        DetectCardArea();
        SetECardState();
    }
    public Item PopItem()   //�Ʒ����� �¾��� ������ ���ۿ��� ������ �̱�
    {
        if (itemBuffer.Count == 0)
        {
            SetUpItemBuffer();
        }
        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }
    void SetUpItemBuffer()      //������ ���ۿ� ������ �ֱ�
    {
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)  //�ۼ�Ʈ ��ŭ ī�� �̱� Ȯ���ƴ�
            {
                itemBuffer.Add(item);
            }
        }

        for (int i = 0; i < itemBuffer.Count; i++)  //�ָ��� ���°� �������� ���� �ٲٱ�
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    void AddCard(bool isMine)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopItem(), isMine);
        (isMine ? myCards : otherCards).Add(card);  //���� ������ �� ī�忡 �ƴϸ� �� ī�忡

        SetOriginOrder(isMine); //ī�� ����Ʈ�� �߰��ϸ鼭 ���� ���̾� ����
        CardAlignment(isMine);      //��Ʈ������ ����� ī�� �����ϸ� �����̱�--
    }

    void SetOriginOrder(bool isMine)    //���� ���̾� ������ ���� �Լ� AddCard���� ���
    {
        int count = isMine ? myCards.Count : otherCards.Count;
        for(int i=0; i < count; i++)
        {
            var targetCard = isMine ? myCards[i] : otherCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment(bool isMine)  //��Ʈ������ ����� ī�� �����ϸ� �����̱�--
    {
        //�ձ۰� ������ ���� �߰�
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine)
        {
            originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * originScale);
        }
        else
        {
            originCardPRSs = RoundAlignment(otherCardLeft, otherCardRight, otherCards.Count, -0.5f, Vector3.one * originScale);
        }
            
        //�ձ۰� ������ ���� �߰�

        var targetCards = isMine ? myCards : otherCards;
        for(int i=0; i<targetCards.Count; i++)
        {
            var targetCard = targetCards[i];
            
            targetCard.originPRS = originCardPRSs[i];   //new PRS(Vector3.zero, Utils.QI, Vector3.one * originScale);
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f); //�� Ʈ�� ���� �Լ�
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount]; // �ڱ� �������� ������� �Ǿ� �Ǵ��� ���ʹϿ¿��� ����
        List<PRS> results = new List<PRS>(objCount);    //����Ʈ ���� �� �̸� obj ������ŭ ����

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f}; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for(int i=0; i < objCount; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        for(int i=0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if(objCount >= 4)
            {           //���� ������ (x-a)^2 + (y-b)^2 = r^2
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)); //objLerps[i] - 0.5f 0�� 1���̿��� 0.5 ���� �߽��̴�
                curve = height >= 0 ? curve : -curve;   //�����ϸ� ������+ �̹Ƿ� ��ȣ�� �ٽ� �ο� 
                targetPos.y += curve;
                targetRot = Quaternion.Lerp(leftTr.rotation, rightTr.rotation, objLerps[i]); //����� �۵��ȵ�;; ȸ���� �ȵǴ� ����
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        if (eCardState == ECardState.Nothing)   //eCardState == ECardState.Nothing�϶� ���� ���ϰ� ����
            return;

        selectCard = card;
        EnlargeCard(true, card);
    }
    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }
    public void CardMouseDown()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
    }
    public void CardMouseUp()
    {
        isMyCardDrag = false;

        if (eCardState != ECardState.CanMouseDrag)
            return;

    }
    private void CardDrag()
    {
        if (!onMyCardArea)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }
    private void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    void EnlargeCard(bool isEnlarge, Card card) //���콺 ���� �� ũ�� Ȯ��
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, mouseOverYpos, -10f); //ī�尡 Ŀ������ �������� �ٸ� ī�� �����ϱ� ���� ������ ����
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * mouseOverScale), false);   //������ Ű�� ��Ʈ������ ��� ����
        }
        else
        {
            card.MoveTransform(card.originPRS, false);  //���콺 exit�ϸ� �ٽ� �������
        }

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge); //���� ���̾� ����
    }

    void SetECardState()
    {
        if (TurnManager.Inst.isLoading)
            eCardState = ECardState.Nothing;
        else if (!TurnManager.Inst.myTurn)
            eCardState = ECardState.CanMouseOver;
        else if (TurnManager.Inst.myTurn)
            eCardState = ECardState.CanMouseDrag;
            
    }

    #endregion


}
