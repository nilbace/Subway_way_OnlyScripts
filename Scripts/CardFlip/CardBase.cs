using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBase : MonoBehaviour
{
    [SerializeField]//���߿� Ȯ�εǸ� ������ ��
    private int _cardCode;

    public void SetCardCode(int code)
    {
        _cardCode = code;
    }
    public int GetCardCode()
    {
        return _cardCode;
    }
    // Start is called before the first frame update
    void Start()
    {
        //default
        _cardCode = 0;
    }
}
