using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public string[] toolTips;

    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private TMP_Text toolTipText;
    [SerializeField] private TMP_Text loadingPercentageText;

    [SerializeField] private StartSceneFenceEntrance startSceneFenceEntrance;

    private bool isStarted = false;
    public void GameStartButton()
    {
        if (!isStarted)
        {
            StartCoroutine(GameStart());
            isStarted = true;
        }
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    private IEnumerator GameStart()
    {
        startSceneFenceEntrance.OpenDoor();

        float duration = 5.0f;
        float elapsedTime = 0.0f;

        // 초기 색상 설정
        Image image = fadeOutPanel.GetComponent<Image>();
        Color color = image.color;
        color.a = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            color.a = alpha;
            image.color = color;

            yield return null;
        }

        // 최종 색상 설정
        color.a = 1.0f;
        image.color = color;


        toolTipText.text = toolTips[Random.Range(0, toolTips.Length)];
        toolTipText.gameObject.SetActive(true);

        loadingPercentageText.text = "Loading";
        loadingPercentageText.gameObject.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GameScene");
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress == 0.15f) loadingPercentageText.text = "Loading.";
            else if (asyncOperation.progress == 0.45f) loadingPercentageText.text = "Loading..";
            else if (asyncOperation.progress == 0.70f) loadingPercentageText.text = "Loading...";
            else if (asyncOperation.progress >= 0.9f)
            {
                loadingPercentageText.text = "시작하려면 스페이스바를 누르세요.";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            

            yield return null;
        }
    }
}
