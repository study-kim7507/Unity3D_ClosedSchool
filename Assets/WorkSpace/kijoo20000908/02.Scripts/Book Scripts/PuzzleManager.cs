using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private List<Book> correctOrder;  // 정답 책 순서
    private List<Book> selectedBooks = new List<Book>();  // 플레이어가 선택한 책 목록

    [SerializeField] private GameObject rewardItemPrefab;  // 보상 아이템(열쇠 또는 단서)
    [SerializeField] private Transform itemSpawnPoint;    // 아이템 생성 위치

    [SerializeField] private GameObject ghostFace;        // 귀신 얼굴 오브젝트
    [SerializeField] private float ghostFaceDuration = 2f; // 귀신 얼굴 표시 시간
    [SerializeField] private AudioClip ghostSound;        // 귀신 소리
    private AudioSource audioSource;

    private void Start()
    {
        if (ghostFace != null)
        {
            ghostFace.SetActive(false); // 게임 시작 시 귀신 얼굴 비활성화
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 자동 재생 비활성화
    }

    public void SelectBook(Book book)
    {
        if (!selectedBooks.Contains(book))
        {
            selectedBooks.Add(book);
            Debug.Log($"선택된 책: {book.GetBookName()}");

            if (selectedBooks.Count == correctOrder.Count)
            {
                CheckPuzzleSolution();
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
            Debug.Log("퍼즐 성공! 올바른 순서로 배치했습니다.");
            RewardPlayer();  // 아이템 획득 로직
        }
        else
        {
            Debug.Log("퍼즐 실패! 순서를 다시 시도하세요.");
            ShowGhostFace(); // 귀신 얼굴 표시 및 소리 재생
            ResetPuzzle();
        }
    }

    private void RewardPlayer()
    {
        if (rewardItemPrefab != null && itemSpawnPoint != null)
        {
            Instantiate(rewardItemPrefab, itemSpawnPoint.position, Quaternion.identity);
            Debug.Log("아이템을 획득했습니다!");
        }
        else
        {
            Debug.LogWarning("보상 아이템 프리팹 또는 스폰 위치가 설정되지 않았습니다!");
        }
    }

    private void ResetPuzzle()
    {
        selectedBooks.Clear();
        Debug.Log("책 선택이 초기화되었습니다.");
    }

    private void ShowGhostFace()
    {
        if (ghostFace != null)
        {
            ghostFace.SetActive(true); // 귀신 얼굴 활성화

            // 섬뜩한 소리 재생
            if (ghostSound != null)
            {
                audioSource.clip = ghostSound;
                audioSource.loop = true; // 소리 반복 재생
                audioSource.Play();
            }

            // 일정 시간 후 귀신 얼굴 비활성화
            Invoke(nameof(HideGhostFace), ghostFaceDuration);
        }
        else
        {
            Debug.LogWarning("귀신 얼굴 오브젝트가 설정되지 않았습니다!");
        }
    }

    private void HideGhostFace()
    {
        if (ghostFace != null)
        {
            ghostFace.SetActive(false); // 귀신 얼굴 비활성화
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // 소리 중지
        }
    }
}
