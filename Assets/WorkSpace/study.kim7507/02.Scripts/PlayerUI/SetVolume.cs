using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider; // 슬라이더를 드래그하여 할당
    void Start()
    {
        // 믹서에서 현재 볼륨 값을 가져와 슬라이더에 반영
        float currentVolume;
        if (mixer.GetFloat("Master", out currentVolume))
        {
            // Log10을 사용하여 슬라이더 값으로 변환
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
    }

    public void SetLevel(float value)
    {
        mixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }
}
