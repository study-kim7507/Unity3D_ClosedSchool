using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<Book> correctOrder;  // ���� å ����
    private List<Book> selectedBooks = new List<Book>();  // �÷��̾ ������ å ���

    [SerializeField] private GameObject rewardItemPrefab;  // ���� ������(���� �Ǵ� �ܼ�)
    [SerializeField] private Transform itemSpawnPoint;    // ������ ���� ��ġ

    [SerializeField] private GameObject ghostFace;        // �ͽ� �� ������Ʈ
    [SerializeField] private float ghostFaceDuration = 2f; // �ͽ� �� ǥ�� �ð�

    public void SelectBook(Book book)
    {
        if (!selectedBooks.Contains(book))
        {
            selectedBooks.Add(book);
            Debug.Log($"���õ� å: {book.GetBookName()}");

            if (selectedBooks.Count == correctOrder.Count)
            {
                CheckPuzzleSolution();  // ���� ���� Ȯ��
            }
        }
    }

    private void CheckPuzzleSolution()
    {
        bool isCorrect = true;

        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (selectedBooks[i].GetBookOrder() != correctOrder[i].GetBookOrder())
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("���� ����! �ùٸ� ������ ��ġ�߽��ϴ�.");
            RewardPlayer();  // ������ ȹ�� ����
        }
        else
        {
            Debug.Log("���� ����! ������ �ٽ� �õ��ϼ���.");
            ShowGhostFace(); // �ͽ� �� ǥ��
            ResetPuzzle();
        }
    }

    private void RewardPlayer()
    {
        if (rewardItemPrefab != null && itemSpawnPoint != null)
        {
            Instantiate(rewardItemPrefab, itemSpawnPoint.position, Quaternion.identity);
            Debug.Log("�������� ȹ���߽��ϴ�!");
        }
        else
        {
            Debug.LogWarning("���� ������ ������ �Ǵ� ���� ��ġ�� �������� �ʾҽ��ϴ�!");
        }
    }

    private void ResetPuzzle()
    {
        selectedBooks.Clear();  // ���õ� å �ʱ�ȭ
        Debug.Log("å ������ �ʱ�ȭ�Ǿ����ϴ�.");
    }

    private void ShowGhostFace()
    {
        if (ghostFace != null)
        {
            ghostFace.SetActive(true); // �ͽ� �� Ȱ��ȭ
            Invoke(nameof(HideGhostFace), ghostFaceDuration); // ���� �ð� �� ��Ȱ��ȭ
        }
        else
        {
            Debug.LogWarning("�ͽ� �� ������Ʈ�� �������� �ʾҽ��ϴ�!");
        }
    }

    private void HideGhostFace()
    {
        if (ghostFace != null)
        {
            ghostFace.SetActive(false); // �ͽ� �� ��Ȱ��ȭ
        }
    }
}
