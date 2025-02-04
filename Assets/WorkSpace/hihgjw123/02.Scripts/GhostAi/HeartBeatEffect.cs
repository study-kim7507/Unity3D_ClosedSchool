using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HeartbeatEffect : MonoBehaviour
{
   
    [SerializeField] private Volume postProcessVolume;   // Post Process Volume
    [SerializeField] private float effectDuration = 3f;  // 효과 지속 시간
    [SerializeField] private float maxIntensity = 0.5f;  // 최대 붉은 효과 강도
    [SerializeField] GhostAiController ghostAiController;


    public AudioSource heartbeatSound;  // 심장박동 소리
    private ColorAdjustments colorAdjustments;
    public bool isEffectActive = false;
    private float effectTimeElapsed = 0f;

    private void Start()
    {
        // PostProcess에서 Color Adjustments 가져오기
        if (postProcessVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.colorFilter.Override(Color.white); // 초기 색상 설정
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isEffectActive)
        {
            // 심장박동 효과가 활성화된 경우 색상을 주기적으로 변화시킴
            effectTimeElapsed += Time.deltaTime;
            float t = Mathf.PingPong(effectTimeElapsed * 2f, 1f); // 주기적으로 색상 변화

            // 심장박동 소리와 함께 색상을 붉은색으로 보간
            colorAdjustments.colorFilter.Override(Color.Lerp(Color.white, Color.red, t * maxIntensity));

            // 효과 시간이 다 되면 색상 원래대로 복귀
            if (effectTimeElapsed >= effectDuration)
            {
                colorAdjustments.colorFilter.Override(Color.white);
                isEffectActive = false; // 효과 비활성화
                effectTimeElapsed = 0f;  // 시간 리셋
            }
              
        }
    }
}

