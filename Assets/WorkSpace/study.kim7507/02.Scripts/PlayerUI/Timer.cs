using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }
    public float playTime;
    public TMP_Text timerText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        playTime += Time.deltaTime;

        // 지금까지 플레이타임을 시, 분, 초, 밀리초로 변환
        int hours = (int)(playTime / 3600);
        int minutes = (int)((playTime % 3600) / 60);
        int seconds = (int)(playTime % 60);

        timerText.text = string.Format("{0:D2} : {1:D2} : {2:D2}", hours, minutes, seconds);
    }
}
