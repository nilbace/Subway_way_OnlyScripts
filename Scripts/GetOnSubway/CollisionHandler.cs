using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameManager1 gameManager; // GameManager ��ũ��Ʈ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            gameManager.HitObstacle(); // ��ֹ��� �浹�� �� �ð� ����
        }
    }
}