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
    private int _limitTime;     //���ѽð� int or float �����غ���
    [SerializeField]
    private int _numOfCard;     //��������� ī�� ����
    [SerializeField]
    private int _numOfFoundCard;

    private float _curTime;
    [SerializeField]
    private bool _canFlipCard;

    [SerializeField]
    private TextMeshProUGUI _timeText;

    private void CardPrefabInitiate()
    {
        //ī�� ������ ����

    }
    private void CastRay()
    {
        //Raycast�� Object ����
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
        //ī�� �����Ҷ�
        var tween = cardObject.transform.DORotate(new Vector3(0, 180, 0), 1.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        _canFlipCard = true;
        //�갡 ���� ���� �ȵɰ� ���� �ѵ� �ϴ� �ڷ�ƾ�� ������ �̰� ����Ǿ�� �ؼ� ���⿡ �ھƵ� ���� �����Ҽ� ������ �����Ұ���
        if (_selectedCard2 !=  null)
        {
            CardFlipResult();
        }
    }
    IEnumerator FrontToBack(GameObject cardObject)
    {
        //ī�� Ʋ������ �ٽ� ������
        _canFlipCard = false;
        var tween = cardObject.transform.DORotate(new Vector3(0, 360, 0), 1.0f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        _canFlipCard = true;
    }
    private void CardSelect(GameObject cardObject)
    {
        StartCoroutine(BackToFront(cardObject));
        // ���� �����ڷ� ��ü �������� �ǵ�� �ʿ�
        if (_selectedCard1 == null)
        {
            Debug.Log("1��ī�� �����߾��");
            //1�� ����ī�尡 NULL�� ��� 1���� �Ҵ�
            _selectedCard1 = cardObject;
            StartCoroutine(BackToFront(cardObject));
            //������ ī�� ���� �Ұ����ϰ� ����
            _selectedCard1.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Debug.Log("�Ѵ� �����߾��");
            //1�� ����ī�尡 NULL�� �ƴϸ� 2���� �Ҵ�
            _selectedCard2 = cardObject;
            StartCoroutine(BackToFront(cardObject));
            //������ ī�� ���� �Ұ����ϰ� ����
            _selectedCard2.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void CardFlipResult()
    {
        //1�� ī��� 2�� ī�� �ڵ� �� ���׿����ڷ� �ȵǴµ� �ϴ�
        if (_selectedCard1.GetComponent<CardBase>().GetCardCode() == _selectedCard2.GetComponent<CardBase>().GetCardCode())
        {
            Debug.Log("����  ī��׿�");
            //�����ϸ� �״�� �ΰ� ã�� ī��� +2
            _numOfFoundCard += 2;

            //ī�� �� ã������ ���� ����
            if (_numOfCard ==  _numOfFoundCard)
            {
                GameClear();
            }
        }
        else
        {
            Debug.Log("�ٸ� ī��׿�");
            //�ٸ��� ������
            StartCoroutine(FrontToBack(_selectedCard1));
            StartCoroutine(FrontToBack(_selectedCard2));
            _selectedCard1.GetComponent<BoxCollider2D>().enabled = true;
            _selectedCard2.GetComponent<BoxCollider2D>().enabled = true;
        }
        //���� ī�� ����
        _selectedCard1 = null;
        _selectedCard2 = null;
    }
    private  void GameClear()
    {
        Debug.Log("Ŭ����!");
        //ī�� �� ã���� ����Ŭ����
    }
    //�ϴ� public���� �����ϰ� ���߿� private�� �ٲܰ���  => ��ư�� �ֱ� ���ؼ�
    public void TimerStart()
    {
        _canFlipCard = true;
        StartCoroutine(StartTimer());
    }
    private void  GameOver()
    {
        //Ÿ�̸� �� �Ǿ����� ���ӿ���
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
                Debug.Log("Ÿ�̸� ����");
                GameOver();
                _curTime = 0;
                yield break;
            }
        }
    }

    // ������ �ϴ°�
    //ī�� ������ �ִϸ��̼� ����� �ߴµ� �̻��ϴ�
    //ī�� ������ ���� + ���� ī�� �̹���
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
