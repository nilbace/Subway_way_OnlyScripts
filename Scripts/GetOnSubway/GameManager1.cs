using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public float timeLimit = 120f; // ������ �ð� ���� (�� ����)
    public int targetCount = 4; // ��ǥ ��� ��
    public TMP_Text timerText; // UI�� ���� �ð��� ǥ���� �ؽ�Ʈ
    public TMP_Text targetText; // UI�� ���� ��ǥ ��� ���� ǥ���� �ؽ�Ʈ

    private float currentTime; // ���� �ð�
    private int remainingTargets; // ���� ��ǥ ��� ��

    void Start()
    {
        currentTime = timeLimit;
        remainingTargets = targetCount;
        UpdateUI();
        InvokeRepeating("UpdateTimer", 1f, 1f); // 1�ʸ��� UpdateTimer �Լ��� ȣ���Ͽ� �ð� ������Ʈ
    }

    void UpdateTimer()
    {
        currentTime -= 1f;
        if (currentTime <= 0f)
        {
            EndGame();
        }
        UpdateUI();
    }

    public void HitObstacle()
    {
        currentTime -= 5f;
        if (currentTime <= 0f)
        {
            EndGame();
        }
        UpdateUI();
    }

    public void PassLine()
    {
        remainingTargets--;
        if (remainingTargets <= 0)
        {
            ClearGame();
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
        targetText.text = "Targets: " + remainingTargets.ToString();
    }

    void EndGame()
    {
        Debug.Log("Game Over");
        // ���� ���� ó��
    }

    void ClearGame()
    {
        Debug.Log("Game Cleared");
        // Ŭ���� ó��
    }
}
