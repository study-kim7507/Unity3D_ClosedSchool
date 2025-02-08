using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider; // �����̴��� �巡���Ͽ� �Ҵ�
    void Start()
    {
        // �ͼ����� ���� ���� ���� ������ �����̴��� �ݿ�
        float currentVolume;
        if (mixer.GetFloat("Master", out currentVolume))
        {
            // Log10�� ����Ͽ� �����̴� ������ ��ȯ
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
    }

    public void SetLevel(float value)
    {
        mixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }
}
