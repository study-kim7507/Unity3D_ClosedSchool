using UnityEngine;
using TMPro; // TextMeshPro »ç¿ë

public class GoalTrigger : MonoBehaviour
{
    public PuzzleManager3 puzzleManager3; // ÆÛÁñ ¸Å´ÏÀú
    public int requiredGoals = 3; // ÇÊ¿äÇÑ °ñ È½¼ö
    private int currentGoals = 0; // ÇöÀç °ñ ¼º°ø È½¼ö
    public TMP_Text goalCountText; // °ñ °³¼ö UI ÅØ½ºÆ®

    private void Start()
    {
        if (goalCountText != null)
        {
            goalCountText.gameObject.SetActive(false); // UI Ã³À½¿£ ¼û±è
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basketball")) // ³ó±¸°øÀÌ °ñ´ë¿¡ µé¾î°¬À» ¶§
        {
            currentGoals++;
            Debug.Log($"°ñ ¼º°ø! ÇöÀç °ñ ¼ö: {currentGoals}/{requiredGoals}");
            UpdateGoalCountUI();

            if (currentGoals >= requiredGoals) // 3°ñÀ» ³ÖÀ¸¸é ÆÛÁñ ¿Ï·á
            {
                puzzleManager3.CompletePuzzle();
                HideUI(); // ÆÛÁñ ¿Ï·á ÈÄ UI ¼û±è
            }
        }
    }

    private void UpdateGoalCountUI()
    {
        if (goalCountText != null)
        {
            goalCountText.text = $"{currentGoals}/{requiredGoals}";
            goalCountText.gameObject.SetActive(true); // UI È°¼ºÈ­
        }
    }

    private void HideUI()
    {
        if (goalCountText != null)
        {
            goalCountText.gameObject.SetActive(false);
        }
    }
}
