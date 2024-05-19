using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public int numberOfLines = 3; // ��ֹ� ��(��)�� ����
    public GameObject obstaclePrefab; // ��ֹ� ������

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < numberOfLines; i++)
        {
            List<int> emptySpaces = GetEmptySpaces();
            for (int j = 0; j < 3; j++)
            {
                // ��ֹ��� ������ ��ġ ����
                Vector3 position = new Vector3(j, i, 0);
                if (emptySpaces.Contains(j))
                {
                    // �� ĭ�̸� ��ֹ��� �������� ����
                    continue;
                }
                Instantiate(obstaclePrefab, position, Quaternion.identity);
            }
        }
    }

    List<int> GetEmptySpaces()
    {
        List<int> emptySpaces = new List<int>();
        int emptySpaceCount = Random.Range(1, 4); // �ּ� 1������ �ִ� 3���� �� ĭ
        for (int i = 0; i < emptySpaceCount; i++)
        {
            emptySpaces.Add(Random.Range(0, 3)); // �� ĭ�� ��ġ�� �����ϰ� ����
        }
        return emptySpaces;
    }
}
