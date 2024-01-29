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
    [SerializeField] List<Card> myCards;        //내 카드 리스트
   // [SerializeField] List<Card> otherCards;     //적 카드 리스트
    [SerializeField] Transform cardSpawnPoint;     //카드 스폰 포인트
    [SerializeField] Transform myCardLeft;      //카드 정렬 위치
    [SerializeField] Transform myCardRight;     
    //[SerializeField] Transform otherCardLeft;    
   // [SerializeField] Transform otherCardRight;
    [SerializeField] ECardState eCardState;

    [SerializeField] List<Card> standbyCards;

    List<Item> itemBuffer;
    Card selectCard;
    bool isMyCardDrag;
    bool onMyCardArea;
    bool onStandByArea;
    enum ECardState {  Nothing, CanMouseOver, CanMouseDrag}
    CostManager costmanager;

    float originScale = 1.0f;
    float mouseOverYpos = -2.2f;
    float mouseOverScale = 1.8f;

    private void Start()        //시작할때 셋업
    {
        SetUpItemBuffer();
        TurnManager.OnAddCard += AddCard;
        costmanager = CostManager.Inst.GetComponent<CostManager>();
    }

    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
    }

    private void Update()       //1, 2누르면 테스트
    {
        if (isMyCardDrag)
        {
            CardDrag();
        }
        DetectCardArea();
        SetECardState();
    }
    public Item PopItem()   //아래에서 셋업한 아이템 버퍼에서 아이템 뽑기
    {
        if (itemBuffer.Count == 0)
        {
            SetUpItemBuffer();
        }
        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }
    void SetUpItemBuffer()      //아이템 버퍼에 아이템 넣기
    {
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)  //퍼센트 만큼 카드 뽑기 .확률아님
            {
                itemBuffer.Add(item);
            }
        }
        

        //필요없어질 기능
        for (int i = 0; i < itemBuffer.Count; i++)  //주르륵 나온것 랜덤으로 순서 바꾸기
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
        //card.Setup(PopItem(), isMine);
        card.Setup(PopItem(), true);
        //(isMine ? myCards : otherCards).Add(card);  //내가 맞으면 내 카드에 아니면 적 카드에
        myCards.Add(card);
        //SetOriginOrder(isMine); //카드 리스트에 추가하면서 솔팅 레이어 설정
        //CardAlignment(isMine);      //두트윈에셋 사용해 카드 정렬하며 움직이기--
        SetOriginOrder(true); //카드 리스트에 추가하면서 솔팅 레이어 설정
        CardAlignment(true);      //두트윈에셋 사용해 카드 정렬하며 움직이기--
    }

    void SetOriginOrder(bool isMine)    //솔팅 레이어 설정을 위한 함수 AddCard에서 사용
    {
        //int count = isMine ? myCards.Count : otherCards.Count;
        int count = myCards.Count;
        for(int i=0; i < count; i++)
        {
            //var targetCard = isMine ? myCards[i] : otherCards[i];
            var targetCard = myCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment(bool isMine)  //두트윈에셋 사용해 카드 정렬하며 움직이기--
    {
        //둥글게 정렬을 위한 추가
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine)
        {
            originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * originScale);
        }
        else
        {
           // originCardPRSs = RoundAlignment(otherCardLeft, otherCardRight, otherCards.Count, -0.5f, Vector3.one * originScale);
        }

        //둥글게 정렬을 위한 추가

        var targetCards = myCards;//isMine ? myCards : otherCards;
        for(int i=0; i<targetCards.Count; i++)
        {
            var targetCard = targetCards[i];
            
            targetCard.originPRS = originCardPRSs[i];   //new PRS(Vector3.zero, Utils.QI, Vector3.one * originScale);
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f); //두 트윈 에셋 함수
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount]; // 자기 포지션이 어느정도 되야 되는지 쿼터니온에서 사용됨
        List<PRS> results = new List<PRS>(objCount);    //리스트 만들 때 미리 obj 개수만큼 생성

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
            {           //원의 방정식 (x-a)^2 + (y-b)^2 = r^2
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)); //objLerps[i] - 0.5f 0과 1사이에서 0.5 빼니 중심이다
                curve = height >= 0 ? curve : -curve;   //제곱하면 무조건+ 이므로 부호를 다시 부여 
                //targetPos.y += curve;
                targetRot = Quaternion.Lerp(leftTr.rotation, rightTr.rotation, objLerps[i]); //제대로 작동안됨;; 회전이 안되는 문제
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        if (eCardState == ECardState.Nothing)   //eCardState == ECardState.Nothing일때 오버 못하게 막기
            return;

        selectCard = card;
        EnlargeCard(true, card);
    }
    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }
    public void CardMouseDown()     //마우스를 누르면 bool변수 isMyCardDrag ture로 바꿔서 Update의 CardDrag() 실행가능하게
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
    }
    public void CardMouseUp(Card card)       //마우스를 떼면 false로 바뀌며 카드의 위치를 다시 원래대로
    {
        isMyCardDrag = false;

        if (eCardState != ECardState.CanMouseDrag)
            return;

        //카드를 위에다 드래그 업 했을 때 카드는 위 쪽에 놓아지고 턴이 지나면 다시 돌아와야함
        //card.gameObject.SetActive(false);
        //card.gameObject.SetActive(true);
        if (onStandByArea)
        {
            StandbyCards(card);
        }
    }

    private void StandbyCards(Card card)    //상단에 드래그해서 놓으면 카드 삭제 및 standbyCards에 추가하고 
    {
        if (costmanager.curCost >= card.item.cost)
        {
            myCards.Remove(card);
            card.gameObject.SetActive(false);
            standbyCards.Add(card);
            costmanager.CostChange(-card.item.cost);
            CardAlignment(true);      //두트윈에셋 사용해 카드 정렬하며 움직이기--
        }
        else { return; }    //카드 코스트가 보유 코스트보다 높으면 무시
    }

    private void CardDrag()     //카드 드래그 = 마우스의 위치에 카드의 위치를 고정
    {
        if (!onMyCardArea)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }
    private void DetectCardArea()   //카드 선택 레이어 확대 및 드래그 가능 공간
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);    //마우스 위치에 레이캐스트 쏘기
        int layer = LayerMask.NameToLayer("MyCardArea");                                // 레이캐스트가 판단할 레이어 이름
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);   // 카드 공간에 마우스가 들어오면 true 아니면 false
        int standByLayer = LayerMask.NameToLayer("StandByArea");
        onStandByArea = Array.Exists(hits, x => x.collider.gameObject.layer == standByLayer);
    }

    void EnlargeCard(bool isEnlarge, Card card) //마우스 오버 시 크기 확대
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, mouseOverYpos, -10f); //카드가 커졌을때 겹쳐지는 다른 카드 무시하기 위해 앞으로 땡김
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * mouseOverScale), false);   //스케일 키움 두트윈에셋 사용 안함
        }
        else
        {
            card.MoveTransform(card.originPRS, false);  //마우스 exit하면 다시 원래대로
        }

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge); //솔팅 레이어 관련
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
