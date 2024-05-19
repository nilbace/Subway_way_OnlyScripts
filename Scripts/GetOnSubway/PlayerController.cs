using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.5f; // ���ΰ��� �̵� �ӵ�
    public float laneWidth = 1.5f; // ��ֹ� ���� ����

    private int currentLane = 1; // ���ΰ��� ���� ��ġ (0: ����, 1: �߾�, 2: ������)

    void Update()
    {
        // �¿� ȭ��ǥ Ű �Է��� �����Ͽ� ���ΰ��� �̵���ŵ�ϴ�.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            transform.position += Vector3.left * laneWidth;
        }
    }

    void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            transform.position += Vector3.right * laneWidth;
        }
    }
}
