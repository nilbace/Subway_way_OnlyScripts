using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EachSeat : MonoBehaviour //각 버튼별 독립 동작
{

    private enum State
    {
        Sit, Fake, Stand, Woman, Man
    }
    private State _currentState = State.Sit;

    public TMP_Text TMP_State;

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f)); //시작 시 3~7초 대기
        while (true)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < 0.33f) StartCoroutine(Fake()); //3분의 1 확률로 페이크
            if (rand > 0.66f) StartCoroutine(Stand()); //3분의 1 확률로 일어남,
                                                       //남은 3분의 1 확률은 계속 앉아있음

            yield return new WaitForSeconds(Random.Range(3f, 7f)); //3~7초마다 위의 행동
        }
    }

    private void DeActivate() //버튼 비활성화
    {
        StopAllCoroutines();
        GetComponent<Button>().interactable = false;
    }

    private void Sit()
    {
        TMP_State.text = "Sit";
        _currentState = State.Sit;
    }

    private IEnumerator Fake()
    {
        TMP_State.text = "Fake";
        _currentState = State.Fake;
        yield return new WaitForSeconds(1f);
        Sit(); //1초 대기 후 다시 착석
    }

    private IEnumerator Stand()
    {
        TMP_State.text = "Stand";
        _currentState = State.Stand;
        yield return new WaitForSeconds(1f);
        Sit(); //0.5(임시 1초) 대기 후 다시 착석
    }

    public void ButtonClick(int seat) //1~8번 버튼마다 버튼 번호 전달받음
    {
        if (_currentState == State.Stand)
        {
            DeActivate(); //버튼 고정
            if (MainControl.Inst.womanSeat == -1) //아직 여자가 안 앉았다면
            {
                MainControl.Inst.womanSeat = seat; //여자 좌석번호 전달
                TMP_State.text = "Woman";
                _currentState = State.Woman;
                DeActivate();
            }
            else //여자가 이미 앉은 상태(게임 성공)
            {
                TMP_State.text = "Man";
                _currentState = State.Man;
                MainControl.Inst.Clear(seat); //남자 좌석번호 전달
            }
        }
        else //일어선 상태가 아니면
        {
            MainControl.Inst.Penalty(10f); //10초 페널티
        }
    }

    void Start()
    {
        MainControl.StopGame += DeActivate; //델리게이트에 비활성 함수 추가
        StartCoroutine(Activate());
    }

}
