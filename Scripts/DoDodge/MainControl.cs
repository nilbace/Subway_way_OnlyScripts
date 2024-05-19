using JetBrains.Annotations;
using Naninovel;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MainControl: MonoBehaviour //��ü �δ������� ����
{
    private float _currentTime = 59.9f;

    public static MainControl Inst; //temporary singleton
    public TMP_Text TMP_Time;
    public int womanSeat = -1; //���ڰ� ���� �� ���� ����
    public delegate void stopGame();
    public static event stopGame StopGame;

    public GameObject GoodClearCanvas, BadClearCanvas, FailCanvas; //temp

    private IEnumerator StartTimer()
    {
        while (_currentTime > 0f) //���ѽð� 60��
        {
            TMP_Time.text = ((int)_currentTime + 1).ToString(); //��� �ð� ǥ��

            _currentTime -= Time.deltaTime;
            yield return null;
        }
        Fail(); //60�� �ʰ� �� ����
    }

    private bool IsNextSeat(int seat1, int seat2)
    {
        if (Mathf.Abs(seat1 - seat2) == 1) return true;
        return false;
    }

    
    private IEnumerator GoodClear()
    {
        yield return new WaitForSeconds(2f);
        GoodClearCanvas.SetActive(true);
    }

    private IEnumerator BadClear()
    {
        yield return new WaitForSeconds(2f);
        BadClearCanvas.SetActive(true);
    }

    private void Fail()
    {
        StopGame();
        TMP_Time.text = "0";
        FailCanvas.SetActive(true);
    }

    public void Clear(int manSeat)
    {
        StopAllCoroutines(); //Ÿ�̸� ����
        StopGame(); //�̺�Ʈ ��������Ʈ�� ��� ��ư ��Ȱ��ȭ
        if (IsNextSeat(womanSeat, manSeat)) StartCoroutine(GoodClear());
        else StartCoroutine(BadClear());
    }


    public void Penalty(float time)
    {
        _currentTime -= time;
    }


    void Start()
    {
        if (Inst == null) Inst = this;
        else Destroy(this); //temporary singleton

        StartCoroutine(StartTimer());
    }

}
