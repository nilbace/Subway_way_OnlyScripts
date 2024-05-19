using System.Collections;
using System.Collections.Generic;
using UnityEditor.EventSystems;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CardFlipManager : MonoBehaviour
{
    private GameObject _raycastObj;
    private GameObject _selectedCard1;
    private GameObject _selectedCard2;


    [SerializeField]
    private int _limitTime;     //제한시간 int or float 생각해보기
    [SerializeField]
    private int _numOfCard;     //만들어지는 카드 개수
    [SerializeField]
    private int _numOfFoundCard;

    private float _curTime;
    [SerializeField]
    private bool _canFlipCard;

    [SerializeField]
    private TextMeshProUGUI _timeText;

    private void CardPrefabInitiate()
    {
        //카드 프리팹 생성

    }
    private void CastRay()
    {
        //Raycast로 Object 선택
        _raycastObj = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            _raycastObj = hit.collider.gameObject;
            CardSelect(_raycastObj);
        }
    }
    IEnumerator BackToFront(GameObject cardObject)
    {
        _canFlipCard = false;
        //카드 선택할때
        var tween = cardObject.transform.DORotate(new Vector3(0, 180, 0), 1.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        _canFlipCard = true;
        //얘가 여기 들어가면 안될거 같긴 한데 일단 코루틴이 끝나고 이게 실행되어야 해서 여기에 박아둠 추후 수정할수 있으면 수정할거임
        if (_selectedCard2 !=  null)
        {
            CardFlipResult();
        }
    }
    IEnumerator FrontToBack(GameObject cardObject)
    {
        //카드 틀렸을때 다시 뒤집기
        _canFlipCard = false;
        var tween = cardObject.transform.DORotate(new Vector3(0, 360, 0), 1.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        _canFlipCard = true;
    }
    private void CardSelect(GameObject cardObject)
    {
        StartCoroutine(BackToFront(cardObject));
        // 삼항 연산자로 대체 가능한지 피드백 필요
        if (_selectedCard1 == null)
        {
            Debug.Log("1번카드 선택했어요");
            //1번 선택카드가 NULL인 경우 1번에 할당
            _selectedCard1 = cardObject;
            StartCoroutine(BackToFront(cardObject));
            //선택한 카드 선택 불가능하게 설정
            _selectedCard1.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Debug.Log("둘다 선택했어요");
            //1번 선택카드가 NULL이 아니면 2번에 할당
            _selectedCard2 = cardObject;
            StartCoroutine(BackToFront(cardObject));
            //선택한 카드 선택 불가능하게 설정
            _selectedCard2.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void CardFlipResult()
    {
        //1번 카드와 2번 카드 코드 비교 삼항연산자로 안되는듯 하다
        if (_selectedCard1.GetComponent<CardBase>().GetCardCode() == _selectedCard2.GetComponent<CardBase>().GetCardCode())
        {
            Debug.Log("같은  카드네요");
            //동일하면 그대로 두고 찾은 카드수 +2
            _numOfFoundCard += 2;

            //카드 다 찾았으면 게임 종료
            if (_numOfCard ==  _numOfFoundCard)
            {
                GameClear();
            }
        }
        else
        {
            Debug.Log("다른 카드네요");
            //다르면 뒤집기
            StartCoroutine(FrontToBack(_selectedCard1));
            StartCoroutine(FrontToBack(_selectedCard2));
            _selectedCard1.GetComponent<BoxCollider2D>().enabled = true;
            _selectedCard2.GetComponent<BoxCollider2D>().enabled = true;
        }
        //선택 카드 비우기
        _selectedCard1 = null;
        _selectedCard2 = null;
    }
    private  void GameClear()
    {
        Debug.Log("클리어!");
        //카드 다 찾으면 게임클리어
    }
    //일단 public으로 설정하고 나중에 private로 바꿀거임  => 버튼에 넣기 위해서
    public void TimerStart()
    {
        _canFlipCard = true;
        StartCoroutine(StartTimer());
    }
    private void  GameOver()
    {
        //타이머 다 되었을때 게임오버
    }
    IEnumerator StartTimer()
    {
        _curTime = _limitTime;
        while(_curTime  > 0)
        {
            _curTime -= Time.deltaTime;
            _timeText.text = ((int)_curTime).ToString();
            yield return null; 

            if (_curTime <= 0)
            {
                Debug.Log("타이머 종료");
                GameOver();
                _curTime = 0;
                yield break;
            }
        }
    }

    // 만들어야 하는것
    //카드 뒤집기 애니메이션 만들긴 했는데 이상하다
    //카드 프리팹 생성 + 같은 카드 이미지
    void Start()
    {
        _canFlipCard = false;
        _numOfCard = 12;
        _numOfFoundCard = 0;
        _limitTime = 60;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)&& _canFlipCard)
        {
            CastRay();
        }
    }
}
