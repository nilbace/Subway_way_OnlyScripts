using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EachSeat : MonoBehaviour //�� ��ư�� ���� ����
{

    private enum State
    {
        Sit, Fake, Stand, Woman, Man
    }
    private State _currentState = State.Sit;

    public TMP_Text TMP_State;

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(Random.Range(3f, 7f)); //���� �� 3~7�� ���
        while (true)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < 0.33f) StartCoroutine(Fake()); //3���� 1 Ȯ���� ����ũ
            if (rand > 0.66f) StartCoroutine(Stand()); //3���� 1 Ȯ���� �Ͼ,
                                                       //���� 3���� 1 Ȯ���� ��� �ɾ�����

            yield return new WaitForSeconds(Random.Range(3f, 7f)); //3~7�ʸ��� ���� �ൿ
        }
    }

    private void DeActivate() //��ư ��Ȱ��ȭ
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
        Sit(); //1�� ��� �� �ٽ� ����
    }

    private IEnumerator Stand()
    {
        TMP_State.text = "Stand";
        _currentState = State.Stand;
        yield return new WaitForSeconds(1f);
        Sit(); //0.5(�ӽ� 1��) ��� �� �ٽ� ����
    }

    public void ButtonClick(int seat) //1~8�� ��ư���� ��ư ��ȣ ���޹���
    {
        if (_currentState == State.Stand)
        {
            DeActivate(); //��ư ����
            if (MainControl.Inst.womanSeat == -1) //���� ���ڰ� �� �ɾҴٸ�
            {
                MainControl.Inst.womanSeat = seat; //���� �¼���ȣ ����
                TMP_State.text = "Woman";
                _currentState = State.Woman;
                DeActivate();
            }
            else //���ڰ� �̹� ���� ����(���� ����)
            {
                TMP_State.text = "Man";
                _currentState = State.Man;
                MainControl.Inst.Clear(seat); //���� �¼���ȣ ����
            }
        }
        else //�Ͼ ���°� �ƴϸ�
        {
            MainControl.Inst.Penalty(10f); //10�� ���Ƽ
        }
    }

    void Start()
    {
        MainControl.StopGame += DeActivate; //��������Ʈ�� ��Ȱ�� �Լ� �߰�
        StartCoroutine(Activate());
    }

}
