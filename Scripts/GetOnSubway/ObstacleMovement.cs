using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // ��ֹ��� �̵� �ӵ�

    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);
    }
}