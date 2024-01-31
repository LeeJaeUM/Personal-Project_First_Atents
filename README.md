# Personal-Project_First_Atents
 First atents Assignment

 ## 프로젝트명 : CADO
 ![image](https://github.com/LeeJaeUM/Personal-Project_First_Atents/assets/106094800/ca4e5652-ee75-44fd-8e3a-f76f3da0dd04)


 ### 프로젝트 설치 방법
 빌드된 파일을 다운 후 실행

### 프로젝트 소개 
카드를 사용해 타일의 위치를 바꾸거나 공격하여 적을 처치하는 카드게임
-----

### 플레이 영상 링크 : https://youtu.be/w5VEBYRP6Ls
----

### 플레이 방법
![explain_final](https://github.com/LeeJaeUM/Personal-Project_First_Atents/assets/106094800/e28ea6cb-ea64-4b6c-b190-f797d16d8b18)


## 중요 소스코드 및 코드 설명
- ItemSO
   - 카드에 들어갈 정보
- Card
  - 플레이어가 사용할 카드 및 정보를 받아와 표현
- CardManager
   - 카드를 뽑고 카드배열에 추가하는 기능 및 정렬. 마우스 이벤트 정의. 카드를 상단으로 드래그하여 사용 가능하게 한다.
- Order
    - 카드의 SpriteRender의 정렬 순서 정의 함수
- TurnManager
    - CardManager의 OnAddCard를 받아 턴이 지날 때 마다 카드를 플레이어 덱에 놓는 역할
- Tile
    - 플레이어와 적의 위치정보를 가진다. 타일 위에 있는 오브젝트에 적용될 함수 정의되어있음
- TileManager
    - 공격 및 이동 함수 정의. Tile의 정보를 가져와 어떤 Tile에 공격 또는 이동 함수를 실행 할 지 정함
- EnemyBase
    - TileManager의 EnemyTileAttack을 미리 값을 넣어놓아 뭘 사용할 지 정의해둔 코드  
- EnemyManager
    - TurnManager의 액션으로 턴을 감지하고 턴이 지날 때 마다 적의 행동을 정의
- GameManager
    - 플레이어 승리 시 작동할 코드 정의
